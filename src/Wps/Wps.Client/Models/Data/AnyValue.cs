using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [XmlRoot("AnyValue", Namespace = ModelNamespaces.Ows)]
    public class AnyValue : LiteralValue
    {
    }
}
