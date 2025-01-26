using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;


namespace GameHelper.Helpers
{
    public static class ThemeManager
    {
        public static void ApplyTheme(string theme, string color)
        {
            var themeDict = new ResourceDictionary();
            var colorDict = new ResourceDictionary();
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            switch (theme)
            {
                case "Light":
                    themeDict.Source = new Uri($"pack://application:,,,/{assemblyName};component/Themes/LightTheme.xaml", UriKind.Absolute);
                    break;
                case "Dark":
                    themeDict.Source = new Uri($"pack://application:,,,/{assemblyName};component/Themes/DarkTheme.xaml", UriKind.Absolute);
                    break;
                default:
                    throw new ArgumentException("Invalid theme");
            }

            if (!ColorDefinitions.Colors.TryGetValue(color, out var hexColor))
            {
                throw new ArgumentException("Invalid color");
            }

            var baseColor = (Color)ColorConverter.ConvertFromString(hexColor);
            var accent1 = ColorHelper.CreateColorVariant(baseColor, -0.2);
            var accent2 = ColorHelper.CreateColorVariant(baseColor, 0.2);

            colorDict.Add("PrimaryColor", new SolidColorBrush(baseColor));
            colorDict.Add("Accent1Color", new SolidColorBrush(accent1));
            colorDict.Add("Accent2Color", new SolidColorBrush(accent2));

            // Add the colors to the resource keys used by the application
            colorDict.Add("ThemeAccentBrush", new SolidColorBrush(baseColor));
            colorDict.Add("ThemeAccent2Brush", new SolidColorBrush(accent1));
            colorDict.Add("ThemeAccent3Brush", new SolidColorBrush(accent2));

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
            Application.Current.Resources.MergedDictionaries.Add(colorDict);
        }
    }
}
