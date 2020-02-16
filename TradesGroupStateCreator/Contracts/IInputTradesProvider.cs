using System.Collections.Generic;

namespace TradesGroupStateCreator.Contracts
{
    public interface IInputTradesProvider
    {
        /// <summary>
        /// Get Input Trades
        /// </summary>
        /// <returns></returns>
        List<InputTradeModel> GetInputTrades();
    }
}
