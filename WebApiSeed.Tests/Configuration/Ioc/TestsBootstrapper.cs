namespace WebApiSeed.Tests.Configuration.Ioc
{
    using Castle.Windsor;
    using Data.Configuration.Ioc;

    public static class TestsBootstrapper
    {
        /// <summary>
        ///     Initialize IOC container
        /// </summary>
        /// <returns></returns>
        public static IWindsorContainer InitializeContainer()
        {
            return new WindsorContainer().Install(
                new WindsorInstaller(),
                new DataInstaller(),
                new Common.Configuration.Ioc.WindsorInstaller());
        }

        /// <summary>
        ///     Release container
        /// </summary>
        /// <param name="container"></param>
        public static void Release(IWindsorContainer container)
        {
            container.Dispose();
        }
    }
}