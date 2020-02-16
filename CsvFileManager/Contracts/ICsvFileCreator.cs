namespace CsvFileManager.Contracts
{
    public interface ICsvFileCreator
    {
        /// <summary>
        /// Create Csv File
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="data"></param>
        void CreateCsvFile(string outputPath, string data);
    }
}
