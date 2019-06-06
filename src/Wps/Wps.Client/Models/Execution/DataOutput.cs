using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("Output", Namespace = ModelNamespaces.Wps)]
    public class DataOutput
    {

        [XmlAttribute("transmission")]
        public TransmissionMode Transmission { get; set; }

        [XmlAttribute("id")]
        public string Identifier { get; set; }

        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public DataOutput[] Outputs { get; set; }

    }
}
