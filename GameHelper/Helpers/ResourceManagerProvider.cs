using System.Globalization;
using System.Resources;

namespace GameHelper.Helpers
{
    public static class ResourceManagerProvider
    {
        private static ResourceManager _resourceManager = new ResourceManager("GameHelper.Resources.Strings", typeof(ResourceManagerProvider).Assembly);

        public static ResourceManager ResourceManager => _resourceManager;

        public static void SetResourceManager(CultureInfo culture)
        {
            _resourceManager = new ResourceManager("GameHelper.Resources.Strings", typeof(ResourceManagerProvider).Assembly);
            CultureInfo.CurrentUICulture = culture;
        }
    }
}
