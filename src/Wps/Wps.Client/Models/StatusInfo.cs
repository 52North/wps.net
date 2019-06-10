using System;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Object used to provide identification and status information about a running job.
    /// </summary>
    [XmlRoot("StatusInfo", Namespace = ModelNamespaces.Wps)]
    public class StatusInfo
    {

        /// <summary>
        /// Unambiguously identifier of a job within a WPS instance.
        /// </summary>
        [XmlElement("JobID", Namespace = ModelNamespaces.Wps)]
        public string JobId { get; set; }

        /// <summary>
        /// Well-known identifier describing the status of the job.
        /// </summary>
        [XmlElement("Status", Namespace = ModelNamespaces.Wps, Type = typeof(JobStatus))]
        public JobStatus Status { get; set; }

        /// <summary>
        /// Date and time by which the job and its results will be no longer accessible.
        /// </summary>
        [XmlElement("ExpirationDate", Namespace = ModelNamespaces.Wps, Type = typeof(DateTime))]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Date and time by which the processing job will be finished.
        /// </summary>
        [XmlElement("EstimatedCompletion", Namespace = ModelNamespaces.Wps, Type = typeof(DateTime))]
        public DateTime EstimatedCompletion { get; set; }

        /// <summary>
        /// Date and time for the next suggested status polling.
        /// </summary>
        [XmlElement("NextPoll", Namespace = ModelNamespaces.Wps, Type = typeof(DateTime))]
        public DateTime NextPollDateTime { get; set; }

        /// <summary>
        /// Percentage of process that has been completed.
        /// </summary>
        [XmlElement("PercentCompleted", Namespace = ModelNamespaces.Wps)]
        public int CompletionRate { get; set; }

    }
}
