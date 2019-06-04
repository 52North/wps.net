using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    /// <summary>
    /// Operation allowing WPS clients to query the result of a finished processing job.
    /// </summary>
    [XmlRoot("GetResult", Namespace = ModelNamespaces.Wps)]
    public class GetResultRequest : RequestBase
    {

        /// <summary>
        /// Job identifier.
        /// </summary>
        [XmlElement("JobID", Namespace = ModelNamespaces.Wps)]
        public string JobId { get; set; }

    }
}
