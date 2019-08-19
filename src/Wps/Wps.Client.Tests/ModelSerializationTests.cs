using FluentAssertions;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Ows;
using Wps.Client.Models.Requests;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class ModelSerializationTests : IClassFixture<XmlSerializationService>
    {

        private readonly XmlSerializationService _serializer;

        public ModelSerializationTests(XmlSerializationService serializer)
        {
            _serializer = serializer;
        }

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/Requests/GetCapabilities.xml")]
        public void SerializeGetCapabilitiesRequest_ValidRequestGiven_ShouldPass(string expectedXml)
        {
            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetCapabilitiesRequest()
            {
                AcceptedFormats = new[] { "text/xml", "text/plain" },
                AcceptedVersions = new[] { "1.0.0", "2.0.0" },
                Sections = new[] { "section 1", "section 2" },
                UpdateSequence = "update sequence"
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeDescribeProcessRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                         <wps:DescribeProcess xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" lang=""en-US"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                                           <ows:Identifier>id1</ows:Identifier>
                                           <ows:Identifier>id2</ows:Identifier>
                                           <ows:Identifier>id3</ows:Identifier>
                                         </wps:DescribeProcess>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new DescribeProcessRequest()
            {
                Identifiers = new[] { "id1", "id2", "id3" },
                Language = "en-US",
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeGetStatusRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                         <wps:GetStatus xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                                             <wps:JobID>testJobId</wps:JobID>
                                         </wps:GetStatus>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetStatusRequest
            {
                JobId = "testJobId"
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeGetResultRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                         <wps:GetResult xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                                             <wps:JobID>testJobId</wps:JobID>
                                         </wps:GetResult>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetResultRequest()
            {
                JobId = "testJobId"
            };

            var resultXml = _serializer.Serialize(request);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeValueRange_ValidRangeGiven_ShouldPass()
        {
            const string expectedXml = @"
<?xml version=""1.0"" encoding=""utf-8""?>
<ows:Range xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" rangeClosure=""closed-open"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
   <ows:MinimumValue>10</ows:MinimumValue>
   <ows:MaximumValue>100</ows:MaximumValue>
   <ows:Spacing>10</ows:Spacing>
</ows:Range>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var range = new ValueRange
            {
                MinimumValue = "10",
                MaximumValue = "100",
                RangeClosure = RangeClosure.ClosedOpen,
                Spacing = "10"
            };

            var resultXml = _serializer.Serialize(range);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeDataInput_ValidInputGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><Input id=""test-id"" mimeType=""test mime type"" encoding=""test encoding"" schema=""test schema"" xmlns=""http://www.opengis.net/wps/2.0""><wps:Data xmlns:wps=""http://www.opengis.net/wps/2.0""><LiteralValue xmlns=""http://www.opengis.net/wps/2.0"">105</LiteralValue></wps:Data><wps:Reference xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xli:href=""test"" schema=""test-schema"" xmlns:wps=""http://www.opengis.net/wps/2.0"" /></Input>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var dataInput = new DataInput
            {
                Identifier = "test-id",
                Reference = new ResourceReference
                {
                    Href = "test",
                    Schema = "test-schema"
                },
                Data = new LiteralDataValue
                {
                    Value = 105.ToString(CultureInfo.InvariantCulture)
                },
                MimeType = "test mime type",
                Schema = "test schema",
                Encoding = "test encoding"
            };

            var resultXml = _serializer.Serialize(dataInput);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeDataOutput_ValidOutputGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><wps:Output xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" mimeType=""test-mimetype"" encoding=""test-encoding"" schema=""test-schema"" transmission=""value"" id=""test-id"" xmlns:wps=""http://www.opengis.net/wps/2.0"" />";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var dataInput = new DataOutput()
            {
                Identifier = "test-id",
                Transmission = TransmissionMode.Value,
                Encoding = "test-encoding",
                MimeType = "test-mimetype",
                Schema = "test-schema"
            };

            var resultXml = _serializer.Serialize(dataInput);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeExecuteRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><wps:Execute xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" mode=""sync"" response=""document"" xmlns:wps=""http://www.opengis.net/wps/2.0""><ows:Identifier>org.n52.wps.server.algorithm.SimpleBufferAlgorithm</ows:Identifier><wps:Input id=""data""><wps:Reference xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xli:href=""http://geoprocessing.demo.52north.org:8080/geoserver/wfs?SERVICE=WFS&amp;VERSION=1.0.0&amp;REQUEST=GetFeature&amp;TYPENAME=topp:tasmania_roads&amp;SRS=EPSG:4326&amp;OUTPUTFORMAT=GML3"" schema=""http://schemas.opengis.net/gml/3.1.1/base/feature.xsd"" xmlns:wps=""http://www.opengis.net/wps/2.0"" /></wps:Input><wps:Input id=""width""><wps:Data><LiteralValue xmlns=""http://www.opengis.net/wps/2.0"">0.05</LiteralValue></wps:Data></wps:Input><wps:Output transmission=""value"" id=""result"" /></wps:Execute>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var executeRequest = new ExecuteRequest
            {
                Identifier = "org.n52.wps.server.algorithm.SimpleBufferAlgorithm",
                ExecutionMode = ExecutionMode.Synchronous,
                ResponseType = ResponseType.Document,
                Inputs = new[]
                {
                    new DataInput
                    {
                        Identifier = "data",
                        Reference = new ResourceReference
                        {
                            Href = "http://geoprocessing.demo.52north.org:8080/geoserver/wfs?SERVICE=WFS&VERSION=1.0.0&REQUEST=GetFeature&TYPENAME=topp:tasmania_roads&SRS=EPSG:4326&OUTPUTFORMAT=GML3",
                            Schema = "http://schemas.opengis.net/gml/3.1.1/base/feature.xsd"
                        }
                    },
                    new DataInput
                    {
                        Identifier = "width",
                        Data = new LiteralDataValue
                        {
                            Value = 0.05f.ToString(CultureInfo.InvariantCulture)
                        }
                    }
                },
                Outputs = new[]
                {
                    new DataOutput
                    {
                        Identifier = "result",
                        Transmission = TransmissionMode.Value
                    }
                }
            };

            var resultXml = _serializer.Serialize(executeRequest);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeBoundingBoxData_ValidObjectsGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><wps:BoundingBoxData xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:wps=""http://www.opengis.net/wps/2.0""><wps:Format mimeType=""test"" maximumMegabytes=""0"" default=""false"" /><wps:Format mimeType=""test"" maximumMegabytes=""0"" default=""false"" /><wps:SupportedCRS default=""true"">test-uri-1</wps:SupportedCRS><wps:SupportedCRS default=""true"">test-uri-1</wps:SupportedCRS><wps:SupportedCRS default=""true"">test-uri-1</wps:SupportedCRS></wps:BoundingBoxData>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var boundingBoxData = new BoundingBoxData
            {
                Formats = new[]
                {
                    new Format { MimeType = "test" },
                    new Format { MimeType = "test" },
                },
                SupportedCrs = new[]
                {
                    new CoordinateReferenceSystem{ IsDefault = true, Uri = "test-uri-1" },
                    new CoordinateReferenceSystem{ IsDefault = true, Uri = "test-uri-1" },
                    new CoordinateReferenceSystem{ IsDefault = true, Uri = "test-uri-1" },
                }
            };

            var resultXml = _serializer.Serialize(boundingBoxData);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Theory]
        [InlineData("crs-uri", 3, new[] { 1.1, 2.2, 3.3 }, new[] { 3.1, 2.2, 1.3 })]
        public void SerializeBoundingBoxValue_ValidObjectsGiven_ShouldPass(string crsUri, int dimensionCount, double[] lCornerPoints, double[] rCornerPoints)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");

            var expectedXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<BoundingBox p1:crs=""crs-uri"" p1:dimensions=""3"" xmlns:p1=""http://www.opengis.net/ows/2.0"" xmlns=""http://www.opengis.net/ows/2.0"">
    <ows:LowerCorner xmlns:ows=""http://www.opengis.net/ows/2.0"">{string.Join(" ", lCornerPoints)}</ows:LowerCorner>
    <ows:UpperCorner xmlns:ows=""http://www.opengis.net/ows/2.0"">{string.Join(" ", rCornerPoints)}</ows:UpperCorner>
</BoundingBox>";

            // Remove white spaces and new line characters for XML comparison.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var boundingBoxData = new BoundingBoxValue
            {
                CrsUri = crsUri,
                DimensionCount = dimensionCount,
                LowerCornerPoints = lCornerPoints,
                UpperCornerPoints = rCornerPoints,
            };

            var resultXml = _serializer.Serialize(boundingBoxData);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

    }
}