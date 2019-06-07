using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("DCP", Namespace = ModelNamespaces.Ows)]
    public class DistributedComputingPlatform
    {

        /// <summary>
        /// Connection point URLs for the HTTP DCP.
        /// </summary>
        [XmlElement("HTTP", Namespace = ModelNamespaces.Ows)]
        public PlatformConnectionPoint HttpConnectionPoint { get; set; }

    }
}
