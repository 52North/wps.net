using FluentAssertions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class XmlSerializationServiceTests : IClassFixture<XmlSerializationService>
    {

        private readonly XmlSerializationService _serializer;

        public XmlSerializationServiceTests(XmlSerializationService serializer)
        {
            _serializer = serializer;
        }

        [Fact]
        public void SerializeObject_ValidObjectGiven_ShouldPass()
        {
            const string expectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <TestObject xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                            <FirstProperty>Test</FirstProperty>
                                            <SecondProperty>10</SecondProperty>
                                        </TestObject>";
            
            // Remove white spaces and new line characters. They do not change the actual (de)serialization of the XML.
            var trimmedExpectedXml = Regex.Replace(expectedXml, @"\s+", string.Empty);

            var obj = new TestObject
            {
                FirstProperty = "Test",
                SecondProperty = 10
            };

            var resultXml = _serializer.Serialize(obj);
            var trimmedResult = Regex.Replace(resultXml, @"\s+", string.Empty);
            trimmedResult.Should().Be(trimmedExpectedXml);
        }

        [Fact]
        public void DeserializeObject_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <TestObject xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                            <FirstProperty>Test</FirstProperty>
                                            <SecondProperty>10</SecondProperty>
                                        </TestObject>";

            var resultObject = _serializer.Deserialize<TestObject>(xml);
            resultObject.FirstProperty.Should().Be("Test");
            resultObject.SecondProperty.Should().Be(10);
        }

        [XmlRoot("TestObject")]
        public class TestObject
        {
            [XmlElement("FirstProperty")]
            public string FirstProperty { get; set; }

            [XmlElement("SecondProperty")]
            public int SecondProperty { get; set; }
        }

    }
}
