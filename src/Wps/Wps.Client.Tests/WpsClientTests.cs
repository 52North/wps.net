using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Ows;
using Wps.Client.Models.Requests;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class WpsClientTests
    {

        private const string MockUri = "http://mock.uri";

        /*
         * Helpers
         */

        private static HttpMessageHandler GetMockedMessageHandlerForResponse(string response, HttpStatusCode code = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = code,
                    Content = new StringContent(response, Encoding.UTF8)
                })
                .Verifiable();

            return handlerMock.Object;
        }

        /*
         * Get Capabilities Tests
         */

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/GetCapabilities.xml")]
        public async Task GetCapabilities_ValidUriGiven_ShouldPass(string httpClientResponseXml)
        {
            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml)), new XmlSerializationService());

            var capabilities = await wpsClient.GetCapabilities(MockUri);
            capabilities.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCapabilities_NullUriGiven_ShouldThrow_ArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());
            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetCapabilities(null));
        }

        /*
         * Describe Process Tests
         */

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/ExceptionReport.xml")]
        public async Task DescribeProcess_InvalidStringIdentifiersGiven_ShouldThrowOwsException(string httpClientResponseXml)
        {
            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml, HttpStatusCode.BadRequest)), new XmlSerializationService());

            await Assert.ThrowsAsync<OwsException>(() => wpsClient.DescribeProcess(MockUri, "invalid 1", "invalid 2"));
        }

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/ExceptionReport.xml")]
        public async Task DescribeProcess_InvalidSummariesGiven_ShouldThrowOwsException(string httpClientResponseXml)
        {
            var summaries = new[]
            {
                new ProcessSummary {Identifier = "invalid 1"},
                new ProcessSummary {Identifier = "invalid 2"}
            };

            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml, HttpStatusCode.BadRequest)), new XmlSerializationService());

            await Assert.ThrowsAsync<OwsException>(() => wpsClient.DescribeProcess(MockUri, summaries));
        }

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/ProcessOfferings.xml")]
        public async Task DescribeProcess_ValidStringIdentifiersGiven_ShouldReturnTwoProcesses(string httpClientResponseXml)
        {
            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml)), new XmlSerializationService());

            var collection = await wpsClient.DescribeProcess(MockUri, "i.gensigset", "i.gensig");
            collection.Should().HaveCount(2);
        }

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/ProcessOfferings.xml")]
        public async Task DescribeProcess_ValidSummariesGiven_ShouldReturnTwoProcesses(string httpClientResponseXml)
        {
            var summaries = new[]
            {
                new ProcessSummary {Identifier = "i.gensigset"},
                new ProcessSummary {Identifier = "i.gensig"}
            };

            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml)), new XmlSerializationService());

            var collection = await wpsClient.DescribeProcess(MockUri, summaries);
            collection.Should().HaveCount(2);
        }

        [Fact]
        public async Task DescribeProcess_NullUriGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.DescribeProcess(null, string.Empty));
        }

        [Fact]
        public async Task DescribeProcess_EmptyIdentifiersGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<InvalidOperationException>(() => wpsClient.DescribeProcess(MockUri, new string[0]));
        }

        /*
         * GetJobStatus Tests
         */

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/JobStatus.xml")]
        public async Task GetStatus_ValidJobIdGiven_ShouldReturnJobStatus(string httpClientResponseXml)
        {
            const string expectedJobId = "FB6DD4B0-A2BB-11E3-A5E2-0800200C9A66";
            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml)), new XmlSerializationService());

            var status = await wpsClient.GetJobStatus(MockUri, expectedJobId);
            status.Should().NotBeNull();
            status.JobId.Should().Be(expectedJobId);
        }

        [Fact]
        public async Task GetStatus_NullJobIdGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetJobStatus(MockUri, null));
        }

        [Fact]
        public async Task GetStatus_NullWpsUriGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetJobStatus(null, string.Empty));
        }

    }
}

