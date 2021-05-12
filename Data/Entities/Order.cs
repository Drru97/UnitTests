using System;

namespace Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime PlacedDate { get; set; }
        public string Item { get; set; }
        public int Count { get; set; }
        public TimeSpan EstimatedDelivery { get; set; }
    }
}
