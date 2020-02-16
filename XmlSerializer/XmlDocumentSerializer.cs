using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using XmlSerializer.Contracts;

namespace XmlSerializer
{
    public class XmlDocumentSerializer : IXmlDocumentSerializer
    {
        private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IXmlDocumentReader _xmlDocumentReader;

        public XmlDocumentSerializer(IXmlDocumentReader xmlDocumentReader)
        {
            _xmlDocumentReader = xmlDocumentReader;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public List<T> Deserialize<T>(string xmlFilePath)
        {
            var result = new List<T>();

            try
            {
                var xmlInputData = _xmlDocumentReader.ReadAllText(xmlFilePath);

                var doc = new XmlDocument();//class used to parse xml document

                doc.LoadXml(xmlInputData);

                var root = doc.FirstChild;//only one root element is permitted in xml structure

                if (root.HasChildNodes)//root element's childs
                {
                    BuildListOfElementFromXmlData(result, root.ChildNodes);
                }
                else//empty or corrupted xml document
                {
                    Logger.Info("Root without child nodes. Please check the xml document format.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            return result;
        }

        private void BuildListOfElementFromXmlData<T>(List<T> result, XmlNodeList xmlNodes)
        {
            try
            {
                foreach (XmlNode childNode in xmlNodes)
                {
                    //Getting Xml doc structure
                    var childElement = (XmlElement)childNode;
                    var dicoChildElementAttributes = FillDicoChildElementAttributes(childElement);//Attributes of child elements
                    var dicoChildElementChildNodes = FillDicoChildElementChildNodes(childElement);//ChildNodes of child elements

                    var element = (T)Activator.CreateInstance(typeof(T));
                    var properties = typeof(T).GetProperties();//Get properties of type T 

                    //Set Properties Values by finding equivalent in Xml data
                    foreach (var property in properties)
                    {
                        var lowerPropertyName = property.Name.ToLower();//avoid case sensitivity issues

                        var value = string.Empty;
                        if (dicoChildElementAttributes.ContainsKey(lowerPropertyName))//Attribute property
                        {
                            value = dicoChildElementAttributes[lowerPropertyName];
                        }
                        else if (dicoChildElementChildNodes.ContainsKey(lowerPropertyName))//ChildNode property
                        {
                            value = dicoChildElementChildNodes[lowerPropertyName];
                        }
                        else
                        {
                            Logger.Warn($"Property name {property.Name} not found in the xml document.");
                            continue;
                        }

                        var convertedValue = Convert.ChangeType(value, property.PropertyType);//good property type
                        element.GetType().GetProperty(property.Name).SetValue(element, convertedValue);
                    }

                    result.Add(element);
                }
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message, ex);
                throw;
            }
        }

        private Dictionary<string, string> FillDicoChildElementChildNodes(XmlElement childElement)
        {
            var dicoChildNodes = new Dictionary<string, string>();

            try
            {
                foreach (XmlNode childNode in childElement.ChildNodes)
                {
                    var childNodeName = childNode.Name == "#text"//no name given to the xmlNode
                        ? "value"//default name
                        : childNode.Name.ToLower();//avoid case sensitivity issues

                    if (!dicoChildNodes.ContainsKey(childNodeName))
                    {
                        dicoChildNodes.Add(childNodeName, childNode.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            return dicoChildNodes;
        }

        private Dictionary<string, string> FillDicoChildElementAttributes(XmlElement childElement)
        {
            var dicoAttributes = new Dictionary<string, string>();

            try
            {
                foreach (XmlAttribute attribute in childElement.Attributes)
                {
                    var attributeName = attribute.Name.ToLower();//avoid case sensitivity issues

                    if (!dicoAttributes.ContainsKey(attributeName))
                    {
                        dicoAttributes.Add(attributeName, attribute.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

            return dicoAttributes;
        }
    }
}
