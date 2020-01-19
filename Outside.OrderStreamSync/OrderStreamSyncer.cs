using System.Threading.Tasks;

namespace Outside.OrderStreamSync
{
    public class OrderStreamSyncer
    {
        public OrderStreamSyncer(ISalesChannelIdQuery salesChannelIdQuery, IOrderPersister orderPersister)
        {
        }

        public Task Sync(OrderMessage orderMessage)
        {
            return Task.CompletedTask;;
        }
    }
}