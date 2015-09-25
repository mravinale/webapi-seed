namespace WebApiSeed.Data.Configuration.EF
{
    using System.Data.Entity.Infrastructure;

    public class WebApiSeedDbContextFactory : IDbContextFactory<WebApiSeedDbContext>
    {
        public WebApiSeedDbContext Create()
        {
            var _dbContext = new WebApiSeedDbContext(new WebApiSeedDbInitializer());

            return _dbContext;
        }
    }
}