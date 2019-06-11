using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    /// <summary>
    /// Connection point URLs for the HTTP DCP.
    /// </summary>
    [XmlRoot("HTTP", Namespace = ModelNamespaces.Ows)]
    public class PlatformConnectionPoint
    {

        /// <summary>
        /// Connection point URL prefix and any constraints for the HTTP "Get" request method for this operation request.
        /// </summary>
        [XmlElement("Get", Namespace = ModelNamespaces.Ows)]
        public ConnectionPoint Get { get; set; }

        /// <summary>
        /// Connection point URL and any constraints ofr the HTTP "Post" request method for this operation request.
        /// </summary>
        [XmlElement("Post", Namespace = ModelNamespaces.Ows)]
        public ConnectionPoint Post { get; set; }

    }

    public class ConnectionPoint
    {
        [XmlAttribute("href", Namespace = ModelNamespaces.Xlink)]
        public string Hyperlink { get; set; }
    }

}