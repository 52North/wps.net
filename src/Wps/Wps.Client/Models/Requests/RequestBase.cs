using System.Xml.Serialization;

namespace Wps.Client.Models.Requests
{
    public abstract class RequestBase : Request
    {

        /// <summary>
        /// The type of the required service.
        /// </summary>
        [XmlAttribute("service")]
        public string Service { get; set; } = "WPS";

        /// <summary>
        /// The version of the required service.
        /// </summary>
        [XmlAttribute("version")]
        public string Version { get; set; } = "2.0.0";

    }
}
