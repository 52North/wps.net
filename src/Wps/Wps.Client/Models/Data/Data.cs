using System.Xml.Serialization;

namespace Wps.Client.Models.Data
{
    public abstract class Data
    {

        /// <summary>
        /// List of supported formats by the data, including mimetype, encoding and schema.
        /// </summary>
        [XmlElement("Format")]
        public Format[] Formats { get; set; }

    }
}
