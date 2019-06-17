using System;

namespace Wps.Client.Services.Arguments
{
    public class SessionPolledEventArgs : EventArgs
    {
        
        public DateTime PolledAt { get; }
        public DateTime NextPollAt { get; }

        public SessionPolledEventArgs(DateTime polledAt, DateTime nextPollAt)
        {
            PolledAt = polledAt;
            NextPollAt = nextPollAt;
        }

    }
}
