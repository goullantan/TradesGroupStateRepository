namespace TradesGroupStateCreator
{
    public class InputTradeModel
    {
        public string CorrelationId { get; set; }
        public double NumberOfTrades { get; set; }
        public double Limit { get; set; }
        public double Value { get; set; }
        public string TradeId { get; set; }

        public override string ToString()
        {
            return $"CorrelationId: {CorrelationId} - NumberOfTrades: {NumberOfTrades} - Limit: {Limit} - Value: {Value} - TradeId: {TradeId}";
        }
    }
}
