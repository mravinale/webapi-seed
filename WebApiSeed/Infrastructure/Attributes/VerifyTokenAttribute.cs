using System.Diagnostics;

namespace WebApiSeed.Infrastructure.Attributes
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Data.Repositories.Interfaces;
    using Dtos;
    using Helpers.Interfaces;
    using Ioc;
    using Resources;

    /// <summary>
    ///     Verify token
    /// </summary>
    public class VerifyTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var container =
                ((WindsorDependencyResolver) (filterContext.RequestContext.Configuration.DependencyResolver)).Container;
            var userRepository = container.Resolve<IUserRepository>();
            var securityHelper = container.Resolve<ISecurityHelper>();
            var token = filterContext.Request.Headers.Authorization.Parameter;
            var userId = securityHelper.GetUserIdForToken(token);
            var user = userRepository.FindUserById(userId);
            if (user.AccessToken != token)
            {
                Trace.TraceError("[VerifyTokenAttribute] Invalid Access Token: " + token);
                filterContext.Response = filterContext.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new ErrorDto
                    {
                        Code = int.Parse(ControllerErrorCodes.TokenMismatch),
                        Error = ControllersErrorMessages.TokenMismatch
                    }
                    );
            }
        }
    }
}