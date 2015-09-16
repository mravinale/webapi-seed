namespace WebApiSeed.Infrastructure.Automapper
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    /// <summary>
    ///     Automapper configuration
    /// </summary>
    public static class AutomapperConfiguration
    {
        /// <summary>
        ///     Configure method
        /// </summary>
        /// <param name="serviceLocator"></param>
        public static void Configure(Func<Type, object> serviceLocator = null)
        {
            if (serviceLocator != null)
                Mapper.Configuration.ConstructServicesUsing(serviceLocator);

            var configurators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof (IObjectMapper).IsAssignableFrom(t)
                            && !t.IsAbstract
                            && !t.IsInterface)
                .Select(Activator.CreateInstance).OfType<IObjectMapper>();

            foreach (var configurator in configurators)
            {
                configurator.Apply();
            }
        }
    }
}