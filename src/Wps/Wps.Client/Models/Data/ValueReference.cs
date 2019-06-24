using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [XmlRoot("ValueReference", Namespace = ModelNamespaces.Ows)]
    public class ValueReference : LiteralValue
    {}
}
