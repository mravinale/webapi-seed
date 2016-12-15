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

            Infrastructure.Ioc.AutofacRegister.Register(ref builder);
            RegisterCommon.Register(ref builder);
            DataInstaller.Register(ref builder);

            return builder.Build();
        }
    }
}