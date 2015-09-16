namespace WebApiSeed.Common.Helpers
{
    using System;
    using System.Diagnostics;
    using Interfaces;

    /// <summary>
    ///     Trace logging helper
    /// </summary>
    public class LoggingHelper : ILoggingHelper
    {
        /// <summary>
        ///     Trace logging error
        /// </summary>
        /// <param name="exception">Exception to be logged</param>
        public void TraceError(Exception exception)
        {
            Trace.TraceError("[LoggingHelper] Exception: " + exception.Message);
            Trace.TraceError("[LoggingHelper] Stack trace: " + exception.StackTrace);
            if (exception.InnerException == null)
                return;

            Trace.TraceError("[LoggingHelper] Inner exception: " + exception.InnerException.Message);
            Trace.TraceError("[LoggingHelper] Inner exception stack trace: " + exception.InnerException.StackTrace);
        }

        public void TraceError(String module, Exception exception)
        {
            Trace.TraceError("[" + module + "] Exception: " + exception.Message);
            Trace.TraceError("[" + module + "] Stack trace: " + exception.StackTrace);
            if (exception.InnerException == null)
                return;

            Trace.TraceError("[" + module + "] Inner exception: " + exception.InnerException.Message);
            Trace.TraceError("[" + module + "] Inner exception stack trace: " + exception.InnerException.StackTrace);
        }
    }
}