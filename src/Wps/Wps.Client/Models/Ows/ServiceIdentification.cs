using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    /// <summary>
    /// General metadata for a specific server.
    /// </summary>
    [XmlRoot("ServiceIdentification", Namespace = ModelNamespaces.Ows)]
    public class ServiceIdentification : DescriptiveObject
    {

        /// <summary>
        /// A service type name from a registry of services.
        /// </summary>
        [XmlElement("ServiceType", Namespace = ModelNamespaces.Ows)]
        public string Type { get; set; }

        /// <summary>
        /// Unordered list of one or more versions of this service type implemented by the server.
        /// </summary>
        [XmlElement("ServiceTypeVersion", Namespace = ModelNamespaces.Ows)]
        public string Versions { get; set; }

        /// <summary>
        /// Fees and terms for retrieving data from or otherwise using this server, including the monetary units as specified in ISO 4217. The reserved value NONE (case insensitive) shall be used to mean no fees or terms.
        /// </summary>
        [XmlElement("Fees", Namespace = ModelNamespaces.Ows)]
        public string Fees { get; set; }

        /// <summary>
        /// Access constraint applied to assure the protection of privacy or intellectual property, or any other restrictions on retrieving or using data from or otherwise using this server. The reserved value NONE (case insensitive) shall be used to mean no access constraints are imposed.
        /// </summary>
        [XmlElement("AccessConstraints", Namespace = ModelNamespaces.Ows)]
        public string AccessConstraints { get; set; }

    }
}
