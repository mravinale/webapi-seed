namespace WebApiSeed.Common.Helpers.Interfaces
{
    using System;

    public interface ILoggingHelper
    {
        void TraceError(Exception exception);

        void TraceError(String module, Exception exception);
    }
}