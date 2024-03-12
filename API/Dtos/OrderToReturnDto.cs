using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderToReturnDto
    {

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public AddressOrder ShipToAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

    }
}