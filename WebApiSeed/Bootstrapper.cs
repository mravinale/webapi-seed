namespace WebApiSeed
{
    using Autofac;
    using Autofac.Integration.WebApi;

    using AutoMapper;

    using Common.Configuration.Ioc;

    using Data.Configuration.EF;
    using Data.Configuration.Ioc;
    using Infrastructure.Ioc;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;

    /// <summary>
    ///     Bootstrapper class
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        ///     Initialize IOC container
        /// </summary>
        /// <returns></returns>
        public static IContainer InitializeContainer()
        {
            // Autofac container
            var builder = new ContainerBuilder();

            AutofacRegister.Register(ref builder);
            RegisterCommon.Register(ref builder);
            DataInstaller.Register(ref builder);

            return builder.Build();
        }
    }
}