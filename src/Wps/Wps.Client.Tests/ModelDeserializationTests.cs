using FluentAssertions;
using System.IO;
using System.Xml.Serialization;
using Wps.Client.Models;
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

    }
}
