using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [XmlRoot("LiteralDataDomain", Namespace = ModelNamespaces.Wps)]
    public class LiteralDataDomain
    {

        /// <summary>
        /// Identifies a valid format for an input or output.
        /// </summary>
        [XmlElement("AnyValue", Type = typeof(AnyValue), Namespace = ModelNamespaces.Ows),
         XmlElement("AllowedValues", Type = typeof(AllowedValues), Namespace = ModelNamespaces.Ows),
         XmlElement("ValueReference", Type = typeof(ValueReference), Namespace = ModelNamespaces.Ows)]
        public LiteralValue PossibleLiteralValues { get; set; }

        /// <summary>
        /// Reference to the data type of this set of values.
        /// </summary>
        [XmlElement("DataType", Namespace = ModelNamespaces.Ows)]
        public DataType DataType { get; set; }

        /// <summary>
        /// Indicates that this quantity has units and provides the unit of measurement.
        /// </summary>
        [XmlElement("ValuesUnit", Namespace = ModelNamespaces.Ows)]
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// The default value of this quantity.
        /// </summary>
        [XmlElement("DefaultValue", Namespace = ModelNamespaces.Ows)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Indicates that this is the default/native domain.
        /// </summary>
        [XmlAttribute("default")]
        public bool IsDefault { get; set; }

    }
}
