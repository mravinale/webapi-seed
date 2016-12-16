namespace WebApiSeed.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Dtos;

    using Infrastructure.Helpers.Interfaces;

    using Resources;

    using Services.Interfaces;

    using Common.User;

    /// <summary>
    ///     Users Controller
    /// </summary>
    [RoutePrefix("api/v1/User")]
    [Authorize]
    public class UsersController : ApiController
    {

        #region Constructor

        private readonly ISecurityHelper _securityHelper;
        private readonly IUserServices _userServices;

        /// <summary>
        ///     Users controller constructor
        /// </summary>
        /// <param name="userServices">User services</param>
        /// <param name="securityHelper">Security helper</param>
        public UsersController(IUserServices userServices, 
            ISecurityHelper securityHelper)
        {
            _userServices = userServices;
            _securityHelper = securityHelper;
        }

        #endregion

        /// <summary>
        ///     Retrieve a user by its ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Http status code</returns>
        /// <response code="200">User found and returned</response>
        /// <response code="404">User not found</response>
        [Route("{id}")]
        [ResponseType(typeof (UserDto))]
        [HttpGet]
        //[VerifyToken]
        public HttpResponseMessage Get(int id)
        {
            var user = _userServices.GetUserById(id);
            return user == null 
                ? Request.CreateResponse(HttpStatusCode.NotFound, new ErrorDto())
                : Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        ///     Retrieve all users
        /// </summary>
        /// <returns>List of User Dtos</returns>
        /// <response code="200">User found and returned</response>
        /// <response code="404">User not found</response>
        [Route("")]
        [ResponseType(typeof (IEnumerable<UserDto>))]
        [HttpGet]
        [AllowAnonymous]
        //[VerifyToken]
        public HttpResponseMessage Get()
        {
            var users = _userServices.GetAllUsers();

            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        /// <summary>
        ///     Updates information for a user
        /// </summary>
        /// <param name="userDto">User Dto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">User found and returned</response>
        /// <response code="500">Internal server error</response>
        [Route("")]
        [ResponseType(typeof (UserDto))]
        [HttpPut]
        //[VerifyToken]
        public HttpResponseMessage Put(UserDto userDto)
        {
            var result = _userServices.UpdateUser(userDto);
            switch (result.Result.Enum)
            {
                case UserServiceResultEnum.UsernameExists:
                    return Request.CreateResponse(HttpStatusCode.Conflict, new ErrorDto
                    {
                        Code = int.Parse(ControllerErrorCodes.UsernameExists),
                        Error = ControllersErrorMessages.UsernameExists
                    });
                case UserServiceResultEnum.Success:
                    return Request.CreateResponse(HttpStatusCode.OK, result.Dto);
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, userDto);
            }
        }

        /// <summary>
        ///     Delete a user from the database
        /// </summary>
        /// <param name="id">User id to delete</param>
        /// <returns></returns>
        /// <response code="204">User deleted</response>
        /// <response code="500">Internal server error</response>
        [Route("{id}")]
        [ResponseType(typeof (UserDto))]
        [HttpDelete]
        //[VerifyToken]
        public HttpResponseMessage Delete(int id)
        {
            Trace.WriteLine("[UserController] Deleting a user.");
            var user = _userServices.GetUserById(id);
            _userServices.DeleteUser(id);
            return Request.CreateResponse(HttpStatusCode.NoContent, user);
        }

        
    }
}