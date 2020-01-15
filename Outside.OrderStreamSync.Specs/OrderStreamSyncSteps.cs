using System;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Outside.OrderStreamSync.Specs
{
    [Binding]
    public class OrderStreamSyncSteps
    {
        private readonly Mock<ISalesChannelIdQuery> _salesChannelQueryMock;
        private readonly OrderStreamSyncer _orderStreamSyncer;
        private readonly Mock<IOrderPersister> _orderPersisterMock;

        public OrderStreamSyncSteps()
        {
            _salesChannelQueryMock = new Mock<ISalesChannelIdQuery>();
            _orderPersisterMock = new Mock<IOrderPersister>();
            _orderStreamSyncer = new OrderStreamSyncer(_salesChannelQueryMock.Object, _orderPersisterMock.Object);
        }

        [Given(@"we have this sales channel id (.*) for (.*)")]
        public void GivenWeHaveThisSalesChannelIdForSalesChannel(int salesChannelId, string salesChannel)
        {
            _salesChannelQueryMock.Setup(p => p.GetIdBy(salesChannel))
                .ReturnsAsync(salesChannelId);
        }
        
        [When(@"A new order message arrives into the message queue")]
        public async Task WhenANewOrderMessageArrivesIntoTheMessageQueue(Table table)
        {
            var orderMessage = table.CreateInstance<OrderMessage>();
            await _orderStreamSyncer.Sync(orderMessage);
        }
        
        [Then(@"this order should have been persisted in the database")]
        public void ThenThisOrderShouldHaveBeenPersistedInTheDatabase(Table table)
        {
            var backOfficeOrderExample = table.CreateInstance<BackOfficeOrderExample>();
            _orderPersisterMock.Verify(p => p.Persist(It.Is<BackOfficeOrder>(actualOrder => backOfficeOrderExample.Matches(actualOrder))));
        }
    }

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

    public class OrderMessage
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public string SalesChannel { get; set; }
        public decimal OrderAmount { get; set; }
    }

    public interface ISalesChannelIdQuery
    {
        Task<int> GetIdBy(string salesChannelDescription);
    }

    public interface IOrderPersister
    {
        Task Persist(BackOfficeOrder backOfficeOrder);
    }

    public class BackOfficeOrder
    {
        public string OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public int SalesChannelId { get; set; }
        public decimal OrderAmount { get; set; }
    }

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
