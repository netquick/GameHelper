using System.IO;
using System.Windows.Input;
using System.Xml.Linq;
using GameHelper.ViewModels;
namespace GameHelper.Config
{
    public static class ConfigManager
    {
        private static readonly string ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameHelper", "config.xml");

        static ConfigManager()
        {
            InitializeConfig();
        }

        private static void InitializeConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                var directory = Path.GetDirectoryName(ConfigFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var config = new XElement("Config",
                    new XElement("LogFilePath", Path.Combine(directory, "log.txt")),
                    new XElement("Theme", "Light"), // Default theme
                    new XElement("Color", "Red"), // Default color
                    new XElement("Language", "EN"), // Default language
                    new XElement("Keybinds") // Keybinds element
                );

                config.Save(ConfigFilePath);
            }
        }

        public static string GetLogFilePath()
        {
            var config = XElement.Load(ConfigFilePath);
            return config.Element("LogFilePath")?.Value;
        }

        public static string GetTheme()
        {
            var config = XElement.Load(ConfigFilePath);
            return config.Element("Theme")?.Value ?? "Light"; // Default to Light theme
        }

        public static string GetColor()
        {
            var config = XElement.Load(ConfigFilePath);
            return config.Element("Color")?.Value ?? "Red"; // Default to Red color
        }

        public static void SetTheme(string theme)
        {
            var config = XElement.Load(ConfigFilePath);
            var themeElement = config.Element("Theme");
            if (themeElement != null)
            {
                themeElement.SetValue(theme);
            }
            else
            {
                config.Add(new XElement("Theme", theme));
            }
            config.Save(ConfigFilePath);
        }

        public static void SetColor(string color)
        {
            var config = XElement.Load(ConfigFilePath);
            var colorElement = config.Element("Color");
            if (colorElement != null)
            {
                colorElement.SetValue(color);
            }
            else
            {
                config.Add(new XElement("Color", color));
            }
            config.Save(ConfigFilePath);
        }

        public static string GetMenuState()
        {
            var config = XElement.Load(ConfigFilePath);
            return config.Element("MenuState")?.Value ?? "Expanded"; // Default to Expanded
        }

        public static void SetMenuState(string menuState)
        {
            var config = XElement.Load(ConfigFilePath);
            var menuStateElement = config.Element("MenuState");
            if (menuStateElement != null)
            {
                menuStateElement.SetValue(menuState);
            }
            else
            {
                config.Add(new XElement("MenuState", menuState));
            }
            config.Save(ConfigFilePath);
        }

        public static string GetLanguage()
        {
            var config = XElement.Load(ConfigFilePath);
            return config.Element("Language")?.Value ?? "EN"; // Default to English
        }

        public static void SetLanguage(string language)
        {
            var config = XElement.Load(ConfigFilePath);
            var languageElement = config.Element("Language");
            if (languageElement != null)
            {
                languageElement.SetValue(language);
            }
            else
            {
                config.Add(new XElement("Language", language));
            }
            config.Save(ConfigFilePath);
        }

        // New methods for handling keybinds
        public static List<KeybindViewModel> GetKeybinds()
        {
            var config = XElement.Load(ConfigFilePath);
            var keybindsElement = config.Element("Keybinds");
            if (keybindsElement == null)
            {
                return new List<KeybindViewModel>();
            }

            return keybindsElement.Elements("Keybind").Select(x => new KeybindViewModel
            {
                Name = x.Element("Name")?.Value,
                Key = Enum.TryParse(x.Element("Key")?.Value, out Key key) ? key : Key.None,
                Keybind = x.Element("Keybind")?.Value,
                Action = x.Element("Action")?.Value,
                Interval = int.Parse(x.Element("Interval")?.Value ?? "0")
            }).ToList();
        }

        public static void SaveKeybinds(List<KeybindViewModel> keybinds)
        {
            var config = XElement.Load(ConfigFilePath);
            var keybindsElement = config.Element("Keybinds");
            if (keybindsElement == null)
            {
                keybindsElement = new XElement("Keybinds");
                config.Add(keybindsElement);
            }
            else
            {
                keybindsElement.RemoveAll();
            }

            foreach (var keybind in keybinds)
            {
                keybindsElement.Add(new XElement("Keybind",
                    new XElement("Name", keybind.Name),
                    new XElement("Key", keybind.Key.ToString()),
                    new XElement("Keybind", keybind.Keybind),
                    new XElement("Action", keybind.Action),
                    new XElement("Interval", keybind.Interval)
                ));
            }

            config.Save(ConfigFilePath);
        }
    }
}
