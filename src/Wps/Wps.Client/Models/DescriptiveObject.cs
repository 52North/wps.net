using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Object containing human-readable descriptive information.
    /// </summary>
    public abstract class DescriptiveObject
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
        /// Keywords that characterize the process.
        /// </summary>
        [XmlArray("Keywords", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Keyword", Namespace = ModelNamespaces.Ows)]
        public string[] Keywords { get; set; }

    }
}
