using System.Xml.Serialization;

namespace Wps.Client.Models
{
    public enum RangeClosure
    {

        [XmlEnum("closed")] Closed,
        [XmlEnum("open")] Open,
        [XmlEnum("open-closed")] OpenClosed,
        [XmlEnum("closed-open")] ClosedOpen

    }
}
