using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("Address", Namespace = ModelNamespaces.Ows)]
    public class Address
    {

        /// <summary>
        /// Address line for the location.
        /// </summary>
        [XmlElement("DeliveryPoint", Namespace = ModelNamespaces.Ows)]
        public string[] DeliveryPoints { get; set; }

        /// <summary>
        /// City of location
        /// </summary>
        [XmlElement("City", Namespace = ModelNamespaces.Ows)]
        public string City { get; set; }

        /// <summary>
        /// State or province of the location.
        /// </summary>
        [XmlElement("AdministrativeArea", Namespace = ModelNamespaces.Ows)]
        public string AdministrativeArea { get; set; }

        /// <summary>
        /// ZIP or other postal code.
        /// </summary>
        [XmlElement("PostalCode", Namespace = ModelNamespaces.Ows)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country of the physical address.
        /// </summary>
        [XmlElement("Country", Namespace = ModelNamespaces.Ows)]
        public string Country { get; set; }

        /// <summary>
        /// Address of the electronic mailbox of the responsible organization or individual.
        /// </summary>
        [XmlElement("ElectronicMailAddress", Namespace = ModelNamespaces.Ows)]
        public string[] Emails { get; set; }

    }
}
