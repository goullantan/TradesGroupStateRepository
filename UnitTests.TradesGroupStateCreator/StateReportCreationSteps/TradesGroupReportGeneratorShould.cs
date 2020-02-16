using CsvFileManager;
using CsvFileManager.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradesGroupStateCreator;
using TradesGroupStateCreator.Contracts;

namespace UnitTests.TradesGroupStateCreator.StateReportCreationSteps
{
    [TestClass]
    public class TradesGroupReportGeneratorShould
    {
        private readonly IOutputCsvFileWrapper _outputCsvFileWrapper;
        private readonly ICsvFileCreator _csvFileCreator;
        private readonly ITradesGroupReportGenerator _tradesGroupReportGenerator;

        public TradesGroupReportGeneratorShould()
        {
            _outputCsvFileWrapper = Substitute.For<IOutputCsvFileWrapper>();
            _csvFileCreator = new CsvFileCreator();//no need to test System.IO methods
            _tradesGroupReportGenerator = new TradesGroupReportGenerator(_outputCsvFileWrapper, _csvFileCreator);
        }

        [TestMethod]
        public void GenerateReport()
        {
            var path = @"C:\temp\TestResult.csv";//hard path just used for the test
            _outputCsvFileWrapper.GetOutputCsvFilePath().Returns(path);

            IEnumerable<TradesGroupModel> tradesGroupWithState = new List<TradesGroupModel>();
            _tradesGroupReportGenerator.GenerateCsvReport(tradesGroupWithState.OrderBy(t => t.CorrelationId));

            Assert.IsTrue(File.Exists(path));//file existence
            Assert.IsTrue(File.ReadAllText(path) == "CorrelationID;NumberOfTrades;State\r\n");//headers with carriage return
        }
    }
}
