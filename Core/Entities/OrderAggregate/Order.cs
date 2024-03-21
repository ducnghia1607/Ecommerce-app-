using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, IReadOnlyList<OrderItem> orderItems, AddressOrder address,
        DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {

            BuyerEmail = buyerEmail;
            OrderItems = orderItems;
            ShipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }
        public string BuyerEmail { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public AddressOrder ShipToAddress { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public decimal Subtotal { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}