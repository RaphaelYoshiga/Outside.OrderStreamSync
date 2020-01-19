namespace Outside.OrderStreamSync
{
    public interface IBackOfficeOrderFactory
    {
        BackOfficeOrder InstantiateFrom(OrderMessage orderMessage, int salesChannelId);
    }

    public class BackOfficeOrderFactory : IBackOfficeOrderFactory
    {
        public BackOfficeOrder InstantiateFrom(OrderMessage orderMessage, int salesChannelId)
        {
            return new BackOfficeOrder
            {
                CustomerEmail = orderMessage.CustomerEmail,
                SalesChannelId = salesChannelId,
                OrderAmount = orderMessage.OrderAmount,
                OrderId = orderMessage.OrderId
            };
        }
    }
}
