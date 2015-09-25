namespace WebApiSeed.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Data.Configuration.EF;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiSeedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}