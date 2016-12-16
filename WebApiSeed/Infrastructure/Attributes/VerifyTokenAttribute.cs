using System.Diagnostics;

namespace WebApiSeed.Infrastructure.Attributes
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Dtos;
    using Helpers.Interfaces;
    using Autofac;
    using Resources;
    using Services.Interfaces;

    /// <summary>
    ///     Verify token
    /// </summary>
    public class VerifyTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var container = new ContainerBuilder().Build();
            var userServices = container.Resolve<IUserServices>();
            var securityHelper = container.Resolve<ISecurityHelper>();
            var token = filterContext.Request.Headers.Authorization.Parameter;
            var userId = securityHelper.GetUserIdForToken(token);
            var user = userServices.FindUserById(userId);

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