using System.Globalization;
using System.Resources;

namespace GameHelper.Helpers
{
    public class LocalizationHelper
    {
        private static ResourceManager _resourceManager = ResourceManagerProvider.ResourceManager;

        public static string GetString(string key)
        {
            return _resourceManager.GetString(key, CultureInfo.CurrentUICulture);
        }

        public static void ChangeCulture(string cultureName)
        {
            CultureInfo newCulture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = newCulture;
            CultureInfo.CurrentCulture = newCulture;
        }
    }
}
