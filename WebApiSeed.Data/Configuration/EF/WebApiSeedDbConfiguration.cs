namespace WebApiSeed.Data.Configuration.EF
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    public class WebApiSeedDbConfiguration : DbConfiguration
    {
        private readonly int _retryCount = Convert.ToInt32(ConfigurationManager.AppSettings["SqlAzureStrategyRetryCount"]);
        private readonly int _retryDelay = Convert.ToInt32(ConfigurationManager.AppSettings["SqlAzureStrategyRetryDelay"]);

        public WebApiSeedDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(_retryCount, TimeSpan.FromSeconds(_retryDelay)));
        }
    }
}