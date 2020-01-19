using System.Collections.Generic;
using System.Linq;
using Shouldly;
using TechTalk.SpecFlow;

namespace Outside.OrderStreamSync.Specs
{
    [Binding]
    public class CalculatorSteps
    {
        private readonly Calculator _calculator;
        private int _addResult;

        public CalculatorSteps()
        {
            _calculator = new Calculator();
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            _calculator.Enter(number);
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _addResult = _calculator.Add();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expected)
        {
            _addResult.ShouldBe(expected);
        }
    }

    public class Calculator
    {
        private readonly List<int> _enteredNumbers = new List<int>();

        public void Enter(in int number)
        {
            _enteredNumbers.Add(number);
        }

        public int Add()
        {
            return _enteredNumbers.Sum();
        }
    }
}
