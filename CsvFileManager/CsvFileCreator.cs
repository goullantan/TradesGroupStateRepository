using CsvFileManager.Contracts;
using log4net;
using System;
using System.IO;
using System.Reflection;

namespace CsvFileManager
{
    public class CsvFileCreator : ICsvFileCreator
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// CreateCsvFile
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="data"></param>
        public void CreateCsvFile(string outputPath, string data)
        {
            try
            {
                Logger.Info($"Creating csv file in the following path : {outputPath}");
                File.WriteAllText(outputPath, data);//Create csv file
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
