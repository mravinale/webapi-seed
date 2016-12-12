namespace WebApiSeed.Infrastructure.Mappings
{
    using System;
    using System.Configuration;
    using AutoMapper;
    using Common.Extensions;
    using Data.Domain;
    using Dtos;
    using IObjectMapper = Automapper.IObjectMapper;

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