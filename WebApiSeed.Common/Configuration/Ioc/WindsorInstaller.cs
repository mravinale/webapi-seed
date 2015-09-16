namespace WebApiSeed.Common.Configuration.Ioc
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Utils;
    using Utils.Interfaces;

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
                Types.FromThisAssembly()
                    .Where(type => (type.Name.EndsWith("Services") ||
                                    type.Name.EndsWith("Resolver") ||
                                    type.Name.EndsWith("Helper")) && type.IsClass)
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            container.Register(Component.For<IRetryExecuter>()
                .ImplementedBy<RetryExecuter>()
                .LifestyleTransient());
        }
    }
}