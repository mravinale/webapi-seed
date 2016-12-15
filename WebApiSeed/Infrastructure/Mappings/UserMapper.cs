namespace WebApiSeed.Infrastructure.Mappings
{
    using AutoMapper;

    using Data.Domain;
    using Dtos;

    /// <summary>
    ///     User mappers
    /// </summary>
    public class UserMapper : Profile
    {
        /// <summary>
        ///     Apply mappings
        /// </summary>
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}