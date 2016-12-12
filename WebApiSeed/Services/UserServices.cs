namespace WebApiSeed.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Device.Location;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using AutoMapper;
    using Common.Extensions;
    using Common.Helpers.Interfaces;
    using Data.Domain;
    using Data.Repositories.Interfaces;
    using Dtos;
    using Infrastructure.Helpers.Interfaces;
    using Interfaces;
    using Resources;
    using Common;
    using Common.User;

    /// <summary>
    ///     User services
    /// </summary>
    public class UserServices : IUserServices
    {
        #region Constructor

        private readonly IMapper _mapperEngine;
        private readonly ISecurityHelper _securityHelper;
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userRepository">User repository</param>
        /// <param name="mapperEngine">Mapper engine</param>
        /// <param name="securityHelper">Security helper</param>
        public UserServices(IUserRepository userRepository, IMapper mapperEngine,
            ISecurityHelper securityHelper)
        {
            _userRepository = userRepository;
            _mapperEngine = mapperEngine;
            _securityHelper = securityHelper;
        }

        #endregion

        #region CRUD

        /// <summary>
        ///     Retrieve all users in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var userDtos = _mapperEngine.Map<IList<User>, IList<UserDto>>(users);
            return userDtos;
        }

        /// <summary>
        ///     Retrieve a user based on its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserDto</returns>
        public UserDto GetUserById(int userId)
        {
            var user = _userRepository.FindUserById(userId);
            return user != null ? _mapperEngine.Map<User, UserDto>(user) : null;
        }


        /// <summary>
        ///     Update user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public ServiceResult<UserDto, UserServiceResult> UpdateUser(UserDto userDto)
        {
            var user = _mapperEngine.Map<UserDto, User>(userDto);

            //Don't allow user creation here
            var existingUser = _userRepository.FindUserById(userDto.Id);
            if (existingUser == null)
                return null;

            #region Update only those fields that are not null in the DTO

            if (userDto.UserName != null)
            {
                var usernameExists = _userRepository.FindUserByUserName(userDto.UserName);
                if (usernameExists != null && usernameExists.Id != userDto.Id)
                {
                    return new ServiceResult<UserDto, UserServiceResult>
                    {
                        Result = new UserServiceResult { Enum = UserServiceResultEnum.UsernameExists },
                        Dto = _mapperEngine.Map<User, UserDto>(existingUser)
                    };
                }

                existingUser.UserName = user.UserName;
            }

            if (userDto.Gender != null)
                existingUser.Gender = user.Gender;

            #endregion

            _userRepository.SaveOrUpdateUser(existingUser);

            return new ServiceResult<UserDto, UserServiceResult>
            {
                Result = new UserServiceResult { Enum = UserServiceResultEnum.Success },
                Dto = _mapperEngine.Map<User, UserDto>(existingUser)
            };
        }

        /// <summary>
        ///     Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            var user = _userRepository.FindUserById(id);
            if (user == null) return false;

            _userRepository.DeleteUser(user.Id);
            return true;
        }

        /// <summary>
        ///     Retrieve a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public UserDto GetUserByUsername(string username)
        {
            var user = _userRepository.FindUserByUserName(username);
            return user != null ? _mapperEngine.Map<User, UserDto>(user) : null;
        }

        #endregion


        /// <summary>
        ///     Logs out a user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>Update user object</returns>
        public UserDto LogoutUser(int userId)
        {
            var user = _userRepository.FindUserById(userId);
            user.AccessToken = String.Empty;
            _userRepository.SaveOrUpdateUser(user);

            return _mapperEngine.Map<User, UserDto>(user);
        }

    }
}