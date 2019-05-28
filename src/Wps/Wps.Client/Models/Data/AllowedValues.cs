using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [XmlRoot("AllowedValues", Namespace = ModelNamespaces.Ows)]
    public class AllowedValues : LiteralValue
    {

        [XmlElement("Value", Namespace = ModelNamespaces.Ows)]
        public string[] Values { get; set; }

    }
}
