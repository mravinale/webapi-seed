namespace WebApiSeed.Infrastructure.Mappings
{
    using System.Linq;
    using AutoMapper;
    using Data.Configuration.EF.Interfaces;
    using Data.Domain;

    /// <summary>
    ///     Entity Resolver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityResolver<T> : ValueResolver<int, T> where T : BaseEntity
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public EntityResolver(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Resolve core
        /// </summary>
        protected override T ResolveCore(int source)
        {
            return _dbContext.Entity<T>().FirstOrDefault(x => x.Id == source);
        }
    }
}