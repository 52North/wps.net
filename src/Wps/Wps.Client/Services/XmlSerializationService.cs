using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Wps.Client.Services
{
    public class XmlSerializationService : IXmlSerializer
    {
        public string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new CustomEncodingStringWriter(Encoding.UTF8))
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
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
