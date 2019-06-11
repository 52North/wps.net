using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("ExceptionReport", Namespace = ModelNamespaces.Ows)]
    public class ExceptionReport
    {

        [XmlAttribute("version", Namespace = ModelNamespaces.Ows)]
        public string Version { get; set; }

        [XmlAttribute("lang", Namespace = ModelNamespaces.Ows)]
        public string Language { get; set; }

        [XmlElement("Exception", Namespace = ModelNamespaces.Ows)]
        public OwsException[] Exceptions { get; set; }

    }
}
