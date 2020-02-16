using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfigurationHelper.Contracts;
using TradesGroupStateCreator.Contracts;
using NSubstitute;
using TradesGroupStateCreator;
using NUnit.Framework;

namespace UnitTests.TradesGroupStateCreator
{
    [TestClass]
    public class InputXmlFileWrapperShould
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;
        private readonly IInputXmlFileWrapper _inputXmlFileWrapperShould;

        public InputXmlFileWrapperShould()
        {
            _configurationDataProvider = Substitute.For<IConfigurationDataProvider>();
            _inputXmlFileWrapperShould = new InputXmlFileWrapper(_configurationDataProvider);
        }

        [TestMethod]
        public void ReturnInputXmlFilePath()
        {
            var path = "c:\\temp";
            _configurationDataProvider.GetValueFromSectionAndKey(Arg.Any<string>(), Arg.Any<string>()).Returns(path);
            var result = _inputXmlFileWrapperShould.GetInputXmlFilePath();
            NUnit.Framework.Assert.That(result, Is.EqualTo(path));
        }
    }
}
