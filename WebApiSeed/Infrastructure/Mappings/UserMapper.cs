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
    public class UserMapper : IObjectMapper
    {
        /// <summary>
        ///     Apply mappings
        /// </summary>
        public void Apply()
        {
            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<UserDto, User>();
        }
    }
}