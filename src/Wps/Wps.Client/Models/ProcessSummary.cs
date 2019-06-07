using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("ProcessSummary", Namespace = ModelNamespaces.Wps)]
    public class ProcessSummary : IdentifiableObject
    {

        /// <summary>
        /// Type of the process description.
        /// </summary>
        [XmlAttribute("processModel", Namespace = ModelNamespaces.Wps)]
        public string ProcessModel { get; set; } = "native";

        /// <summary>
        /// Job control options supported for this process. (i.e. sync-execute, async-execute)
        /// </summary>
        /// <remarks>
        /// At least one option is required.
        /// </remarks>
        [XmlAttribute("jobControlOptions", Namespace = ModelNamespaces.Wps)]
        public string JobControlOptions { get; set; }

        /// <summary>
        /// Supported transmission modes for the outputs. (i.e. by value, by reference)
        /// </summary>
        /// <remarks>
        /// At least one mode is required.
        /// </remarks>
        [XmlAttribute("outputTransmission", Namespace = ModelNamespaces.Wps)]
        public TransmissionMode OutputTransmission { get; set; }

        /// <summary>
        /// Keywords that characterize the process.
        /// </summary>
        [XmlArray("Keywords", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem("Keyword", Namespace = ModelNamespaces.Ows)]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Version of the process offered.
        /// </summary>
        [XmlAttribute("processVersion", Namespace = ModelNamespaces.Wps)]
        public string ProcessVersion { get; set; }

    }
}
