using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Infrastructure;

namespace Core.Specification
{
    public class OrdersWithItemsAndDeliverySpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndDeliverySpecification()
        {
        }

        public OrdersWithItemsAndDeliverySpecification(string email) : base(x => x.BuyerEmail == email)
        {
            AddInClude(x => x.OrderItems);
            AddInClude(x => x.DeliveryMethod);
            AddOrderByDescending(x => x.OrderDate);
        }


        public OrdersWithItemsAndDeliverySpecification(int id, string email) : base(x => x.BuyerEmail == email && x.Id == id)
        {
            AddInClude(x => x.OrderItems);
            AddInClude(x => x.DeliveryMethod);
        }
    }
}