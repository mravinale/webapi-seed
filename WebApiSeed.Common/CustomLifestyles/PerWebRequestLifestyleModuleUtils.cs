namespace WebApiSeed.Common.CustomLifestyles
{
    using System.Reflection;
    using Castle.MicroKernel.Lifestyle;

    public class PerWebRequestLifestyleModuleUtils
    {
        private static readonly FieldInfo InitializedFieldInfo =
            typeof (PerWebRequestLifestyleModule).GetField("initialized",
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);

        public static bool IsInitialized
        {
            get { return (bool) InitializedFieldInfo.GetValue(null); }
        }
    }
}