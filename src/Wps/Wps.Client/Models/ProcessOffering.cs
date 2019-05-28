using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("ProcessOffering", Namespace = ModelNamespaces.Wps)]
    public class ProcessOffering
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
        public string OutputTransmission { get; set; }

        /// <summary>
        /// Release version of the process.
        /// </summary>
        [XmlAttribute("processVersion", Namespace = ModelNamespaces.Wps)]
        public string ProcessVersion { get; set; }

        /// <summary>
        /// The process enclosed by the process offering.
        /// </summary>
        [XmlElement("Process", Namespace = ModelNamespaces.Wps)]
        public Process Process { get; set; }

    }
}
