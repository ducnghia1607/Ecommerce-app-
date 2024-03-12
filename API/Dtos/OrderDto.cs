using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BasketId { get; set; }
        public AddressDto ShipToAddress { get; set; }

        public int DeliveryMethodId { get; set; }
    }
}