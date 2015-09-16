namespace WebApiSeed.Common.CustomLifestyles
{
    using System.Web;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Lifestyle;
    using Castle.MicroKernel.Lifestyle.Scoped;

    public class HybridPerWebRequestTransientScopeAccessor : IScopeAccessor
    {
        private readonly ILifetimeScope secondaryScope = new DefaultLifetimeScope();
        private readonly IScopeAccessor webRequestScopeAccessor = new WebRequestScopeAccessor();

        public ILifetimeScope GetScope(CreationContext context)
        {
            if (HttpContext.Current != null && PerWebRequestLifestyleModuleUtils.IsInitialized)
                return webRequestScopeAccessor.GetScope(context);
            return secondaryScope;
        }

        public void Dispose()
        {
            webRequestScopeAccessor.Dispose();
            secondaryScope.Dispose();
        }
    }
}