using ConfigurationHelper.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using TradesGroupStateCreator;
using TradesGroupStateCreator.Contracts;

namespace UnitTests.TradesGroupStateCreator
{
    [TestClass]
    public class OutputCsvFileWrapperShould
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;
        private readonly IOutputCsvFileWrapper _outputCsvFileWrapper;

        public OutputCsvFileWrapperShould()
        {
            _configurationDataProvider = Substitute.For<IConfigurationDataProvider>();
            _outputCsvFileWrapper = new OutputCsvFileWrapper(_configurationDataProvider);
        }

        [TestMethod]
        public void ReturnOutputCsfFilePath()
        {
            var path = "c:\\temp";
            _configurationDataProvider.GetValueFromSectionAndKey(Arg.Any<string>(), Arg.Any<string>()).Returns(path);
            var result = _outputCsvFileWrapper.GetOutputCsvFilePath();
            NUnit.Framework.Assert.That(result, Is.EqualTo(path));
        }
    }
}
