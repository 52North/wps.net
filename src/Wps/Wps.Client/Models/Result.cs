using System;
using System.Xml;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("Result", Namespace = ModelNamespaces.Wps)]
    public class Result<TData>
    {

        /// <summary>
        /// Unambiguously identifier of a job within a WPS instance.
        /// </summary>
        [XmlElement("JobID", Namespace = ModelNamespaces.Wps)]
        public string JobId { get; set; }

        /// <summary>
        /// Date and time by which the results will be no longer accessible.
        /// </summary>
        [XmlElement("ExpirationDate", Namespace = ModelNamespaces.Wps, Type = typeof(DateTime))]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Output item returned by a process execution.
        /// </summary>
        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public ResultOutput<TData>[] Outputs { get; set; }

    }
}
