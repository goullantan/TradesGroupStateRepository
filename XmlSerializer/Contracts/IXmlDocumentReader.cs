namespace XmlSerializer.Contracts
{
    public interface IXmlDocumentReader
    {
        /// <summary>
        /// Read All Text
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        string ReadAllText(string xmlFilePath);
    }
}
