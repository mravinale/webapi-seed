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
    public class ManyToManyEntityResolver<T, U> : IValueResolver<IEnumerable<T>, IEnumerable<U>, IEnumerable<U>>
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
        public IEnumerable<U> Resolve(IEnumerable<T> source, IEnumerable<U> destination, IEnumerable<U> destMember, ResolutionContext context)
        {
            return source.Select(item => _dbContext.Entity<U>().FirstOrDefault(x => x.Id == item.Id));
        }
    }
}