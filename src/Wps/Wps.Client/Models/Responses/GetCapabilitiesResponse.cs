using System.Xml.Serialization;
using Wps.Client.Models.Ows;
using Wps.Client.Utils;

namespace Wps.Client.Models.Responses
{
    /// <summary>
    /// The response received from the WPS server.
    /// </summary>
    [XmlRoot("Capabilities", Namespace = ModelNamespaces.Wps)]
    public class GetCapabilitiesResponse
    {

        /// <summary>
        /// The type of service offered by the server.
        /// </summary>
        [XmlAttribute("service", Namespace = ModelNamespaces.Wps)]
        public string Service { get; set; }

        /// <summary>
        /// The version of the service.
        /// </summary>
        [XmlAttribute("version", Namespace = ModelNamespaces.Wps)]
        public string Version { get; set; }

        /// <summary>
        /// General metadata for a specific server.
        /// </summary>
        [XmlElement("ServiceIdentification", Namespace = ModelNamespaces.Ows)]
        public ServiceIdentification ServiceIdentification { get; set; }

        /// <summary>
        /// Information about the service provider.
        /// </summary>
        [XmlElement("ServiceProvider", Namespace = ModelNamespaces.Ows)]
        public ServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Metadata about the operations and related abilities specified by this service and implemented by this server.
        /// </summary>
        [XmlElement("OperationsMetadata", Namespace = ModelNamespaces.Ows)]
        public OperationsMetadata OperationsMetadata { get; set; }

        /// <summary>
        /// The languages supported by this server.
        /// </summary>
        [XmlArray("Languages", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Language", Namespace = ModelNamespaces.Ows, Type = typeof(string))]
        public string[] Languages { get; set; }

        /// <summary>
        /// The summary of the offered processes by the server.
        /// </summary>
        [XmlArray("Contents", Namespace = ModelNamespaces.Wps)]
        [XmlArrayItem("ProcessSummary", Namespace = ModelNamespaces.Wps)]
        public ProcessSummary[] ProcessSummaries { get; set; }

    }
}
