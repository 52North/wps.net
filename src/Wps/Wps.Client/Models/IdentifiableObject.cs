using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Represents an object that has properties allowing itself to be identified.
    /// </summary>
    public abstract class IdentifiableObject
    {

        /// <summary>
        /// Human readable title for the object. (i.e. Input, Output, Process, etc.)
        /// </summary>
        /// <remarks>
        /// This property is required and shall not be null!
        /// </remarks>
        [XmlElement("Title", Namespace = ModelNamespaces.Ows)]
        public string Title { get; set; }

        /// <summary>
        /// Human readable short description of the object. (i.e. Input, Output, Process, etc.)
        /// </summary>
        [XmlElement("Abstract", Namespace = ModelNamespaces.Ows)]
        public string Abstract { get; set; }

        /// <summary>
        /// Unambiguous identifier of the object. (i.e. Input, Output, Process, etc.)
        /// </summary>
        [XmlElement("Identifier", Namespace = ModelNamespaces.Ows)]
        public string Identifier { get; set; }

    }
}
