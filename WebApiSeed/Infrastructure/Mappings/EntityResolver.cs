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
    public class EntityResolver<T> : IValueResolver<int, T, T> where T : BaseEntity
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
        public T Resolve(int source, T destination, T destMember, ResolutionContext context)
        {
            return _dbContext.Entity<T>().FirstOrDefault(x => x.Id == source);
        }
    }
}