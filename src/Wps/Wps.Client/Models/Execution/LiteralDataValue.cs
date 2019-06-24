using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("LiteralValue", Namespace = ModelNamespaces.Wps)]
    public class LiteralDataValue : IXmlSerializable
    {

        public string Value { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            Value = reader.ReadElementContentAsString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteRaw(Value);
        }
    }

}
