using System.Collections;
using System.Configuration;
using System.Reflection;
using ConfigurationHelper.Contracts;
using log4net;

namespace ConfigurationHelper
{
    public class ConfigurationDataProvider : IConfigurationDataProvider
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// GetValueFromKey
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValueFromSectionAndKey(string sectionName, string key)
        {
            try
            {
                Hashtable section = (Hashtable)ConfigurationManager.GetSection(sectionName);
                return section[key].ToString();
            }
            catch (System.Exception ex)
            {
                Logger.Error($"Unknown config section or key : {ex.Message}", ex);
                throw;
            }
        }
    }
}
