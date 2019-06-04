using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    /// <summary>
    /// Request offering information about a job that has been executed asynchronously.
    /// </summary>
    [XmlRoot("GetStatus", Namespace = ModelNamespaces.Wps)]
    public class GetStatusRequest : RequestBase
    {

        /// <summary>
        /// The ID of the job concerned by the status request.
        /// </summary>
        [XmlElement("JobID", Namespace = ModelNamespaces.Wps)]
        public string JobId { get; set; }

    }
}
