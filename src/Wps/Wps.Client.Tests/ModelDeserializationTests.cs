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

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithAllowedValues_ShouldPass()
        {
            var serialize = new XmlSerializer(typeof(LiteralDataDomain));
            var xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:AllowedValues>
                            <ows:Value>uncorrected</ows:Value>
                            <ows:Value>dos1</ows:Value>
                            <ows:Value>dos2</ows:Value>
                            <ows:Value>dos2b</ows:Value>
                            <ows:Value>dos3</ows:Value>
                            <ows:Value>dos4</ows:Value>
                        </ows:AllowedValues>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            using (var reader = new StringReader(xml))
            {
                var domain = serialize.Deserialize(reader) as LiteralDataDomain;
                domain.Should().NotBeNull();
                domain?.IsDefault.Should().BeTrue();
                domain?.DefaultValue.Should().Be("uncorrected");
                domain?.PossibleLiteralValues.GetType().Should().Be(typeof(AllowedValues));
                domain?.DataType?.Reference.Should().Be("string");
                if(domain?.PossibleLiteralValues is AllowedValues values)
                {
                    values.Values.Length.Should().Be(6);
                }
            }
        }

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithAnyValue_ShouldPass()
        {
            var serialize = new XmlSerializer(typeof(LiteralDataDomain));
            var xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:AnyValue></ows:AnyValue>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            using (var reader = new StringReader(xml))
            {
                var domain = serialize.Deserialize(reader) as LiteralDataDomain;
                domain.Should().NotBeNull();
                domain?.IsDefault.Should().BeTrue();
                domain?.DefaultValue.Should().Be("uncorrected");
                domain?.PossibleLiteralValues.GetType().Should().Be(typeof(AnyValue));
                domain?.DataType?.Reference.Should().Be("string");                         

            }
        }

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithValueReference_ShouldPass()
        {
            var serialize = new XmlSerializer(typeof(LiteralDataDomain));
            var xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:ValueReference></ows:ValueReference>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            using (var reader = new StringReader(xml))
            {
                var domain = serialize.Deserialize(reader) as LiteralDataDomain;
                domain.Should().NotBeNull();
                domain?.IsDefault.Should().BeTrue();
                domain?.DefaultValue.Should().Be("uncorrected");
                domain?.PossibleLiteralValues.GetType().Should().Be(typeof(ValueReference));
                domain?.DataType?.Reference.Should().Be("string");

            }
        }

        [Fact]
        public void DeserializeLiteralData_ValidXmlGiven_WithLiteralDataDomain_ShouldPass()
        {
            var serialize = new XmlSerializer(typeof(LiteralData));
            var xml = @"<wps:LiteralData xmlns:ns=""http://www.opengis.net/wps/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ns:Format default=""true"" mimeType=""text/plain""/>
                            <ns:Format mimeType=""text/xml""/>
                            <LiteralDataDomain>
                                <ows:AnyValue/>
                                <ows:DataType ows:reference=""float""/>
                            </LiteralDataDomain>
                        </wps:LiteralData>";

            using (var reader = new StringReader(xml))
            {
                var data = serialize.Deserialize(reader) as LiteralData;
                data.Should().NotBeNull();
                data?.Formats.Length.Should().Be(2);
                data?.LiteralDataDomains.Length.Should().Be(1);
            }
        }
    }
}
