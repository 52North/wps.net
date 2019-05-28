using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("ServiceProvider", Namespace = ModelNamespaces.Ows)]
    public class ServiceProvider
    {

        /// <summary>
        /// Metadata about the organization that provides this specific service instance or server.
        /// </summary>
        [XmlElement("ProviderName", Namespace = ModelNamespaces.Ows)]
        public string ProviderName { get; set; }

        /// <summary>
        /// Reference the most relevant web site of the service provider.
        /// </summary>
        [XmlElement("ProviderSite", Namespace = ModelNamespaces.Ows)]
        public ProviderSite ProviderSite { get; set; }

        /// <summary>
        /// Information for contacting the service provider.
        /// </summary>
        [XmlElement("ServiceContact", Namespace = ModelNamespaces.Ows)]
        public ServiceContact ServiceContact { get; set; }

    }
}
