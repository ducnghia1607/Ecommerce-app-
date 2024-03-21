using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Infrastructure;

namespace Core.Specification
{
    public class OrderByPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}