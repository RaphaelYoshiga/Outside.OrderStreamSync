using Shouldly;

namespace Outside.OrderStreamSync.Specs
{
    public class BackOfficeOrderExample
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public int SalesChannelId { get; set; }
        public decimal OrderAmount { get; set; }

        public bool Matches(BackOfficeOrder actualOrder)
        {
            actualOrder.OrderId.ShouldBe(OrderId);
            actualOrder.CustomerEmail.ShouldBe(CustomerEmail);
            actualOrder.SalesChannelId.ShouldBe(SalesChannelId);
            actualOrder.OrderAmount.ShouldBe(OrderAmount);

            return true;
        }
    }
}