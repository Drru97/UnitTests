namespace Domain.Models
{
    public class SimpleOrderRequest : OrderRequest
    {
        public string Item { get; set; }
        public int Count { get; set; }
    }
}
