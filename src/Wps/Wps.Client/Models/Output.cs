using System;
using System.Xml.Serialization;
using Wps.Client.Models.Data;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [Serializable]
    [XmlRoot("Output", Namespace = ModelNamespaces.Wps)]
    public class Output : IdentifiableObject
    {

        /// <summary>
        /// The data that will be present in this output.
        /// </summary>
        [XmlElement("LiteralData", Type = typeof(LiteralData), Namespace = ModelNamespaces.Wps),
         XmlElement("ComplexData", Type = typeof(ComplexData), Namespace = ModelNamespaces.Wps),
         XmlElement("BoundingBoxData", Type = typeof(BoundingBoxData), Namespace = ModelNamespaces.Wps)]
        public Data.Data Data { get; set; }

        /// <summary>
        /// Nested outputs. The nesting level should be as low as possible.
        /// </summary>
        [XmlElement("Output", Type = typeof(Output), Namespace = ModelNamespaces.Wps)]
        public Output[] Outputs;

    }
}
