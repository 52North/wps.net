using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("Reference", Namespace = ModelNamespaces.Wps)]
    public class ResourceReference
    {

        [XmlAttribute("href", Namespace = ModelNamespaces.Xlink)]
        public string Href { get; set; }

        [XmlAttribute("schema")]
        public string Schema { get; set; }

    }
}
