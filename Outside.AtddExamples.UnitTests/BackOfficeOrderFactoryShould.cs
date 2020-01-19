using Outside.OrderStreamSync;
using Shouldly;
using Xunit;

namespace Outside.AtddExamples.UnitTests
{
    public class BackOfficeOrderFactoryShould
    {
        private BackOfficeOrderFactory _factory;

        public BackOfficeOrderFactoryShould()
        {
            _factory = new BackOfficeOrderFactory();
        }

        [Theory]
        [InlineData("test@test.com", 9, "xxxttt", 900)]
        public void InstantiateOrder(string email, int salesChannelId, string orderId, int orderAmount)
        {
            var orderMessage = new OrderMessage()
            {
                CustomerEmail = email,
                OrderAmount = orderAmount,
                OrderId = orderId
            };

            var backOfficeOrder = _factory.InstantiateFrom(orderMessage, salesChannelId);

            backOfficeOrder.CustomerEmail.ShouldBe(email);
            backOfficeOrder.SalesChannelId.ShouldBe(salesChannelId);
            backOfficeOrder.OrderId.ShouldBe(orderId);
            backOfficeOrder.OrderAmount.ShouldBe(orderAmount);
        }
    }
}
