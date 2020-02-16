using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TradesGroupStateCreator.Contracts;

namespace TradesGroupStateCreator
{
    public class TradesAggregator : ITradesAggregator
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Aggregate Input Trades
        /// </summary>
        /// <param name="inputTrades"></param>
        /// <returns></returns>
        public IEnumerable<TradeModel> AggregateInputTrades(List<InputTradeModel> inputTrades)
        {
            Logger.Info("Aggregating input trades...");

            var aggregatedTrades = new List<TradeModel>();

            try
            {
                aggregatedTrades = inputTrades
                    .GroupBy(t => new { t.CorrelationId, t.NumberOfTrades, t.Limit })
                    .Select(grp => new TradeModel
                    {
                        CorrelationId = grp.Key.CorrelationId,
                        NumberOfTrades = grp.Key.NumberOfTrades,
                        Count = grp.Count(),
                        Limit = grp.Key.Limit,
                        Value = grp.Sum(t => t.Value)
                    }).ToList();
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            Logger.Info($"{aggregatedTrades.Count} trades aggregated by CorrelationId got.");

            return aggregatedTrades;
        }
    }
}
