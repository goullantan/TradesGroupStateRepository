using ConfigurationHelper;
using ConfigurationHelper.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace UnitTests.ConfigurationHelper
{
    [TestClass]
    public class ConfigurationDataProviderShould
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;

        public ConfigurationDataProviderShould()
        {
            _configurationDataProvider = new ConfigurationDataProvider();       
        }

        [TestMethod]
        public void ReturnValueFromSectionAndKey()//from App.config
        {
            var result = _configurationDataProvider.GetValueFromSectionAndKey("UnitTests", "Test");
            NUnit.Framework.Assert.That(result, Is.EqualTo("OK"));
        }
    }
}
