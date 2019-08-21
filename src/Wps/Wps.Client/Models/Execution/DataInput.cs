using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Services;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("Input", Namespace = ModelNamespaces.Wps)]
    public class DataInput : IXmlSerializable
    {

        public string MimeType { get; set; }
        public string Encoding { get; set; }
        public string Schema { get; set; }

        public string Identifier { get; set; }
        public object Data { get; set; }
        public ResourceReference Reference { get; set; }
        public DataInput[] Inputs { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            var xmlSerializer = new XmlSerializationService();

            writer.WriteAttributeString("id", Identifier);

            if (Data != null)
            {
                writer.WriteStartElement("wps", "Data", ModelNamespaces.Wps);
                if (!string.IsNullOrEmpty(MimeType)) writer.WriteAttributeString("mimeType", MimeType);
                if (!string.IsNullOrEmpty(Encoding)) writer.WriteAttributeString("encoding", Encoding);
                if (!string.IsNullOrEmpty(Schema)) writer.WriteAttributeString("schema", Schema);
                writer.WriteRaw(xmlSerializer.Serialize(Data, true));
                writer.WriteEndElement();
            }

            if (Reference != null)
            {
                writer.WriteRaw(xmlSerializer.Serialize(Reference, true));
            }

            if (Inputs != null)
            {
                foreach (var input in Inputs)
                {
                    writer.WriteRaw(xmlSerializer.Serialize(input, true));
                }
            }
        }
    }
}
