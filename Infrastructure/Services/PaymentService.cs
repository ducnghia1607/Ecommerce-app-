using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PaymentService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepo.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            // Options truoc deliveryMethod thi moi co HasValue property
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                    .GetByIdAsync((int)basket.DeliveryMethodId);

                // Argument 1: cannot convert from 'int?' to 'int'CS1503
                // -> (int) basket.DeliveryMethodId
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var paymentItentService = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    PaymentMethodTypes = new List<string> { "card" },
                    Currency = "usd"
                };

                intent = await paymentItentService.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };

                intent = await paymentItentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepo.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;
            order.OrderStatus = OrderStatus.PaymentFailed;
            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;
            order.OrderStatus = OrderStatus.PaymentReceived;
            await _unitOfWork.Complete();
            return order;
        }
    }
}