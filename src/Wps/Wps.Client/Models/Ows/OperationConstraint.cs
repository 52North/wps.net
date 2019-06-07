using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Ows
{
    [XmlRoot("Constraint", Namespace = ModelNamespaces.Ows)]
    public class OperationConstraint
    {

        /// <summary>
        /// Name or identifier of this parameter or other quantity.
        /// </summary>
        [XmlAttribute("name", Namespace = ModelNamespaces.Ows)]
        public string Name { get; set; }

        /// <summary>
        /// Unordered list of all the valid values for this parameter or other quantity.
        /// </summary>
        [XmlElement("Value", Namespace = ModelNamespaces.Ows)]
        public string[] Values { get; set; }

        // TODO: Add list of Metadata

    }
}
