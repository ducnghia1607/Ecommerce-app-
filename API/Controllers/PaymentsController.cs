using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly string _whSecret;
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger, IConfiguration config)
        {
            _paymentService = paymentService;
            _logger = logger;
            _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        }



        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> createOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            // And this contains utility methods to process event objects in stripes, web hooks.
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order order;
            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded :", intent.Id);
                    //TODO : Update the order with new status 
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order update payment succeeded :", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed :", intent.Id);
                    //TODO : Update the order with new status 
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order update payment succeeded :", order.Id);

                    break;

            }
            // And what Stripe is expecting back is us to return a new empty result.
            //That way, Stripe won't continue to retry to send this event to our API server.
            return new EmptyResult();
        }
    }
}