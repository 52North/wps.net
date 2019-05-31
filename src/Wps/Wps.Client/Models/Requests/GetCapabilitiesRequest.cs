using System;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    [Serializable]
    [XmlRoot("GetCapabilities", Namespace = ModelNamespaces.Wps)]
    public class GetCapabilitiesRequest
    {
        /// <summary>
        /// The concerned service by the GetCapabilities request.
        /// </summary>
        [XmlAttribute("service", Namespace = ModelNamespaces.Wps)]
        public string Service { get; set; } = "WPS";
    }
}