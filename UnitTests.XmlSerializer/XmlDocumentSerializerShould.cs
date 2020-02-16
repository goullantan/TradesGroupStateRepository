using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using TradesGroupStateCreator;
using XmlSerializer;
using XmlSerializer.Contracts;

namespace UnitTests.XmlSerializer
{
    [TestClass]
    public class XmlDocumentSerializerShould
    {
        private readonly IXmlDocumentReader _xmlDocumentReader;
        private readonly IXmlDocumentSerializer _xmlDocumentSerializer;

        public XmlDocumentSerializerShould()
        {
            _xmlDocumentReader = Substitute.For<IXmlDocumentReader>();
            _xmlDocumentSerializer = new XmlDocumentSerializer(_xmlDocumentReader);       
        }

        [TestMethod]
        public void DeserializeOneElement()
        {
            var xmlData = new StringBuilder();//Substitute input xml data
            xmlData.Append("<Trades>");
            xmlData.Append("<Trade TradeID=\"654\" Limit=\"1000\" NumberOfTrades=\"3\" CorrelationId=\"234\">100</Trade>");
            xmlData.Append("</Trades>");
            var xmlDataString = xmlData.ToString();

            _xmlDocumentReader.ReadAllText(Arg.Any<string>()).ReturnsForAnyArgs(xmlDataString);

            var result = _xmlDocumentSerializer.Deserialize<InputTradeModel>(string.Empty);//path is not needed

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(List<InputTradeModel>));
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(1));
            NUnit.Framework.Assert.That(result.First().TradeId, Is.EqualTo("654"));//trade id is string
            NUnit.Framework.Assert.That(result.First().Limit, Is.EqualTo(1000));
            NUnit.Framework.Assert.That(result.First().NumberOfTrades, Is.EqualTo(3));
            NUnit.Framework.Assert.That(result.First().CorrelationId, Is.EqualTo("234"));//correlation id is string
            NUnit.Framework.Assert.That(result.First().Value, Is.EqualTo(100));
        }

        [TestMethod]
        public void DeserializeSeveralElements()
        {
            var xmlData = new StringBuilder();//Substitute input xml data
            xmlData.Append("<Trades>");
            xmlData.Append("<Trade TradeID=\"654\" Limit=\"1000\" NumberOfTrades=\"3\" CorrelationId=\"234\">100</Trade>");
            xmlData.Append("<Trade TradeID=\"423\" Limit=\"500\" NumberOfTrades=\"1\" CorrelationId=\"222\">600</Trade>");
            xmlData.Append("</Trades>");
            var xmlDataString = xmlData.ToString();

            _xmlDocumentReader.ReadAllText(Arg.Any<string>()).ReturnsForAnyArgs(xmlDataString);

            var result = _xmlDocumentSerializer.Deserialize<InputTradeModel>(string.Empty);//path is not needed

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(List<InputTradeModel>));
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
            //First Deserialized Element
            NUnit.Framework.Assert.That(result.First().TradeId, Is.EqualTo("654"));//trade id is string
            NUnit.Framework.Assert.That(result.First().Limit, Is.EqualTo(1000));
            NUnit.Framework.Assert.That(result.First().NumberOfTrades, Is.EqualTo(3));
            NUnit.Framework.Assert.That(result.First().CorrelationId, Is.EqualTo("234"));//correlation id is string
            NUnit.Framework.Assert.That(result.First().Value, Is.EqualTo(100));
            //Second Deserialized Element
            NUnit.Framework.Assert.That(result[1].TradeId, Is.EqualTo("423"));//trade id is string
            NUnit.Framework.Assert.That(result[1].Limit, Is.EqualTo(500));
            NUnit.Framework.Assert.That(result[1].NumberOfTrades, Is.EqualTo(1));
            NUnit.Framework.Assert.That(result[1].CorrelationId, Is.EqualTo("222"));//correlation id is string
            NUnit.Framework.Assert.That(result[1].Value, Is.EqualTo(600));
        }
    }
}
