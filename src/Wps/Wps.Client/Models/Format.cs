using System.Text;
using System.Xml.Serialization;

namespace Wps.Client.Models
{
    [XmlRoot("Format")]
    public class Format
    {

        /// <summary>
        /// The media type of the data.
        /// </summary>
        /// <remarks>
        /// It is mandatory to precise the media type.
        /// </remarks>
        [XmlAttribute("mimetype")]
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
        /// The maximum size of the input data, in megabytes.
        /// </summary>
        [XmlAttribute("maximumMegabytes")]
        public int MaximumMegabytes { get; set; }

        /// <summary>
        /// Indicates that this format is the default format.
        /// </summary>
        [XmlAttribute("default")]
        public bool IsDefault { get; set; }

    }
}
