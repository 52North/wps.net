using System;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    [Serializable]
    [XmlRoot("GetCapabilities", Namespace = ModelNamespaces.Wps)]
    public class GetCapabilitiesRequest : Request
    {

        [XmlAttribute("updateSequence", Namespace = ModelNamespaces.Ows)]
        public string UpdateSequence { get; set; }

        [XmlArray("AcceptVersions", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Version", Namespace = ModelNamespaces.Ows)]
        public string[] AcceptedVersions { get; set; } = {"2.0.0"};

        [XmlArray("Sections", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Section", Namespace = ModelNamespaces.Ows)]
        public string[] Sections { get; set; }

        [XmlArray("AcceptFormats", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("OutputFormat", Namespace = ModelNamespaces.Ows)]
        public string[] AcceptedFormats { get; set; }

        [XmlAttribute("service", Namespace = ModelNamespaces.Wps)]
        public string Service { get; set; } = "WPS";

    }
}