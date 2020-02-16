using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using TradesGroupStateCreator;
using TradesGroupStateCreator.Contracts;
using XmlSerializer.Contracts;

namespace UnitTests.TradesGroupStateCreator
{
    [TestClass]
    public class InputTradeProviderShould
    {
        private readonly IInputXmlFileWrapper _inputXmlFileWrapper;
        private readonly IXmlDocumentSerializer _xmlDocumentSerializer;
        private readonly IInputTradesProvider _inputTradesProvider;

        public InputTradeProviderShould()
        {
            _inputXmlFileWrapper = Substitute.For<IInputXmlFileWrapper>();
            _xmlDocumentSerializer = Substitute.For<IXmlDocumentSerializer>();
            _inputTradesProvider = new InputTradesProvider(_inputXmlFileWrapper, _xmlDocumentSerializer);
        }

        [TestMethod]
        public void ReturnEmptyInputTrades()
        {
            var listInputTrades = new List<InputTradeModel>();
            _xmlDocumentSerializer.Deserialize<InputTradeModel>(Arg.Any<string>()).Returns(listInputTrades);
            var result = _inputTradesProvider.GetInputTrades();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(List<InputTradeModel>));
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(0));
        }

        [TestMethod]
        public void ReturnInputTrades()
        {
            var listInputTrades = new List<InputTradeModel>
            {
                new InputTradeModel(),
                new InputTradeModel()
            };
            _xmlDocumentSerializer.Deserialize<InputTradeModel>(Arg.Any<string>()).Returns(listInputTrades);
            var result = _inputTradesProvider.GetInputTrades();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(List<InputTradeModel>));
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
