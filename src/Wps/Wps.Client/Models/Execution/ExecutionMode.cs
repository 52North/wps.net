using System.Xml.Serialization;

namespace Wps.Client.Models.Execution
{
    public enum ExecutionMode
    {
        [XmlEnum("sync")] Synchronous,
        [XmlEnum("async")] Asynchronous,
        [XmlEnum("auto")] Auto
    }
}
