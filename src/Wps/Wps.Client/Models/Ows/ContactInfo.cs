using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    /// <summary>
    /// Address of the responsible party.
    /// </summary>
    [XmlRoot("ContactInfo", Namespace = ModelNamespaces.Ows)]
    public class ContactInfo
    {

        /// <summary>
        /// Telephone numbers at which the organization or individual may be contacted.
        /// </summary>
        // TODO: Add 'Facsimile' numbers deserialization
        [XmlArray("Phone", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Voice", Namespace = ModelNamespaces.Ows)]
        public string[] Phone { get; set; }

        /// <summary>
        /// Physical and email address at which the organization or individual may be contacted.
        /// </summary>
        [XmlElement("Address", Namespace = ModelNamespaces.Ows, Type = typeof(Address))]
        public Address Address { get; set; }

        /// <summary>
        /// Time period when individuals can contact the organization or individual.
        /// </summary>
        [XmlElement("HoursOfService", Namespace = ModelNamespaces.Ows)]
        public string HoursOfService { get; set; }

        /// <summary>
        /// Supplemental instructions on how or when to contact the individual or organization.
        /// </summary>
        [XmlElement("ContactInstructions", Namespace = ModelNamespaces.Ows)]
        public string ContactInstructions { get; set; }

    }
}
