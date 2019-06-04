using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [XmlRoot("Output", Namespace = ModelNamespaces.Wps)]
    public class ResultOutput
    {

        /// <summary>
        /// Unambiguous identifier or name of an output item.
        /// </summary>
        [XmlAttribute("id", Namespace = ModelNamespaces.Wps)]
        public string Id { get; set; }

        /// <summary>
        /// The data provided by this output item.
        /// </summary>
        [XmlElement("Data", Namespace = ModelNamespaces.Wps)]
        public object Data { get; set; }

        /// <summary>
        /// Nested output, child element.
        /// </summary>
        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public ResultOutput Output { get; set; }

    }
}
