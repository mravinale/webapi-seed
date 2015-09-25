namespace WebApiSeed.Infrastructure.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Castle.Windsor;

    /// <summary>
    ///     Windsor dependency resolver
    /// </summary>
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="container"></param>
        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Get service
        /// </summary>
        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        /// <summary>
        ///     Get services
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().ToArray();
        }

        /// <summary>
        ///     Begin scope
        /// </summary>
        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }
    }
}