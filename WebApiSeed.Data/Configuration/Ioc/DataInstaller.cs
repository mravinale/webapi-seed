namespace WebApiSeed.Data.Configuration.Ioc
{
    using System.Configuration;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Common.CustomLifestyles;
    using EF;
    using EF.Interfaces;

    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WebApiSeedDb"].ConnectionString;
            container.Register(
                Component.For<IDbContext>()
                    .ImplementedBy<WebApiSeedDbContext>()
                    .LifeStyle.HybridPerWebRequestTransient()
                    .DependsOn(Parameter.ForKey("connectionString").Eq(connectionString)),
                Classes.FromThisAssembly()
                    .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service") || type.Name.EndsWith("Helper"))
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                    .Configure(x => x.Named(x.Implementation.FullName))
                );
        }
    }
}