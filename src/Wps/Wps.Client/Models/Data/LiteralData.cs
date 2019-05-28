using System.Xml.Schema;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Models.Data
{
    [XmlRoot("LiteralData", Namespace = ModelNamespaces.Wps)]
    public class LiteralData : Data
    {

        [XmlElement("LiteralDataDomain", Type = typeof(LiteralDataDomain), Form = XmlSchemaForm.Unqualified)]
        public LiteralDataDomain[] LiteralDataDomains { get; set; }

    }
}
