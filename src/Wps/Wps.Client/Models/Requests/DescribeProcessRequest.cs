using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    /// <summary>
    /// Request used to describe a process.
    /// </summary>
    [XmlRoot("DescribeProcess", Namespace = ModelNamespaces.Wps)]
    public class DescribeProcessRequest : RequestBase
    {

        /// <summary>
        /// The identifier of the process to be described.
        /// </summary>
        [XmlElement("Identifier", Namespace = ModelNamespaces.Ows)]
        public string[] Identifiers { get; set; }

        /// <summary>
        /// The language that should be used in the process description.
        /// </summary>
        [XmlAttribute("lang", Namespace = ModelNamespaces.Wps)]
        public string Language { get; set; }
        
    }
}
