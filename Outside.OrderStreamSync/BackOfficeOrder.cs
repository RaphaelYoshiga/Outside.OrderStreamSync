namespace Outside.OrderStreamSync
{
    public class BackOfficeOrder
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public int SalesChannelId { get; set; }
        public decimal OrderAmount { get; set; }
    }
}