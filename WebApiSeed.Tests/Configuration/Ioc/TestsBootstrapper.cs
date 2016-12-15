namespace WebApiSeed.Tests.Configuration.Ioc
{
    using Autofac;

    using Common.Configuration.Ioc;

    using Data.Configuration.Ioc;

    using Infrastructure.Ioc;

    /// <summary>
    /// TestsBootstrapper Class
    /// </summary>
    public static class TestsBootstrapper
    {
        /// <summary>
        ///     Initialize IOC container
        /// </summary>
        /// <returns></returns>
        public static IContainer InitializeContainer()
        {
            // Autofac container
            var builder = new ContainerBuilder();

            builder = Infrastructure.Ioc.AutofacRegister.Register(builder);
            builder = RegisterCommon.Register(builder);
            builder = DataInstaller.Register(builder);

            return builder.Build();
        }
    }
}