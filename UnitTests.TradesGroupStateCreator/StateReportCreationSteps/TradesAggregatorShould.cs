using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TradesGroupStateCreator;
using TradesGroupStateCreator.Contracts;

namespace UnitTests.TradesGroupStateCreator
{
    [TestClass]
    public class TradesAggregatorShould
    {
        private readonly ITradesAggregator _tradesAggregator;

        public TradesAggregatorShould()
        {
            _tradesAggregator = new TradesAggregator();
        }

        [TestMethod]
        public void AggregateEmptyInputTrades()
        {
            var inputTrades = new List<InputTradeModel>();
            var result = _tradesAggregator.AggregateInputTrades(inputTrades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IEnumerable<TradeModel>));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.LessThanOrEqualTo(inputTrades.Count));
        }

        [TestMethod]
        public void AggregateInputTradesByCorrelationId()
        {
            var inputTrades = new List<InputTradeModel>
            {
                new InputTradeModel{CorrelationId="234"},
                new InputTradeModel{CorrelationId="234"},
                new InputTradeModel{CorrelationId="222"},
                new InputTradeModel{CorrelationId="234"},
                new InputTradeModel{CorrelationId="200"}
            };
            var result = _tradesAggregator.AggregateInputTrades(inputTrades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IEnumerable<TradeModel>));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.LessThanOrEqualTo(inputTrades.Count));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.EqualTo(3));
        }

        [TestMethod]
        public void SetTradeModelCountProperty()
        {
            var inputTrades = new List<InputTradeModel>
            {
                new InputTradeModel{CorrelationId="234"},
                new InputTradeModel{CorrelationId="234"}
            };
            var result = _tradesAggregator.AggregateInputTrades(inputTrades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IEnumerable<TradeModel>));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.LessThanOrEqualTo(inputTrades.Count));
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(1));//same CorrelationId
            NUnit.Framework.Assert.That(result.First().Count, Is.EqualTo(2));//count nb trades aggregated
        }

        [TestMethod]
        public void SetTradeModelValueProperty()
        {
            var inputTrades = new List<InputTradeModel>
            {
                new InputTradeModel{CorrelationId="234", Value=200},
                new InputTradeModel{CorrelationId="234", Value=100},
            };
            var result = _tradesAggregator.AggregateInputTrades(inputTrades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IEnumerable<TradeModel>));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.LessThanOrEqualTo(inputTrades.Count));
            NUnit.Framework.Assert.That(result.ToList().Count, Is.EqualTo(1));
            NUnit.Framework.Assert.That(result.First().Value, Is.EqualTo(300));//sum of value property
        }
    }
}
