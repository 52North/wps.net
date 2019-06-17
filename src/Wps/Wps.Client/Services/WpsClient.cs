using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Ows;
using Wps.Client.Models.Requests;
using Wps.Client.Models.Responses;

namespace Wps.Client.Services
{
    public class WpsClient : IWpsClient, IDisposable
    {

        private readonly HttpClient _httpClient;
        private readonly IXmlSerializer _serializationService;

        public WpsClient(HttpClient httpClient, IXmlSerializer serializationService)
        {
            _httpClient = httpClient;
            _serializationService = serializationService;
        }

        private async Task<string> GetRequestResult(string wpsUri, Request request)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var requestXml = _serializationService.Serialize(request);
            var response = await _httpClient.PostAsync(wpsUri,
                new StringContent(requestXml, Encoding.UTF8, "text/xml"));

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            var exceptionReport = _serializationService.Deserialize<ExceptionReport>(content);
            var exception = exceptionReport.Exceptions.FirstOrDefault();
            if (exception != null)
            {
                throw exception;
            }

            throw new Exception("Could not get a valid response from the WPS server.");
        }

        public async Task<GetCapabilitiesResponse> GetCapabilities(string wpsUri)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));

            var request = new GetCapabilitiesRequest();

            var content = await GetRequestResult(wpsUri, request);
            var result = _serializationService.Deserialize<GetCapabilitiesResponse>(content);

            return result;
        }

        public Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params ProcessSummary[] processSummaries)
        {
            return DescribeProcess(wpsUri, processSummaries.Select(ps => ps.Identifier).ToArray());
        }

        public async Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params string[] processIdentifiers)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (processIdentifiers == null) throw new ArgumentNullException(nameof(processIdentifiers));

            if (!processIdentifiers.Any()) throw new InvalidOperationException("You cannot get the description of an empty list of identifiers.");

            var request = new DescribeProcessRequest
            {
                Identifiers = processIdentifiers
            };

            var content = await GetRequestResult(wpsUri, request);
            var result = _serializationService.Deserialize<ProcessOfferingCollection>(content);

            return result;
        }

        public async Task<StatusInfo> GetJobStatus(string wpsUri, string jobId)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (jobId == null) throw new ArgumentNullException(nameof(jobId));

            var request = new GetStatusRequest
            {
                JobId = jobId
            };

            var content = await GetRequestResult(wpsUri, request);
            var result = _serializationService.Deserialize<StatusInfo>(content);

            return result;
        }

        public async Task<Result<TData>> GetResult<TData>(string wpsUri, string jobId)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (jobId == null) throw new ArgumentNullException(nameof(jobId));

            var request = new GetResultRequest
            {
                JobId = jobId
            };

            var content = await GetRequestResult(wpsUri, request);
            var result =_serializationService.Deserialize<Result<TData>>(content);

            return result;
        }

        public async Task<string> GetRawResult(string wpsUri, ExecuteRequest request)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.ExecutionMode != ExecutionMode.Synchronous) throw new InvalidOperationException("Cannot execute request as auto or asynchronous in a synchronous session.");
            if (request.ResponseType != ResponseType.Raw) throw new InvalidOperationException($"Raw response type is required for the request in function {nameof(GetRawResult)}.");

            var content = await GetRequestResult(wpsUri, request);
            return content;
        }

        public async Task<Result<TData>> GetDocumentedResult<TData>(string wpsUri, ExecuteRequest request)
        {
            if (wpsUri == null) throw new ArgumentNullException(nameof(wpsUri));
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.ExecutionMode != ExecutionMode.Synchronous) throw new InvalidOperationException("Cannot execute request as auto or asynchronous in a synchronous session.");
            if (request.ResponseType != ResponseType.Document) throw new InvalidOperationException($"Document response type is required for the request in function {nameof(GetDocumentedResult)}.");

            var content = await GetRequestResult(wpsUri, request);
            var result = _serializationService.Deserialize<Result<TData>>(content);

            return result;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
