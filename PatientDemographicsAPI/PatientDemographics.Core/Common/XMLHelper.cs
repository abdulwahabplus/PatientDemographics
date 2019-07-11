using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PatientDemographics.Core.Common
{
    public class XMLHelper
    {
        public string CreateXML(Object objectToXml)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(objectToXml.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, objectToXml);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public Object CreateObject(string XMLString, Object xmlToObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(xmlToObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            xmlToObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return xmlToObject;
        }
    }
}
