using System.Linq;

namespace TradesGroupStateCreator.Contracts
{
    public interface ITradesGroupReportGenerator
    {
        /// <summary>
        /// Generate Csv Report
        /// </summary>
        /// <param name="tradesGroups"></param>
        void GenerateCsvReport(IOrderedEnumerable<TradesGroupModel> tradesGroups);
    }
}
