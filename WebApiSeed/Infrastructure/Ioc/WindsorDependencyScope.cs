namespace WebApiSeed.Infrastructure.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Castle.MicroKernel.Lifestyle;
    using Castle.Windsor;

    internal sealed class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            _container = container;
            _scope = container.BeginScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().ToArray();
        }
    }
}