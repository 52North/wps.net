using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Services;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("Exception", Namespace = ModelNamespaces.Ows)]
    public class OwsException : Exception, IXmlSerializable
    {

        //[XmlAttribute("exceptionCode", Namespace = ModelNamespaces.Ows)]
        public string Code { get; set; }

        //[XmlAttribute("locator", Namespace = ModelNamespaces.Ows)]
        public string Locator { get; set; }

        private string _message;

        public override string Message => _message;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var serializer = new XmlSerializationService();
            Code = reader.GetAttribute("exceptionCode");
            Locator = reader.GetAttribute("locator");

            var subtreeReader = reader.ReadSubtree();
            subtreeReader.MoveToContent();
            while (subtreeReader.Read())
            {
                if (subtreeReader.NodeType == XmlNodeType.Element)
                {
                    if (subtreeReader.LocalName.Equals("ExceptionText"))
                    {
                        _message = subtreeReader.ReadElementContentAsString();
                    }
                }
            }

            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
