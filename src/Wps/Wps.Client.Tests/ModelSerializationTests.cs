using FluentAssertions;
using System.Text.RegularExpressions;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
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

        [Fact]
        public void SerializeGetCapabilitiesRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><GetCapabilities xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" service=""WPS"" xmlns=""http://www.opengis.net/wps/2.0"" />";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var request = new GetCapabilitiesRequest()
            {
                Service = "WPS"
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

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><Input id=""test-id"" xmlns=""http://www.opengis.net/wps/2.0""><wps:Data xmlns:wps=""http://www.opengis.net/wps/2.0""><LiteralValue xmlns=""http://www.opengis.net/wps/2.0"">105</LiteralValue></wps:Data><wps:Reference xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xli:href=""test"" schema=""test-schema"" xmlns:wps=""http://www.opengis.net/wps/2.0"" /></Input>";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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
                    Value = 105
                }
            };

            var resultXml = _serializer.Serialize(dataInput);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeDataOutput_ValidOutputGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><wps:Output xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" transmission=""value"" id=""test-id"" xmlns:wps=""http://www.opengis.net/wps/2.0"" />";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var dataInput = new DataOutput()
            {
                Identifier = "test-id",
                Transmission = TransmissionMode.Value
            };

            var resultXml = _serializer.Serialize(dataInput);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void SerializeExecuteRequest_ValidRequestGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?><wps:Execute xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" service=""WPS"" version=""2.0.0"" mode=""sync"" response=""document"" xmlns:wps=""http://www.opengis.net/wps/2.0""><ows:Identifier>org.n52.wps.server.algorithm.SimpleBufferAlgorithm</ows:Identifier><wps:Input id=""data""><wps:Reference xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xli=""http://www.w3.org/1999/xlink"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xli:href=""http://geoprocessing.demo.52north.org:8080/geoserver/wfs?SERVICE=WFS&amp;VERSION=1.0.0&amp;REQUEST=GetFeature&amp;TYPENAME=topp:tasmania_roads&amp;SRS=EPSG:4326&amp;OUTPUTFORMAT=GML3"" schema=""http://schemas.opengis.net/gml/3.1.1/base/feature.xsd"" xmlns:wps=""http://www.opengis.net/wps/2.0"" /></wps:Input><wps:Input id=""width""><wps:Data><LiteralValue xmlns=""http://www.opengis.net/wps/2.0"">0.05</LiteralValue></wps:Data></wps:Input><wps:Output transmission=""value"" id=""result"" /></wps:Execute>";

            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
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
                            Value = 0.05f
                        }
                    }
                },
                Outputs = new []
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

    }
}