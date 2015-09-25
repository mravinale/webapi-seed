namespace WebApiSeed.Infrastructure.Attributes
{
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Filters;
    using Common.Helpers;
    using Dtos;
    using Elmah;

    /// <summary>
    ///     Exception filter attribute for all unhandled exceptions
    /// </summary>
    public class UnhandledExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var loggingHelper = new LoggingHelper();
            loggingHelper.TraceError(context.Exception);
            ErrorLog.GetDefault(HttpContext.Current).Log(new Error(context.Exception));
            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new ErrorDto
                {
                    Code = 0,
                    Error = "Oops, something went wrong!"
                }
                );
        }
    }
}