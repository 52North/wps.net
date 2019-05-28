using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Information for contacting the service provider.
    /// </summary>
    [XmlRoot("ServiceContact", Namespace = ModelNamespaces.Ows)]
    public class ServiceContact
    {

        /// <summary>
        /// Name of the responsible person.
        /// </summary>
        [XmlElement("IndividualName", Namespace = ModelNamespaces.Ows)]
        public string IndividualName { get; set; }

        /// <summary>
        /// Role or position of the responsible person.
        /// </summary>
        [XmlElement("PositionName", Namespace = ModelNamespaces.Ows)]
        public string PositionName { get; set; }

        /// <summary>
        /// Address of the responsible party.
        /// </summary>
        [XmlElement("ContactInfo", Namespace = ModelNamespaces.Ows)]
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Function performed by the responsible party.
        /// </summary>
        [XmlElement("Role", Namespace = ModelNamespaces.Ows)]
        public string Role { get; set; }

    }
}
