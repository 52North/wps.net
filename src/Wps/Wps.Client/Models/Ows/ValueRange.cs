using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("Range", Namespace = ModelNamespaces.Ows)]
    public class ValueRange
    {

        [XmlAttribute("rangeClosure", Namespace = ModelNamespaces.Ows)]
        public RangeClosure RangeClosure { get; set; }

        [XmlElement("MinimumValue", Namespace = ModelNamespaces.Ows)]
        public string MinimumValue { get; set; }

        [XmlElement("MaximumValue", Namespace = ModelNamespaces.Ows)]
        public string MaximumValue { get; set; }

        [XmlElement("Spacing", Namespace = ModelNamespaces.Ows)]
        public string Spacing { get; set; }

    }
}
