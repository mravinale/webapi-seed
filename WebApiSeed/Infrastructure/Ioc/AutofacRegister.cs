namespace WebApiSeed.Infrastructure.Ioc
{
    using System.Data.Entity;
    using System.Web.Http;

    using AutoMapper;

    using Data.Configuration.EF;

    using Autofac;
    using Autofac.Integration.WebApi;

    using System.Reflection;
    using System;
    using System.Linq;
    using System.Collections.Generic;

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

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            //register the mapper
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

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