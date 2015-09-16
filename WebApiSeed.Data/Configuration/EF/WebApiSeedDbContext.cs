namespace WebApiSeed.Data.Configuration.EF
{
    using System.Data.Entity;
    using Domain;
    using Domain.Mappings;
    using Interfaces;

    public class WebApiSeedDbContext : DbContext, IDbContext
    {
        public WebApiSeedDbContext(IDatabaseInitializer<WebApiSeedDbContext> dbInitializer)
            : base("WebApiSeedDb")
        {
            Database.SetInitializer(dbInitializer);
        }

        public WebApiSeedDbContext(string connectionString, IDatabaseInitializer<WebApiSeedDbContext> dbInitializer)
            : base(connectionString)
        {
            Database.SetInitializer(dbInitializer);
        }

        private IDbSet<User> Users { get; set; }

        public virtual IDbSet<T> Entity<T>() where T : class
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}