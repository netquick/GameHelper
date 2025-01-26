using System.Reflection;
using System.Windows;
using GameHelper.Config;
using GameHelper.Helpers;

namespace GameHelper
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            var lightThemeUri = new Uri($"pack://application:,,,/{assemblyName};component/Themes/LightTheme.xaml", UriKind.Absolute);
            var navigationListViewStyleUri = new Uri($"pack://application:,,,/{assemblyName};component/CustomStyles/NavigationListViewStyle.xaml", UriKind.Absolute);

            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = lightThemeUri });
            resourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = navigationListViewStyleUri });

            Resources.MergedDictionaries.Add(resourceDictionary);


            var messageService = MessageService.Instance;
            messageService.LogMessage("Loading Theme and Color");
            string theme = ConfigManager.GetTheme();
            string color = ConfigManager.GetColor();
            ThemeManager.ApplyTheme(theme, color);
        }
    }
}
