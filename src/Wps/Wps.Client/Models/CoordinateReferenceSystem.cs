using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("CRS", Namespace = ModelNamespaces.Wps)]
    public class CoordinateReferenceSystem : IXmlSerializable
    {

        public string Uri { get; set; }

        public bool IsDefault { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        // This might actually break the XML deserializer of other elements using C# attributes. Careful when reading! Should read the subtree.
        public void ReadXml(XmlReader baseReader)
        {
            IsDefault = bool.TryParse(baseReader.GetAttribute("default"), out var isDefault) && isDefault;
            Uri = baseReader.ReadElementContentAsString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("default", IsDefault.ToString().ToLower());
            writer.WriteRaw(Uri);
        }
    }
}
