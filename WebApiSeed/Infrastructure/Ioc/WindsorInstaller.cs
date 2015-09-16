namespace WebApiSeed.Infrastructure.Ioc
{
    using System.Data.Entity;
    using System.Web.Http;
    using AutoMapper;
    using Automapper;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Data.Configuration.EF;
    using Services;
    using Services.Interfaces;

    /// <summary>
    ///     Windsor API installer
    /// </summary>
    public class WindsorInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     Windsor installer install method
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMappingEngine>()
                    .UsingFactoryMethod(() => Mapper.Engine),
                Types.FromThisAssembly()
                    .Where(type => (type.Name.EndsWith("Services") ||
                                    type.Name.EndsWith("Resolver") ||
                                    type.Name.EndsWith("Helper") ||
                                    type.Name.EndsWith("Mapper") ||
                                    type.Name.EndsWith("Controller")) && type.IsClass)
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            container.Register(Component.For<IDatabaseInitializer<WebApiSeedDbContext>>()
                .ImplementedBy<WebApiSeedDbInitializer>()
                .LifestyleTransient());

            AutomapperConfiguration.Configure(container.Resolve);
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);
        }
    }
}