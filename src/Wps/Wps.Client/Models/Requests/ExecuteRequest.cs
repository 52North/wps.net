using System.Xml.Serialization;
using Wps.Client.Models.Execution;
using Wps.Client.Utils;

namespace Wps.Client.Models.Requests
{
    [XmlRoot("Execute", Namespace = ModelNamespaces.Wps)]
    public class ExecuteRequest : RequestBase
    {

        [XmlAttribute("mode", Namespace = ModelNamespaces.Wps)]
        public ExecutionMode ExecutionMode { get; set; }

        [XmlAttribute("response", Namespace = ModelNamespaces.Wps)]
        public ResponseType ResponseType { get; set; }

        [XmlElement("Identifier", Namespace = ModelNamespaces.Ows)]
        public string Identifier { get; set; }

        [XmlElement("Input", Namespace = ModelNamespaces.Wps)]
        public DataInput[] Inputs { get; set; }

        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public DataOutput[] Outputs { get; set; }

    }
}
