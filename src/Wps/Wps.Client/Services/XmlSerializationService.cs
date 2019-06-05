using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Wps.Client.Utils;

namespace Wps.Client.Services
{
    public class XmlSerializationService : IXmlSerializer
    {
        public string Serialize(object obj, bool omitHeaderDeclaration = false)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("wps", ModelNamespaces.Wps);
            namespaces.Add("ows", ModelNamespaces.Ows);
            namespaces.Add("xli", ModelNamespaces.Xlink);
            namespaces.Add("xsi", ModelNamespaces.XmlSchemaInstance);

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitHeaderDeclaration
            };

            using (var writer = new CustomEncodingStringWriter(Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(xmlWriter, obj, namespaces);
                    return writer.ToString();
                }
            }
        }

        public T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                var obj = (T) serializer.Deserialize(reader);
                return obj;
            }
        }

        private class CustomEncodingStringWriter : StringWriter
        {
            public CustomEncodingStringWriter(Encoding encoding)
            {
                Encoding = encoding;
            }

            public override Encoding Encoding { get; }
        }

    }
}
