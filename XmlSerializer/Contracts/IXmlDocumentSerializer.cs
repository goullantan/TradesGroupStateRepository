using System.Collections.Generic;

namespace XmlSerializer.Contracts
{
    public interface IXmlDocumentSerializer
    {
        /// <summary>
        /// Deserialize Xml document by returning List of T element 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        List<T> Deserialize<T>(string xmlFilePath);
    }  
}
