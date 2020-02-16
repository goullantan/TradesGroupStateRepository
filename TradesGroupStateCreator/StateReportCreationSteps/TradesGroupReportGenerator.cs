using CsvFileManager.Contracts;
using log4net;
using System.Linq;
using System.Reflection;
using System.Text;
using TradesGroupStateCreator.Contracts;

namespace TradesGroupStateCreator
{
    public class TradesGroupReportGenerator : ITradesGroupReportGenerator
    {
        private const string Header = "CorrelationID;NumberOfTrades;State";

        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IOutputCsvFileWrapper _outputCsvFileWrapper;
        private readonly ICsvFileCreator _csvFileCreator;

        public TradesGroupReportGenerator(IOutputCsvFileWrapper outputCsvFileWrapper, ICsvFileCreator csvFileCreator)
        {
            _outputCsvFileWrapper = outputCsvFileWrapper;
            _csvFileCreator = csvFileCreator;
        }

        /// <summary>
        /// Generate Csv Report
        /// </summary>
        /// <param name="tradesGroups"></param>
        public void GenerateCsvReport(IOrderedEnumerable<TradesGroupModel> tradesGroups)
        {
            Logger.Info("Generating report...");

            var sb = new StringBuilder();

            try
            {
                sb.AppendLine(Header);//headers

                foreach (var tradesGroup in tradesGroups)//build report data
                {
                    sb.AppendLine($"{tradesGroup.CorrelationId};{tradesGroup.NumberOfTrades};{tradesGroup.State}");
                }

                var outputPath = _outputCsvFileWrapper.GetOutputCsvFilePath();//output path

                _csvFileCreator.CreateCsvFile(outputPath, sb.ToString());//create csv file
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            Logger.Info("Report generated.");
        }
    }
}