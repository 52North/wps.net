using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// Metadata about the operations and related abilities specified by this service and implemented by this server.
    /// </summary>
    [XmlRoot("OperationsMetadata", Namespace = ModelNamespaces.Ows)]
    public class OperationsMetadata
    {

        /// <summary>
        /// Metadata for unordered list of all the operations that this server interface implements.
        /// </summary>
        [XmlElement("Operation", Namespace = ModelNamespaces.Ows)]
        public Operation[] Operations { get; set; }

        /// <summary>
        /// Optional unordered list of parameter valid domains that each apply to one or more operations which this server interface implements.
        /// </summary>
        [XmlElement("Parameter", Namespace = ModelNamespaces.Ows)]
        public OperationParameter[] Parameters { get; set; }

        /// <summary>
        /// Optional unordered list of valid domain constraints on non-parameter quantities that each apply to this server.
        /// </summary>
        [XmlElement("Constraint", Namespace = ModelNamespaces.Ows)]
        public OperationConstraint[] Constraints { get; set; }

        // TODO: Add list of Metadata

    }
}
