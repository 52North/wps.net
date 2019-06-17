using FluentAssertions;
using System;
using System.Threading.Tasks;
using Wps.Client.Models;
using Wps.Client.Models.Execution;
using Wps.Client.Models.Ows;
using Wps.Client.Models.Requests;
using Wps.Client.Models.Responses;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class SessionTests
    {

        private const string JobId = "test-job-id";

        [Fact]
        public async Task CreateSession_FailedJobGiven_ShouldRaiseFailedEvent()
        {
            ExceptionReport exceptionReport = null;
            var session = new Session<string>(new MockWpsClient(new StatusInfo
            {
                Status = JobStatus.Failed
            }), string.Empty, JobId);

            session.Failed += (sender, args) => { exceptionReport = args.ExceptionReport; };

            await session.StartPolling();

            exceptionReport.Should().NotBeNull();
        }

        [Fact]
        public void CreateSession_RunningJobGiven_ShouldRaisePolledEvent()
        {
            var expectedNextPollDate = new DateTime(20, 1, 20, 20, 20, 20);
            var receivedNextPollDate = DateTime.MinValue;

            var session = new Session<string>(new MockWpsClient(new StatusInfo
            {
                Status = JobStatus.Running,
                NextPollDateTime = expectedNextPollDate
            }), string.Empty, JobId);

            session.Polled += (sender, args) => { receivedNextPollDate = args.NextPollAt; };

            session.StartPolling().Wait(TimeSpan.FromSeconds(4));

            receivedNextPollDate.Should().Be(expectedNextPollDate);
        }

        [Fact]
        public void CreateSession_SucceededJobGiven_ShouldRaiseFinishedEvent()
        {
            Result<LiteralDataValue> resultDocument = null;

            var session = new Session<LiteralDataValue>(new MockWpsClient(new StatusInfo
            {
                Status = JobStatus.Succeeded,
            }), string.Empty, JobId);

            session.Finished += (sender, args) =>
            {
                resultDocument = args.Result;
            };

            session.StartPolling().Wait(TimeSpan.FromSeconds(4));

            resultDocument.JobId.Should().Be(JobId);
            resultDocument.ExpirationDate.Should().NotBe(DateTime.MinValue);
            resultDocument.Outputs.Should().HaveCount(1);
        }

        [Fact]
        public void PollAtNextDate_RunningJobGiven_With_5_SecondInterval_ShouldPollOnceWithin4Seconds()
        {
            var pollCount = 0;

            var session = new Session<LiteralDataValue>(new MockWpsClient(new StatusInfo
            {
                Status = JobStatus.Running,
                NextPollDateTime = DateTime.Now.AddSeconds(5),
            }), string.Empty, JobId);

            session.Polled += (sender, args) => { pollCount++; };

            session.StartPolling().Wait(TimeSpan.FromSeconds(4));

            pollCount.Should().Be(1);
        }

        [Fact]
        public void PollAtNextDate_RunningJobGiven_With_DefaultPollInterval_ShouldPollFourTimesWithin7Seconds()
        {
            var pollCount = 0;

            var statusInfo = new StatusInfo
            {
                Status = JobStatus.Running,
            };

            var session = new Session<LiteralDataValue>(new MockWpsClient(statusInfo), string.Empty, JobId);

            session.Polled += (sender, args) =>
            {
                pollCount++;
            };

            session.StartPolling().Wait(TimeSpan.FromSeconds(7));

            pollCount.Should().Be(4);
        }

        private class MockWpsClient : IWpsClient
        {

            private readonly StatusInfo _jobStatusReturn;

            public MockWpsClient(StatusInfo jobStatusReturn)
            {
                _jobStatusReturn = jobStatusReturn;
            }

            public Task<GetCapabilitiesResponse> GetCapabilities(string wpsUri)
            {
                throw new NotImplementedException();
            }

            public Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params ProcessSummary[] processSummaries)
            {
                throw new NotImplementedException();
            }

            public Task<ProcessOfferingCollection> DescribeProcess(string wpsUri, params string[] processIdentifiers)
            {
                throw new NotImplementedException();
            }

            public Task<ExceptionReport> GetExceptionForRequest(string wpsUri, Request request)
            {
                if (request is GetResultRequest req && req.JobId.Equals(JobId))
                {
                    return Task.FromResult(new ExceptionReport());
                }

                return null;
            }

            public Task<StatusInfo> GetJobStatus(string wpsUri, string jobId)
            {
                return Task.FromResult(_jobStatusReturn);
            }

            public Task<Result<TData>> GetResult<TData>(string wpsUri, string jobId)
            {
                if (jobId.Equals(JobId))
                {
                    return Task.FromResult(new Result<TData>
                    {
                        JobId = JobId,
                        ExpirationDate = DateTime.Now,
                        Outputs = new[]
                        {
                            new ResultOutput<TData>()
                        }
                    });
                }

                throw new InvalidOperationException($"Job id should have been ${JobId}.");
            }

            public Task<string> GetRawResult(string wpsUri, ExecuteRequest request)
            {
                throw new NotImplementedException();
            }

            public Task<Result<TData>> GetDocumentedResult<TData>(string wpsUri, ExecuteRequest request)
            {
                throw new NotImplementedException();
            }

            public Task<Session<TData>> AsyncGetDocumentResultAs<TData>(string wpsUri, ExecuteRequest request)
            {
                throw new NotImplementedException();
            }
        }

    }
}
