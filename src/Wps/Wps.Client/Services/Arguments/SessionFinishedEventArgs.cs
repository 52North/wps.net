using System;
using Wps.Client.Models;

namespace Wps.Client.Services.Arguments
{
    public class SessionFinishedEventArgs<TData> : EventArgs
    {

        public Result<TData> Result { get; }

        public SessionFinishedEventArgs(Result<TData> result)
        {
            Result = result;
        }

    }
}
