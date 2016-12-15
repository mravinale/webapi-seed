namespace WebApiSeed.Infrastructure.Ioc
{
    using System.Data.Entity;
    using System.Web.Http;

    using AutoMapper;

    using Data.Configuration.EF;

    using Autofac;
    using Autofac.Integration.WebApi;

    using System.Reflection;

    /// <summary>
    ///     Windsor API installer
    /// </summary>
    public static class AutofacRegister
    {
        /// <summary>
        ///     Windsor installer install method
        /// </summary>
        public static void Register(ref ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            builder.Register(c => Mapper.Instance).As<IMapper>();

            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                    .Where(type => ((type.Name.EndsWith("Services") ||
                                    type.Name.EndsWith("Resolver") ||
                                    type.Name.EndsWith("Helper") ||
                                    type.Name.EndsWith("Mapper") ||
                                    type.Name.EndsWith("Controller")) && type.IsClass) ||
                                  (!type.IsAbstract && typeof(ApiController).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .InstancePerRequest();

            builder.RegisterType<WebApiSeedDbInitializer>().As<IDatabaseInitializer<WebApiSeedDbContext>>().AsImplementedInterfaces()
                    .InstancePerRequest();
        }
    }
}