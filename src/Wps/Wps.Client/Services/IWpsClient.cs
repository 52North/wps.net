using System;
using System.Threading.Tasks;
using Wps.Client.Models;
using Wps.Client.Models.Requests;
using Wps.Client.Models.Responses;

namespace Wps.Client.Services
{
    public interface IWpsClient
    {

        /// <summary>
        /// Get the capabilities of the WPS server.
        /// </summary>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <returns></returns>
        Task<GetCapabilitiesResponse> GetCapabilities(string wpsUri);

        /// <summary>
        /// Get the process offerings that a process is capable of.
        /// </summary>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="processSummaries">List of process summaries having a valid identifier.</param>
        /// <returns>A collection of the processes offered.</returns>
        Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params ProcessSummary[] processSummaries);

        /// <summary>
        /// Get the process offerings that a process is capable of.
        /// </summary>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="processIdentifiers">List of valid identifiers for the processes.</param>
        /// <returns>A collection of the processes offered.</returns>
        Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params string[] processIdentifiers);

        /// <summary>
        /// Get the status of an ongoing job.
        /// </summary>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="jobId">The id of the job to be checked.</param>
        /// <returns>Detailed information about the job status.</returns>
        Task<StatusInfo> GetJobStatus(string wpsUri, string jobId);

        /// <summary>
        /// Get the result of an asynchronously executed job.
        /// </summary>
        /// <typeparam name="TData">The type of the data included in the result.</typeparam>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="jobId">The id of the job to be whose result should be fetched.</param>
        /// <returns>The document containing additional information about the result and the output data.</returns>
        Task<Result<TData>> GetResult<TData>(string wpsUri, string jobId);

        /// <summary>
        /// Get the raw result of a synchronously executed request.
        /// </summary>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="request">The execution request to be sent to the WPS server.</param>
        /// <returns>The raw string result.</returns>
        Task<string> GetRawResult(string wpsUri, ExecuteRequest request);

        /// <summary>
        /// Get the documented result of a synchronously executed request.
        /// </summary>
        /// <typeparam name="TData">The type of the data included in the result.</typeparam>
        /// <param name="wpsUri">The address pointing to the WPS server.</param>
        /// <param name="request">The execution request to be sent to the WPS server.</param>
        /// <returns>The document containing additional information about the result and the output data.</returns>
        Task<Result<TData>> GetDocumentedResult<TData>(string wpsUri, ExecuteRequest request);

    }
}
