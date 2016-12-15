namespace WebApiSeed.Common.Configuration.Ioc
{
    using Autofac;

    using System.Reflection;

    using Utils;
    using Utils.Interfaces;

    /// <summary>
    ///     Windsor API installer
    /// </summary>
    public static class RegisterCommon
    {
        /// <summary>
        ///     Windsor installer install method
        /// </summary>
        public static void Register(ref ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
                Assembly.GetExecutingAssembly())
                    .Where(type => (type.Name.EndsWith("Services") ||
                                    type.Name.EndsWith("Resolver") ||
                                    type.Name.EndsWith("Helper")) && type.IsClass)
                    .AsImplementedInterfaces()
                    .InstancePerRequest();

            builder.RegisterType<RetryExecuter>().As<IRetryExecuter>().InstancePerLifetimeScope().InstancePerLifetimeScope();
        }
    }
}