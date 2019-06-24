using System;
using Wps.Client.Models.Ows;

namespace Wps.Client.Services.Arguments
{
    public class SessionFailedEventArgs : EventArgs
    {
        
        public ExceptionReport ExceptionReport { get; }

        public SessionFailedEventArgs(ExceptionReport exceptionReport)
        {
            ExceptionReport = exceptionReport;
        }

    }
}
