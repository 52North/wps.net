using System.Xml.Serialization;

namespace Wps.Client.Models.Execution
{
    public enum ResponseType
    {

        [XmlEnum("raw")] Raw,
        [XmlEnum("document")] Document

    }
}
