using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GameHelper.Config;
using GameHelper.Helpers;

namespace GameHelper.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _selectedTheme;
        private string _selectedColor;
        private bool _isDarkTheme;
        private bool _isSidebarCollapsed;
        private GridLength _sidebarWidth;
        private string _toggleButtonContent;
        private string _selectedLanguage;
        private string _currentUserImage; // Add this line
        private readonly MessageService _messageService;

        public MainWindowViewModel()
        {
            Themes = new ObservableCollection<string> { "Light", "Dark" };
            Colors = new ObservableCollection<string>(ColorDefinitions.Colors.Keys);
            Languages = new ObservableCollection<string> { "EN", "DE" };
            _selectedTheme = ConfigManager.GetTheme();
            _selectedColor = ConfigManager.GetColor();
            _isDarkTheme = _selectedTheme == "Dark";
            _isSidebarCollapsed = ConfigManager.GetMenuState() == "Collapsed";
            _sidebarWidth = _isSidebarCollapsed ? new GridLength(70) : new GridLength(240);
            _toggleButtonContent = _isSidebarCollapsed ? ">" : "<";
            _selectedLanguage = ConfigManager.GetLanguage(); // Load saved language

            ToggleSidebarCommand = new RelayCommand(_ => ToggleSidebar());
            MinimizeCommand = new RelayCommand(_ => Minimize());
            MaximizeCommand = new RelayCommand(_ => Maximize());
            CloseCommand = new RelayCommand(_ => Close());
            // Initialize _messageService
            _messageService = MessageService.Instance;
            // Apply the saved language on startup
            ChangeLanguage(_selectedLanguage);

            // Set the initial value for the CurrentUserImage property
            CurrentUserImage = "pack://application:,,,/Images/placeholder_user.jpg";
        }

        public ObservableCollection<string> Themes { get; }
        public ObservableCollection<string> Colors { get; }
        public ObservableCollection<string> Languages { get; }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                    ApplyThemeAndColor();
                }
            }
        }

        public string SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged();
                    ApplyThemeAndColor();
                }
            }
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    SelectedTheme = _isDarkTheme ? "Dark" : "Light";
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSidebarCollapsed
        {
            get => _isSidebarCollapsed;
            set
            {
                if (_isSidebarCollapsed != value)
                {
                    _isSidebarCollapsed = value;
                    OnPropertyChanged();
                    ConfigManager.SetMenuState(_isSidebarCollapsed ? "Collapsed" : "Expanded");
                }
            }
        }

        public GridLength SidebarWidth
        {
            get => _sidebarWidth;
            set
            {
                if (_sidebarWidth != value)
                {
                    _sidebarWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ToggleButtonContent
        {
            get => _toggleButtonContent;
            set
            {
                if (_toggleButtonContent != value)
                {
                    _toggleButtonContent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                    ChangeLanguage(_selectedLanguage);
                }
            }
        }

        public string CurrentUserImage // Add this property
        {
            get => _currentUserImage;
            set
            {
                if (_currentUserImage != value)
                {
                    _currentUserImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ToggleSidebarCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CloseCommand { get; }

        private void ToggleSidebar()
        {
            IsSidebarCollapsed = !IsSidebarCollapsed;
            SidebarWidth = IsSidebarCollapsed ? new GridLength(70) : new GridLength(240);
            ToggleButtonContent = IsSidebarCollapsed ? ">" : "<";
            ConfigManager.SetMenuState(IsSidebarCollapsed ? "Collapsed" : "Expanded");
        }

        private void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize()
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close()
        {
            Application.Current.MainWindow.Close();
        }

        private void ApplyThemeAndColor()
        {
            ThemeManager.ApplyTheme(_selectedTheme, _selectedColor);
            ConfigManager.SetTheme(_selectedTheme);
            ConfigManager.SetColor(_selectedColor);
        }

        private void ChangeLanguage(string language)
        {
            string cultureName = language switch
            {
                "EN" => "en-US",
                "DE" => "de-DE",
                _ => "en-US"
            };

            LocalizationHelper.ChangeCulture(cultureName);
            UpdateApplicationResources(language);
            ConfigManager.SetLanguage(language); // Save the selected language

            // Set the ResourceManager for the new culture
            CultureInfo culture = new CultureInfo(cultureName);
            GameHelper.Helpers.ResourceManagerProvider.SetResourceManager(culture);

            // Refresh the UI to reflect the new language
            Application.Current.MainWindow?.Dispatcher.Invoke(() =>
            {
                ((MainWindow)Application.Current.MainWindow)?.LoadNavigationBar();
            });
        }



        private void UpdateApplicationResources(string language)
        {
            string resourceBaseName = "GameHelper.Resources.Strings";

            var resourceManager = new ResourceManager(resourceBaseName, Assembly.GetExecutingAssembly());
            CultureInfo culture = new CultureInfo(language == "DE" ? "de-DE" : "en-US");

            ResourceSet resourceSet = resourceManager.GetResourceSet(culture, true, true);
            if (resourceSet != null)
            {
                foreach (DictionaryEntry entry in resourceSet)
                {
                    Application.Current.Resources[entry.Key.ToString()] = entry.Value;
                }
            }

            _messageService.LogMessage($"Loaded resources for culture: {culture.Name}");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
