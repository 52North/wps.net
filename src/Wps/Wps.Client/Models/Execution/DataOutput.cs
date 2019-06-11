using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Execution
{
    [XmlRoot("Output", Namespace = ModelNamespaces.Wps)]
    public class DataOutput
    {

        /// <summary>
        /// The media type of the data.
        /// </summary>
        /// <remarks>
        /// It is mandatory to precise the media type.
        /// </remarks>
        [XmlAttribute("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// The encoding procedure or character set of the data. (e.g. raw or base64)
        /// </summary>
        /// <remarks>
        /// It is mandatory to precise the encoding.
        /// </remarks>
        [XmlAttribute("encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// The identification of the data schema.
        /// </summary>
        /// <remarks>
        /// It is mandatory to precise the schema.
        /// </remarks>
        [XmlAttribute("schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Code that indicates the desired data transmission mode for this output.
        /// </summary>
        [XmlAttribute("transmission")]
        public TransmissionMode Transmission { get; set; }

        /// <summary>
        /// Identifier of a particular output, as defined in the process description.
        /// </summary>
        [XmlAttribute("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Nested outputs of this data output.
        /// </summary>
        [XmlElement("Output", Namespace = ModelNamespaces.Wps)]
        public DataOutput[] Outputs { get; set; }

    }
}
