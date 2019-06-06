using System.Xml.Serialization;

namespace Wps.Client.Models
{
    public enum TransmissionMode
    {

        [XmlEnum("value")] Value,
        [XmlEnum("reference")] Reference,
        [XmlEnum("value reference")] ValueReference,

    }
}
