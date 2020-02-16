using System.Collections.Generic;

namespace TradesGroupStateCreator.Contracts
{
    public interface ITradesAggregator
    {
        /// <summary>
        /// Aggregate Input Trades
        /// </summary>
        /// <param name="inputTrades"></param>
        /// <returns></returns>
        IEnumerable<TradeModel> AggregateInputTrades(List<InputTradeModel> inputTrades);
    }
}
