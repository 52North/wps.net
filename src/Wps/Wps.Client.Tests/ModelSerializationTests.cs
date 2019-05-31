using FluentAssertions;
using System.Text.RegularExpressions;
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

    }
}
