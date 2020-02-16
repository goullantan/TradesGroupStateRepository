using log4net;
using System.IO;
using System.Reflection;
using XmlSerializer.Contracts;

namespace XmlSerializer
{
    public class XmlDocumentReader : IXmlDocumentReader
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Read All Text
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public string ReadAllText(string xmlFilePath)
        {
            var data = string.Empty;

            try
            {
                Logger.Info($"Reading file : {xmlFilePath}");
                data = File.ReadAllText(xmlFilePath);
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            return data;
        }
    }
}
