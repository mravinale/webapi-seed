namespace WebApiSeed.Common.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Helpers.Interfaces;
    using Interfaces;

    public class RetryExecuter : IRetryExecuter
    {
        private readonly ILoggingHelper _loggingHelper;

        public RetryExecuter(ILoggingHelper loggingHelper)
        {
            _loggingHelper = loggingHelper;
        }

        /// <summary>
        ///     Executes an action and if it throws an exception, retries using exponential backoff policy
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="maxRetries">Maximum number of attemps to be made</param>
        /// <returns>A TResult indicating if the operation could be performed</returns>
        public TResult WithExponentialBackoff<TResult>(Func<TResult> action, int maxRetries)
        {
            var retry = 0;
            var success = default(TResult);

            while (EqualityComparer<TResult>.Default.Equals(success, default(TResult)) && retry < maxRetries)
            {
                if (retry > 0)
                {
                    //if it's not the first attemp, wait
                    var retryWait = Convert.ToInt32(Math.Pow(2, retry)*1000);
                    Trace.WriteLine(
                        String.Format("Retry.WithExponentialBackoff Waiting {0} milliseconds to retry method '{1}'",
                            retryWait, action.Method.Name));

                    Task.Delay(retryWait).Wait();
                }

                try
                {
                    success = action();
                }
                catch (Exception ex)
                {
                    success = default(TResult);
                    _loggingHelper.TraceError("Retry.WithExponentialBackoff", ex);
                }

                retry++;
            }

            if (retry == maxRetries)
            {
                Trace.WriteLine(
                    String.Format("Retry.WithExponentialBackoff Method '{0}' exited after reaching max retries ({1})",
                        action.Method.Name, maxRetries));
            }

            return success;
        }
    }
}