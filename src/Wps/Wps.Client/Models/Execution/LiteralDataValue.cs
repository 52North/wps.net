using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("LiteralValue", Namespace = ModelNamespaces.Wps)]
    public class LiteralDataValue : IXmlSerializable
    {

        public object Value { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteRaw(Value.ToString());
        }
    }
}
