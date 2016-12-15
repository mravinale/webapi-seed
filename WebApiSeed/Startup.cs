using Microsoft.Owin;
using WebApiSeed;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApiSeed
{
    using System;
    using System.Data.Entity;
    using System.Web.Http;

    using Data.Configuration.EF;

    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Infrastructure.Automapper;

    /// <summary>
    ///     Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The container
        /// </summary>
        private static IContainer _container;

        /// <summary>
        ///     OAuth Options
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        ///     Windsor container
        /// </summary>
        public IContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        ///     App configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            _container = Bootstrapper.InitializeContainer();

            AutomapperConfiguration.Configure(_container.Resolve);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            app.UseCors(CorsOptions.AllowAll);

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(_container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            ConfigureOAuth(app);

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