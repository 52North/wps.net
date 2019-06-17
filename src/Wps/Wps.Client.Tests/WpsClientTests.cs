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

        private static HttpMessageHandler GetMockedMessageHandlerForResponse(string response, HttpStatusCode code = HttpStatusCode.OK, string expectedRequestContent = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    expectedRequestContent == null ? ItExpr.IsAny<HttpRequestMessage>() : ItExpr.Is<HttpRequestMessage>(
                        m => m.Content.ReadAsStringAsync().Result.Equals(expectedRequestContent)),
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

        /*
         * GetResult Tests
         */

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/Responses/Result.xml")]
        public async Task GetResult_ValidJobIdGiven_ShouldReturnJobStatus(string httpClientResponseXml)
        {
            const string jobId = "6b3ef43a-ed8d-4063-8c65-e1171687f256";
            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(httpClientResponseXml)), new XmlSerializationService());

            var result = await wpsClient.GetResult<LiteralDataValue>(MockUri, jobId);
            result.JobId.Should().Be(jobId);
            result.Outputs.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetResult_NullJobIdGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetJobStatus(MockUri, null));
        }

        [Fact]
        public async Task GetResult_NullWpsUriGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetJobStatus(null, string.Empty));
        }

        /*
         * GetRawResult Tests
         */

        [Fact]
        public async Task GetRawResult_ValidRequestGiven_ShouldReturnResult()
        {
            const string expectedResultXml = "<wps:LiteralValue xmlns:wps=\"http://www.opengis.net/wps/2.0\" dataType=\"https://www.w3.org/2001/XMLSchema-datatypes#string\">150</wps:LiteralValue>";
            
            var request = new ExecuteRequest
            {
                Inputs = new[]
                {
                    new DataInput
                    {
                        Data = new LiteralDataValue{Value = "test"}
                    }
                },
                ExecutionMode = ExecutionMode.Synchronous,
                ResponseType = ResponseType.Raw
            };

            var expectedRequestXml = new XmlSerializationService().Serialize(request);

            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(expectedResultXml, HttpStatusCode.OK, expectedRequestXml)), new XmlSerializationService());

            var result = await wpsClient.GetRawResult(MockUri, request);
            result.Should().Be(expectedResultXml);
        }

        [Fact]
        public async Task GetRawResult_NullWpsUriGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetRawResult(null, new ExecuteRequest()));
        }

        [Fact]
        public async Task GetRawResult_NullRequestGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetRawResult(MockUri, null));
        }

        [Fact]
        public async Task GetRawResult_DocumentedResponseTypeGiven_ShouldThrowInvalidOperationException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<InvalidOperationException>(() => wpsClient.GetRawResult(MockUri, new ExecuteRequest
            {
                ResponseType = ResponseType.Document
            }));
        }

        [Fact]
        public async Task GetRawResult_AsynchronousExecutionModeGiven_ShouldThrowInvalidOperationException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<InvalidOperationException>(() => wpsClient.GetRawResult(MockUri, new ExecuteRequest
            {
                ExecutionMode = ExecutionMode.Asynchronous
            }));
        }
        /*
         * GetDocumentedResult Tests
         */

        [Theory]
        [EmbeddedResourceData("Wps.Client.Tests/Resources/SynchronousDocumentedResult.xml")]
        public async Task GetDocumentedResult_ValidRequestGiven_ShouldReturnResult(string expectedHttpResponse)
        {
            var request = new ExecuteRequest
            {
                Identifier = "org.n52.javaps.test.EchoProcess",
                Inputs = new[]
                {
                    new DataInput
                    {
                        Data = new LiteralDataValue{ Value = "test" }
                    }
                },
                Outputs = new []
                {
                    new DataOutput
                    {
                        MimeType = "text/xml"
                    }
                },
                ExecutionMode = ExecutionMode.Synchronous,
                ResponseType = ResponseType.Document
            };

            var expectedRequestXml = new XmlSerializationService().Serialize(request);

            var wpsClient = new WpsClient(new HttpClient(GetMockedMessageHandlerForResponse(expectedHttpResponse, HttpStatusCode.OK, expectedRequestXml)), new XmlSerializationService());

            var result = await wpsClient.GetDocumentedResult<LiteralDataValue>(MockUri, request);
            result.Should().NotBeNull();
            result.Outputs.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetDocumentedResult_NullWpsUriGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetDocumentedResult<object>(null, new ExecuteRequest()));
        }

        [Fact]
        public async Task GetDocumentedResult_NullRequestGiven_ShouldThrowArgumentNullException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<ArgumentNullException>(() => wpsClient.GetDocumentedResult<object>(MockUri, null));
        }

        [Fact]
        public async Task GetDocumentedResult_RawResponseTypeGiven_ShouldThrowInvalidOperationException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<InvalidOperationException>(() => wpsClient.GetDocumentedResult<object>(MockUri, new ExecuteRequest
            {
                ResponseType = ResponseType.Raw
            }));
        }

        [Fact]
        public async Task GetDocumentedResult_SynchronousExecutionModeGiven_ShouldThrowInvalidOperationException()
        {
            var wpsClient = new WpsClient(new HttpClient(), new XmlSerializationService());

            await Assert.ThrowsAsync<InvalidOperationException>(() => wpsClient.GetDocumentedResult<object>(MockUri, new ExecuteRequest
            {
                ExecutionMode = ExecutionMode.Synchronous
            }));
        }

    }
}

