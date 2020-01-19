namespace Outside.OrderStreamSync
{
    public class OrderMessage
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public string SalesChannel { get; set; }
        public decimal OrderAmount { get; set; }
    }
}