using System;
using Data.Entities;

namespace Domain.Models
{
    public class SimpleOrderResult : OrderResult
    {
        public SimpleOrderResult(Order order)
        {
            this.OrderId = Guid.NewGuid();
            this.EstimatedDelivery = order?.EstimatedDelivery ?? TimeSpan.Zero;
        }

        public TimeSpan EstimatedDelivery { get; set; }
    }
}
