using System;
using System.Threading;
using System.Threading.Tasks;
using Wps.Client.Models;
using Wps.Client.Models.Requests;
using Wps.Client.Services.Arguments;
using Task = System.Threading.Tasks.Task;

namespace Wps.Client.Services
{
    public class Session<TData>
    {

        /// <summary>
        /// Event raised when the session has completed successfully.
        /// </summary>
        public event EventHandler<SessionFinishedEventArgs<TData>> Finished;

        /// <summary>
        /// Event raised when there was error during the execution.
        /// </summary>
        public event EventHandler<SessionFailedEventArgs> Failed;

        /// <summary>
        /// Event raised when the session polling system has polled the server for the job status.
        /// </summary>
        public event EventHandler<SessionPolledEventArgs> Polled;

        /// <summary>
        /// This is the interval that the next polling is going to occur if no next date polling has been given from the server.
        /// </summary>
        private const double DefaultNextPollInterval = 2.0;

        public string JobId { get; }
        public bool IsRunning { get; private set; }

        private readonly IWpsClient _wpsClient;
        private readonly string _wpsUri;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private DateTime _nextPoll;
        private DateTime _lastPoll;

        public Session(IWpsClient wpsClient, string wpsUri, string jobId)
        {
            _wpsClient = wpsClient ?? throw new ArgumentNullException(nameof(wpsClient));
            _wpsUri = wpsUri ?? throw new ArgumentNullException(nameof(_wpsUri));
            JobId = jobId ?? throw new ArgumentNullException(nameof(jobId));

            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        /// <summary>
        /// Start the indefinitely long polling task. It is ran in LongRunning mode which ends up creating a new separate thread for the task.
        /// </summary>
        /// <returns>The task containing the polling calls</returns>
        public Task StartPolling()
        {
            IsRunning = true;
            return Task.Factory.StartNew(() =>
                {
                    do
                    {
                        var jobStatus = _wpsClient.GetJobStatus(_wpsUri, JobId).Result;
                        switch (jobStatus.Status)
                        {
                            case JobStatus.Running:
                                if (jobStatus.NextPollDateTime.HasValue)
                                {
                                    _nextPoll = jobStatus.NextPollDateTime.Value;
                                }
                                else
                                {
                                    _nextPoll = DateTime.Now + TimeSpan.FromSeconds(DefaultNextPollInterval);
                                }

                                _lastPoll = DateTime.Now;
                                Polled?.Invoke(this, new SessionPolledEventArgs(DateTime.Now, _nextPoll));
                                break;
                            case JobStatus.Succeeded:
                                var result = _wpsClient.GetResult<TData>(_wpsUri, JobId).Result;

                                Finished?.Invoke(this, new SessionFinishedEventArgs<TData>(result));
                                _cancellationTokenSource.Cancel();
                                break;
                            case JobStatus.Failed:
                                var exceptionReport = _wpsClient.GetExceptionForRequest(_wpsUri, new GetResultRequest
                                {
                                    JobId = JobId
                                }).Result;

                                Failed?.Invoke(this, new SessionFailedEventArgs(exceptionReport));
                                _cancellationTokenSource.Cancel();
                                break;
                        }

                        if (!_cancellationTokenSource.IsCancellationRequested)
                        {
                            if(_nextPoll > _lastPoll)
                            {
                                Task.Delay(_nextPoll - _lastPoll).Wait();
                            }
                        }

                    } while (!_cancellationTokenSource.Token.IsCancellationRequested);

                    IsRunning = false;
                }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

    }
}
