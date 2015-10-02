namespace WebApiSeed.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Net.Http;
    using Dtos;
    using Common;
    using Common.User;

    /// <summary>
    ///     User services interface
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        ///     Retrieve all database users
        /// </summary>
        /// <returns>List of User Dtos</returns>
        IEnumerable<UserDto> GetAllUsers();

        /// <summary>
        ///     Retrieve a user based on its ID
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User Dto</returns>
        UserDto GetUserById(int userId);

        /// <summary>
        ///     Update user information
        /// </summary>
        /// <param name="userDto">User Dto</param>
        /// <returns>Updated User Dto</returns>
        ServiceResult<UserDto, UserServiceResult> UpdateUser(UserDto userDto);

        /// <summary>
        ///     Delete a user from the database
        /// </summary>
        /// <param name="id"></param>
        bool DeleteUser(int id);

        /// <summary>
        ///     Logs out a user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>Update user object</returns>
        UserDto LogoutUser(int userId);

        /// <summary>
        ///     Create user information
        /// </summary>
        /// <param name="userDto">User Dto</param>
        /// <returns>New User Dto</returns>
        ServiceResult<UserDto, UserServiceResult> CreateUser(UserDto userDto);
    }
}