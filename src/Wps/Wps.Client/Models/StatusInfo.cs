using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Object used to provide identification and status information about a running job.
    /// </summary>
    [XmlRoot("StatusInfo", Namespace = ModelNamespaces.Wps)]
    public class StatusInfo : IXmlSerializable
    {

        /// <summary>
        /// Unambiguously identifier of a job within a WPS instance.
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// Well-known identifier describing the status of the job.
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        /// Date and time by which the job and its results will be no longer accessible.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Date and time by which the processing job will be finished.
        /// </summary>
        public DateTime? EstimatedCompletion { get; set; }

        /// <summary>
        /// Date and time for the next suggested status polling.
        /// </summary>
        public DateTime? NextPollDateTime { get; set; }

        /// <summary>
        /// Percentage of process that has been completed.
        /// </summary>
        public int? CompletionRate { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var subtreeReader = reader.ReadSubtree();
            while (subtreeReader.Read())
            {
                if(subtreeReader.NodeType == XmlNodeType.Element)
                {
                    if (subtreeReader.LocalName.Equals("JobID"))
                    {
                        JobId = subtreeReader.ReadElementContentAsString();
                    }

                    if (subtreeReader.LocalName.Equals("Status"))
                    {
                        if(Enum.TryParse(subtreeReader.ReadElementContentAsString(), out JobStatus status))
                        {
                            Status = status;
                        }
                    }

                    if (subtreeReader.LocalName.Equals("ExpirationDate"))
                    {
                        if (DateTime.TryParse(subtreeReader.ReadElementContentAsString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var date))
                        {
                            ExpirationDate = date;
                        }
                    }

                    if (subtreeReader.LocalName.Equals("EstimatedCompletion"))
                    {
                        if (DateTime.TryParse(subtreeReader.ReadElementContentAsString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var date))
                        {
                            EstimatedCompletion = date;
                        }
                    }
                    if (subtreeReader.LocalName.Equals("NextPoll"))
                    {
                        if (DateTime.TryParse(subtreeReader.ReadElementContentAsString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var date))
                        {
                            NextPollDateTime = date;
                        }
                    }

                    if (subtreeReader.LocalName.Equals("PercentCompleted"))
                    {
                        if (int.TryParse(subtreeReader.ReadElementContentAsString(), out var percent))
                        {
                            CompletionRate = percent;
                        }
                    }
                }
            }

            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
