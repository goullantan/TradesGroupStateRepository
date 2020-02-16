using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using TradesGroupStateCreator.Contracts;
using XmlSerializer.Contracts;

namespace TradesGroupStateCreator
{
    public class InputTradesProvider : IInputTradesProvider
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IInputXmlFileWrapper _inputXmlFileWrapper;
        private readonly IXmlDocumentSerializer _xmlDocumentSerializer;

        public InputTradesProvider(IInputXmlFileWrapper inputXmlFileWrapper, IXmlDocumentSerializer xmlDocumentSerializer)
        {
            _inputXmlFileWrapper = inputXmlFileWrapper;
            _xmlDocumentSerializer = xmlDocumentSerializer;
        }

        /// <summary>
        /// Get Input Trades
        /// </summary>
        /// <returns></returns>
        public List<InputTradeModel> GetInputTrades()
        {
            Logger.Info("Getting input trades from xml file...");
            var inputTrades = new List<InputTradeModel>();
            try
            {
                var xmlFilePath = _inputXmlFileWrapper.GetInputXmlFilePath();
                inputTrades = _xmlDocumentSerializer.Deserialize<InputTradeModel>(xmlFilePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
            Logger.Info($"{inputTrades.Count} input trades got.");
            return inputTrades;
        }
    }
}
