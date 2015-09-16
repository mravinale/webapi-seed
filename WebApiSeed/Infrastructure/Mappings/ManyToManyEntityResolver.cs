namespace WebApiSeed.Infrastructure.Mappings
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Configuration.EF.Interfaces;
    using Data.Domain;
    using Dtos;

    /// <summary>
    ///     Many to many entity resolver
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ManyToManyEntityResolver<T, U> : ValueResolver<IEnumerable<T>, IEnumerable<U>>
        where T : BaseDto //Dto
        where U : BaseEntity //EF entities
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="dbContext"></param>
        public ManyToManyEntityResolver(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Resolve core method
        /// </summary>
        protected override IEnumerable<U> ResolveCore(IEnumerable<T> source)
        {
            return source.Select(item => _dbContext.Entity<U>().FirstOrDefault(x => x.Id == item.Id));
        }
    }
}