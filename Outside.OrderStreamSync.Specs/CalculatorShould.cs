using Shouldly;
using Xunit;

namespace Outside.OrderStreamSync.Specs
{
    public class CalculatorShould
    {
        private readonly Calculator _calculator;

        public CalculatorShould()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void AddTwoNumbers()
        {
            _calculator.Enter(50);
            _calculator.Enter(70);

            var result = _calculator.Add();

            result.ShouldBe(120);
        }
    }
}