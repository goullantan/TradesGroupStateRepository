using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TradesGroupStateCreator.Contracts;
using static TradesGroupStateCreator.TradeStateModel;

namespace TradesGroupStateCreator
{
    public class TradesGroupStateAttributor : ITradesGroupStateAttributor
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Attribute state according to trade parameters
        /// </summary>
        /// <param name="trades"></param>
        /// <returns></returns>
        public IOrderedEnumerable<TradesGroupModel> AttributeState(IEnumerable<TradeModel> trades)
        {
            Logger.Info("Attributing state...");

            var tradesWithStatus = new List<TradesGroupModel>();

            try
            {
                tradesWithStatus = trades.Select(t => new TradesGroupModel
                {
                    CorrelationId = t.CorrelationId,
                    NumberOfTrades = t.NumberOfTrades,
                    State = GetTradeState(t)
                }).ToList();
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            Logger.Info($"{tradesWithStatus.Count} trades group with state got.");

            return tradesWithStatus.OrderBy(t => t.CorrelationId);//order by correlation id
        }

        private static EnumTradeState GetTradeState(TradeModel t)
        {
            if (t.Count < t.NumberOfTrades) return EnumTradeState.Pending;//incomplete

            if (t.Value <= t.Limit && t.NumberOfTrades == t.Count)
            {
                return EnumTradeState.Accepted;//value under the limit and all trades are present
            }

            return EnumTradeState.Rejected;//value above limit     
        }
    }
}
