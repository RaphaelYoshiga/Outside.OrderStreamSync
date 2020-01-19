using System;
using System.Threading.Tasks;
using Moq;
using Outside.OrderStreamSync;
using Xunit;

namespace Outside.AtddExamples.UnitTests
{
    public class OrderStreamSyncerShould
    {
        private readonly OrderStreamSyncer _orderStreamSyncer;
        private readonly Mock<ISalesChannelIdQuery> _salesChannelIdQueryMock;
        private readonly Mock<IBackOfficeOrderFactory> _backOfficeOrderFactoryMock;
        private readonly Mock<IOrderPersister> _orderPersisterMock;

        public OrderStreamSyncerShould()
        {
            _salesChannelIdQueryMock = new Mock<ISalesChannelIdQuery>();
            _backOfficeOrderFactoryMock = new Mock<IBackOfficeOrderFactory>();
            _orderPersisterMock = new Mock<IOrderPersister>();
            _orderStreamSyncer = new OrderStreamSyncer(_salesChannelIdQueryMock.Object, _backOfficeOrderFactoryMock.Object, _orderPersisterMock.Object);
        }

        [Theory]
        [InlineData("salesChannel", 9)]
        [InlineData("WebSite", 10)]
        public async Task PersistOrderToDatabase(string salesChannel, int salesChannelId)
        {
            OrderMessage orderMessage = new OrderMessage()
            {
                SalesChannel = salesChannel
            };
            var backOfficeOrder = new BackOfficeOrder();
            _salesChannelIdQueryMock.Setup(p => p.GetIdBy(salesChannel))
                .ReturnsAsync(salesChannelId);
            _backOfficeOrderFactoryMock.Setup(p => p.InstantiateFrom(orderMessage, salesChannelId))
                .Returns(backOfficeOrder);

            await _orderStreamSyncer.Sync(orderMessage);

            _orderPersisterMock.Verify(p => p.Persist(backOfficeOrder));

        }
    }
}
