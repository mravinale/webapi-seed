namespace WebApiSeed.Common.CustomLifestyles
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Registration.Lifestyle;

    public static class LifestyleRegistrationExtensions
    {
        /// <summary>
        ///     One component instance per web request, or if HttpContext is not available, transient.
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="group"></param>
        /// <returns></returns>
        public static ComponentRegistration<S> HybridPerWebRequestTransient<S>(this LifestyleGroup<S> @group)
            where S : class
        {
            return @group.Scoped<HybridPerWebRequestTransientScopeAccessor>();
        }
    }
}