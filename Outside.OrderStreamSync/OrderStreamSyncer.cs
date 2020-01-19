using System.Threading.Tasks;

namespace Outside.OrderStreamSync
{
    public class OrderStreamSyncer
    {
        private readonly ISalesChannelIdQuery _salesChannelIdQuery;
        private readonly IBackOfficeOrderFactory _backOfficeOrderFactory;
        private readonly IOrderPersister _orderPersister;

        public OrderStreamSyncer(ISalesChannelIdQuery salesChannelIdQuery, 
            IBackOfficeOrderFactory backOfficeOrderFactory,
            IOrderPersister orderPersister)
        {
            _orderPersister = orderPersister;
            _backOfficeOrderFactory = backOfficeOrderFactory;
            _salesChannelIdQuery = salesChannelIdQuery;
        }

        public async Task Sync(OrderMessage orderMessage)
        {
            var salesChannelId = await _salesChannelIdQuery.GetIdBy(orderMessage.SalesChannel);
            var backOfficeOrder = _backOfficeOrderFactory.InstantiateFrom(orderMessage, salesChannelId);
            await _orderPersister.Persist(backOfficeOrder);
        }
    }
}