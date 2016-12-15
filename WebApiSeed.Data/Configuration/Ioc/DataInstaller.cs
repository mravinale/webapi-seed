namespace WebApiSeed.Data.Configuration.Ioc
{
    using System.Configuration;

    using EF;
    using EF.Interfaces;

    using Autofac;

    using System.Reflection;

    /// <summary>
    /// DataInstaller Clas
    /// </summary>
    public static class DataInstaller
    {
        /// <summary>
        /// Registers the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static void Register(ref ContainerBuilder builder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WebApiSeedDb"].ConnectionString;

            builder.RegisterType<WebApiSeedDbContext>().As<IDbContext>().WithParameter("connectionString", connectionString).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                    .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service") || type.Name.EndsWith("Helper"))
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
        }
    }
}