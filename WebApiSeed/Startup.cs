using Microsoft.Owin;
using WebApiSeed;

[assembly: OwinStartup(typeof (Startup))]

namespace WebApiSeed
{
    using System;
    using System.Data.Entity;
    using System.Web.Http;
    using Castle.Windsor;
    using Data.Configuration.EF;
    using Infrastructure.Ioc;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using Swashbuckle.Application;

    /// <summary>
    ///     Startup class
    /// </summary>
    public class Startup
    {
        private static IWindsorContainer _container;

        /// <summary>
        ///     OAuth Options
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        ///     Windsor container
        /// </summary>
        public IWindsorContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        ///     App configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            _container = Bootstrapper.InitializeContainer();

            var config = new HttpConfiguration
            {
                DependencyResolver = new WindsorDependencyResolver(_container)
            };

            ConfigureOAuth(app);

            WebApiConfig.Register(config);

#if (DEBUG)
            {
                config.EnableSwagger(c =>
                {
                    c.IncludeXmlComments(String.Format(@"{0}\bin\WebApiSeed.XML", AppDomain.CurrentDomain.BaseDirectory));
                    c.SingleApiVersion("v1", "Web Api Seed");
                }).EnableSwaggerUi();
            }
#endif

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WebApiSeedDbContext>());
        }

        /// <summary>
        ///     oAuth configuration
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365)
            };

            // Token Generation
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}