using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Models.Data;
using Wps.Client.Utils;

namespace Wps.Client.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Input", Namespace = ModelNamespaces.Wps)]
    public class Input : IdentifiableObject
    {

        /// <summary>
        /// Keywords that characterize the object. (i.e. Input, Output, Process, etc.)
        /// </summary>
        [XmlArray("Keywords", Namespace = ModelNamespaces.Ows)]
        [XmlArrayItem(typeof(string))]
        public string[] Keywords;

        /// <summary>
        /// Minimum number of times that values for this parameter are required
        /// </summary>
        [XmlAttribute("minOccurs", Form = XmlSchemaForm.Unqualified)]
        public int MinimumOccurrences { get; set; } = 1;

        /// <summary>
        /// Maximum number of times that this parameter may be present
        /// </summary>
        [XmlAttribute("maxOccurs", Form = XmlSchemaForm.Unqualified)]
        public int MaximumOccurrences { get; set; } = 1;

        /// <summary>
        /// The data that will be present in this input.
        /// </summary>
        [XmlElement("LiteralData", Type = typeof(LiteralData), Namespace = ModelNamespaces.Wps),
         XmlElement("ComplexData", Type = typeof(ComplexData), Namespace = ModelNamespaces.Wps)]
        public Data.Data Data { get; set; }

        /// <summary>
        /// Nested inputs. The nesting level should be as low as possible.
        /// </summary>
        [XmlElement("Input", Type = typeof(Input), Namespace = ModelNamespaces.Wps)]
        public Input[] Inputs;

    }
}
