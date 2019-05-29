using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("Operation", Namespace = ModelNamespaces.Ows)]
    public class Operation
    {

        /// <summary>
        /// Name or identifier of this operation.
        /// </summary>
        [XmlAttribute("name", Namespace = ModelNamespaces.Ows)]
        public string Name { get; set; }

        /// <summary>
        /// Unordered list of Distributed Computing Platforms supported for this operation.
        /// </summary>
        [XmlElement("DCP", Namespace = ModelNamespaces.Ows)]
        public DistributedComputingPlatform[] DistributedComputingPlatforms { get; set; }

        /// <summary>
        /// Unordered list of parameter valid domains that each apply to one or more operations which this server interface implements.
        /// </summary>
        [XmlElement("Parameter", Namespace = ModelNamespaces.Ows)]
        public OperationParameter[] Parameters { get; set; }

        /// <summary>
        /// Unordered list of valid domain constraints on non-parameter quantities that each apply to this server.
        /// </summary>
        [XmlElement("Constraint", Namespace = ModelNamespaces.Ows)]
        public OperationConstraint[] Constraints { get; set; }

        // TODO: Add ExtendedCapabilities? Vendor specific. To be seen.

    }
}
