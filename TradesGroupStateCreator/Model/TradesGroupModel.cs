using static TradesGroupStateCreator.TradeStateModel;

namespace TradesGroupStateCreator
{
    public class TradesGroupModel
    {
        public string CorrelationId { get; set; }
        public double NumberOfTrades { get; set; }
        public EnumTradeState State { get; set; }

        public override string ToString()
        {
            return $"CorrelationId: {CorrelationId} - NumberOfTrades: {NumberOfTrades} - State: {State}";
        }
    }
}
