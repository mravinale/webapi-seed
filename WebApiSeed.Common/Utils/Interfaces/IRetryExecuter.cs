namespace WebApiSeed.Common.Utils.Interfaces
{
    using System;

    public interface IRetryExecuter
    {
        TResult WithExponentialBackoff<TResult>(Func<TResult> action, int maxRetries);
    }
}