using ConfigurationHelper.Contracts;
using log4net;
using System;
using System.Reflection;
using TradesGroupStateCreator.Contracts;

namespace TradesGroupStateCreator
{
    public class InputXmlFileWrapper : IInputXmlFileWrapper
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IConfigurationDataProvider _configurationDataProvider;

        public InputXmlFileWrapper(IConfigurationDataProvider configurationDataProvider)
        {
            _configurationDataProvider = configurationDataProvider;
        }

        /// <summary>
        /// Get InputXml File Path
        /// </summary>
        /// <returns></returns>
        public string GetInputXmlFilePath()
        {
            var path = string.Empty;

            try
            {
                path = _configurationDataProvider.GetValueFromSectionAndKey("Parameters", "InputFilePath");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            return path;
        }
    }
}
