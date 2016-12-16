namespace WebApiSeed.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using Data.Domain;

    using Dtos;

    using Infrastructure.Helpers.Interfaces;

    using Interfaces;

    using Common;
    using Common.User;

    using Data.Configuration.EF.Interfaces;

    /// <summary>
    ///     User services
    /// </summary>
    public class UserServices : IUserServices
    {
        #region Constructor

        private readonly IMapper _mapperEngine;
        private readonly ISecurityHelper _securityHelper;
        private readonly IDbContext _dbContext;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="dbContext">dbContext</param>
        /// <param name="mapperEngine">Mapper engine</param>
        /// <param name="securityHelper">Security helper</param>
        public UserServices(IDbContext dbContext, IMapper mapperEngine, ISecurityHelper securityHelper)
        {
            _dbContext = dbContext;
            _mapperEngine = mapperEngine;
            _securityHelper = securityHelper;
        }

        #endregion

        #region CRUD

        public User FindUserById(int id)
        {
            return _dbContext.Entity<User>().FirstOrDefault(u => u.Id == id);
        }

        public User FindUserByUserName(string userName)
        {
            return _dbContext.Entity<User>().FirstOrDefault(user => user.UserName != null && user.UserName.ToUpper() == userName.ToUpper());
        }

        public IList<User> GetAllUsers()
        {
            return _dbContext.Entity<User>().ToList();
        }

        /// <summary>
        ///     Retrieve all users in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDto> GetAllUsersDto()
        {
            var users = GetAllUsers();
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
            var user = FindUserById(userId);

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
            var existingUser = FindUserById(userDto.Id);

            if (existingUser == null)
            {
                return null;
            }

            #region Update only those fields that are not null in the DTO

            if (userDto.UserName != null)
            {
                var usernameExists = FindUserByUserName(userDto.UserName.ToUpper());

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
            {
                existingUser.Gender = user.Gender;
            }

            #endregion

            SaveOrUpdateUser(existingUser);

            return new ServiceResult<UserDto, UserServiceResult>
            {
                Result = new UserServiceResult { Enum = UserServiceResultEnum.Success },
                Dto = _mapperEngine.Map<User, UserDto>(existingUser)
            };
        }

        public void SaveOrUpdateUser(User user)
        {
            if (user.Id != 0)
            {
                var existingUser = FindUserById(user.Id);

                if (existingUser == null)
                {
                    return;
                }

                //Update scalar values
                if (user.AccessToken != null)
                {
                    existingUser.AccessToken = user.AccessToken;
                }

                if (user.UserName != null)
                {
                    existingUser.UserName = user.UserName;
                }

                if (user.Gender != null)
                {
                    existingUser.Gender = user.Gender;
                }

                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.Entity<User>().Add(user);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        ///     Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            var user = FindUserById(id);

            if (user == null)
            {
                return false;
            }

            _dbContext.Entity<User>().Remove(user);
            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        ///     Retrieve a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public UserDto GetUserByUsername(string username)
        {
            var user = FindUserByUserName(username);

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
            var user = FindUserById(userId);

            user.AccessToken = string.Empty;

            SaveOrUpdateUser(user);

            return _mapperEngine.Map<User, UserDto>(user);
        }
    }
}