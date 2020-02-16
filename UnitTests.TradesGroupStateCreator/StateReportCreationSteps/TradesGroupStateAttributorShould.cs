using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TradesGroupStateCreator;
using TradesGroupStateCreator.Contracts;
using static TradesGroupStateCreator.TradeStateModel;

namespace UnitTests.TradesGroupStateCreator
{
    [TestClass]
    public class TradesGroupStateAttributorShould
    {
        private readonly ITradesGroupStateAttributor _tradesGroupStateAttributor;

        public TradesGroupStateAttributorShould()
        {
            _tradesGroupStateAttributor = new TradesGroupStateAttributor();
        }

        [TestMethod]
        public void AttributeAcceptedState()
        {
            var trades = new List<TradeModel>
            {
                new TradeModel
                {
                    CorrelationId = "234",
                    Limit = 1000,
                    Value = 500,
                    NumberOfTrades = 3,
                    Count = 3
                }
            };
            var result = _tradesGroupStateAttributor.AttributeState(trades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<TradesGroupModel>));
            NUnit.Framework.Assert.That(result.ToList().First().State, Is.EqualTo(EnumTradeState.Accepted));
        }

        [TestMethod]
        public void AttributePendingState()
        {
            var trades = new List<TradeModel>
            {
                new TradeModel
                {
                    CorrelationId = "200",
                    Limit = 1000,
                    Value = 645,
                    NumberOfTrades = 2,
                    Count = 1
                }
            };
            var result = _tradesGroupStateAttributor.AttributeState(trades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<TradesGroupModel>));
            NUnit.Framework.Assert.That(result.ToList().First().State, Is.EqualTo(EnumTradeState.Pending));
        }

        [TestMethod]
        public void AttributeRejectedState()
        {
            var trades = new List<TradeModel>
            {
                new TradeModel
                {
                    CorrelationId = "222",
                    Limit = 500,
                    Value = 600,
                    NumberOfTrades = 1,
                    Count = 1
                }
            };
            var result = _tradesGroupStateAttributor.AttributeState(trades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<TradesGroupModel>));
            NUnit.Framework.Assert.That(result.ToList().First().State, Is.EqualTo(EnumTradeState.Rejected));
        }

        [TestMethod]
        public void ReturnOrderedEnumerableByCorrelationId()
        {
            var trades = new List<TradeModel>
            {
                new TradeModel
                {
                    CorrelationId = "222"
                },
                new TradeModel
                {
                    CorrelationId = "234"
                },
                new TradeModel
                {
                    CorrelationId = "200"
                }
            };
            var result = _tradesGroupStateAttributor.AttributeState(trades);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<TradesGroupModel>));
            NUnit.Framework.Assert.That(result.ToList(), Is.Ordered.By("CorrelationId"));
        }
    }
}
