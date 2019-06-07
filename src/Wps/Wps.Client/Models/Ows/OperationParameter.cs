using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("Parameter", Namespace = ModelNamespaces.Ows)]
    public class OperationParameter
    {

        [XmlAttribute("name", Namespace = ModelNamespaces.Ows)]
        public string Name { get; set; }

        [XmlElement("Value", Namespace = ModelNamespaces.Ows)]
        public string[] Values { get; set; }

        // TODO: Add list of Metadata

    }
}
