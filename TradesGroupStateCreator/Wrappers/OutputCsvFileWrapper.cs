using ConfigurationHelper.Contracts;
using log4net;
using System;
using System.Reflection;
using TradesGroupStateCreator.Contracts;

namespace TradesGroupStateCreator
{
    public class OutputCsvFileWrapper : IOutputCsvFileWrapper
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IConfigurationDataProvider _configurationDataProvider;

        public OutputCsvFileWrapper(IConfigurationDataProvider configurationDataProvider)
        {
            _configurationDataProvider = configurationDataProvider;
        }

        /// <summary>
        /// Get Output Csv File Path
        /// </summary>
        /// <returns></returns>
        public string GetOutputCsvFilePath()
        {
            var path = string.Empty;

            try
            {
                path = _configurationDataProvider.GetValueFromSectionAndKey("Parameters", "OutputFilePath");
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
