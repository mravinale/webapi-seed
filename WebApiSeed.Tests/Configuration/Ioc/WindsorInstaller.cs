namespace WebApiSeed.Tests.Configuration.Ioc
{
    using System.Data.Entity;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Data.Configuration.EF;

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
            container.Register(Component.For<IDatabaseInitializer<WebApiSeedDbContext>>()
                .ImplementedBy<WebApiSeedDbInitializerTests>()
                .LifestyleTransient());
        }
    }
}