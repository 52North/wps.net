using FluentAssertions;
using System.IO;
using System.Xml.Serialization;
using Wps.Client.Models;
using Wps.Client.Models.Data;
using Xunit;

namespace Wps.Client.Tests
{
    public class ModelDeserializationTests
    {

        [Fact]
        public void DeserializeFormat_ValidFormatGiven_ShouldPass()
        {
            var serializer = new XmlSerializer(typeof(Format));
            var xml =
                @"<ns:Format xmlns:ns=""http://www.opengis.net/wps/2.0"" mimeType=""application/x-geotiff"" encoding=""base64"" schema=""test.schema.tld"" maximumMegabytes=""2048"" default=""true""/>";

            using (var reader = new StringReader(xml))
            {
                var format = serializer.Deserialize(reader) as Format;
                format.Should().NotBeNull();
                if (format != null)
                {
                    format.MimeType.Should().Be("application/x-geotiff");
                    format.Encoding.Should().Be("base64");
                    format.Schema.Should().Be("test.schema.tld");
                    format.MaximumMegabytes.Should().Be(2048);
                    format.IsDefault.Should().BeTrue();
                }
            }
        }

        [Fact]
        public void DeserializeDataType_ValidXmlGiven_ShouldPass()
        {
            var serialize = new XmlSerializer(typeof(DataType));
            var xml = @"<ows:DataType xmlns:ows=""http://www.opengis.net/ows/2.0"" ows:reference=""string""/>";

            using (var reader = new StringReader(xml))
            {
                var dataType = serialize.Deserialize(reader) as DataType;
                dataType.Should().NotBeNull();
                dataType?.Reference.Should().Be("string");
                dataType?.GetReferenceType().Should().Be(typeof(string));
            }
        }

    }
}
