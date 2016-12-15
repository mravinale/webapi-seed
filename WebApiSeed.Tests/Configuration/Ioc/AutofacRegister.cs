﻿namespace WebApiSeed.Tests.Configuration.Ioc
{
    using System.Data.Entity;

    using Data.Configuration.EF;

    using Autofac;

    /// <summary>
    ///     Windsor API installer
    /// </summary>
    public class AutofacRegister
    {
        /// <summary>
        ///     Windsor installer install method
        /// </summary>
        public static ContainerBuilder Register(ContainerBuilder builder)
        {
            builder.RegisterType<WebApiSeedDbInitializer>().As<IDatabaseInitializer<WebApiSeedDbContext>>().AsImplementedInterfaces()
                   .InstancePerRequest();

            return builder;
        }
    }
}