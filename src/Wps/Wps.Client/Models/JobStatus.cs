using System.Xml.Serialization;

namespace Wps.Client.Models
{
    public enum JobStatus
    {

        [XmlEnum("Succeeded")] Succeeded,
        [XmlEnum("Failed")] Failed,
        [XmlEnum("Accepted")] Accepted,
        [XmlEnum("Running")] Running

    }
}
