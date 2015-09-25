namespace WebApiSeed
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Infrastructure.Attributes;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     Web API configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     Register services and routes
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );

            config.Filters.Add(new UnhandledExceptionAttribute());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}