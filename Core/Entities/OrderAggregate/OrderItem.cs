using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrder productItemOrder, decimal price, int quantity)
        {
            ItemOrdered = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder ItemOrdered { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}