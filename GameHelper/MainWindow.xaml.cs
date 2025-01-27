using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GameHelper.Helpers;
using GameHelper.Objects;
using GameHelper.ViewModels;
using GameHelper.Views;
using GameHelper.GameViews;
namespace GameHelper
{
    public partial class MainWindow : Window
    {
        private readonly MessageService _messageService;
        private DispatcherTimer _progressBarTimer;
        private Overlay.Overlay_Window overlayWindow;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel
            {
                CurrentUserImage = "Images/placeholder_user.jpg" // Ensure this path is correct
            };

            // Initialize the message service
            _messageService = MessageService.Instance;
            _messageService.NotificationReceived += OnNotificationReceived;
            _messageService.ProgressUpdated += OnProgressUpdated;

            // Initialize the timer
            _progressBarTimer = new DispatcherTimer();
            _progressBarTimer.Interval = TimeSpan.FromSeconds(2);
            _progressBarTimer.Tick += (s, e) =>
            {
                ProgressBar.Visibility = Visibility.Collapsed;
                _progressBarTimer.Stop();
            };

            // Hide the ProgressBar by default
            ProgressBar.Visibility = Visibility.Collapsed;

            // Load the navigation bar
            LoadNavigationBar();

            // Refresh UI and similar
            this.Loaded += MainWindow_Loaded;
        }

        // Event handler for window loaded
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Any additional logic to run when the window is loaded
        }

        // Message service event handlers
        private void OnNotificationReceived(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var textBlock = new TextBlock { Text = message };
                MessageHost.Children.Add(textBlock);
            });
        }

        private void OnProgressUpdated(double progress)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = progress;
                ProgressBar.Visibility = Visibility.Visible;

                // Restart the timer
                _progressBarTimer.Stop();
                _progressBarTimer.Start();
            });
        }

        // Method to get the message service instance
        public MessageService GetMessageService()
        {
            return _messageService;
        }

        // Methods to handle window dragging and resizing
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Allows the window to be dragged around by the custom title bar
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // Check if it's a double-click
            {
                // Toggle between maximized and normal window states
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
            }
            else // Single click
            {
                // Existing logic to drag the window
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
        }

        // Methods to handle window resizing
        private void OnRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;
            if (this.Width > this.MinWidth)
            {
                this.Width = newWidth;
            }
        }

        private void OnBottomDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.Height + e.VerticalChange;
            if (this.Height > this.MinHeight)
            {
                this.Height = newHeight;
            }
        }

        private void OnLeftDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            if (newWidth > this.MinWidth)
            {
                this.Width = newWidth;
                this.Left += e.HorizontalChange;
            }
        }

        private void OnTopDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.Height - e.VerticalChange;
            if (newHeight > this.MinHeight)
            {
                this.Height = newHeight;
                this.Top += e.VerticalChange;
            }
        }

        private void OnBottomRightDragDelta(object sender, DragDeltaEventArgs e)
        {
            OnRightDragDelta(sender, e);
            OnBottomDragDelta(sender, e);
        }

        // Methods to handle overlay window
        private void chkOverlay_Checked(object sender, RoutedEventArgs e)
        {
            if (overlayWindow == null)
            {
                overlayWindow = new Overlay.Overlay_Window();
                overlayWindow.Owner = this; // Optional: set the owner of the overlay window
            }
            overlayWindow.Show();
        }

        private void chkOverlay_Unchecked(object sender, RoutedEventArgs e)
        {
            if (overlayWindow != null)
            {
                overlayWindow.Hide();
            }
        }

        // Methods to handle navigation bar
        private Page lastNavigatedPage = null;
        public Page LastNavigatedPage
        {
            get => lastNavigatedPage;
        }

        public void LoadNavigationBar()
        {
            var themeForegroundBrush = Application.Current.Resources["ThemeForegroundBrush"] as SolidColorBrush;

            var resourceManager = GameHelper.Helpers.ResourceManagerProvider.ResourceManager;

            if (NavigationListView != null)
            {
                NavigationListView.ItemsSource = new List<NavigationItem>
                    {
                        new NavigationItem { Icon = "Icons/home.svg", Text = resourceManager.GetString("NavigationHome", CultureInfo.CurrentUICulture) ?? "Home", IsActive = true },
                        new NavigationItem { Icon = "Icons/layers.svg", Text = resourceManager.GetString("NavigationHelp", CultureInfo.CurrentUICulture) ?? "Overlay" },
                        new NavigationItem { Icon = "Icons/joystick.svg", Text = resourceManager.GetString("NavigationExit", CultureInfo.CurrentUICulture) ?? "Game settings" },
                        new NavigationItem { Icon = "Icons/keys.svg", Text = resourceManager.GetString("NavigationExit", CultureInfo.CurrentUICulture) ?? "Keybinds" },
                        new NavigationItem { Icon = "Icons/server.svg", Text = resourceManager.GetString("NavigationExit", CultureInfo.CurrentUICulture) ?? "Server" },
                        new NavigationItem { Icon = "Icons/settings.svg", Text = resourceManager.GetString("NavigationSettings", CultureInfo.CurrentUICulture) ?? "Settings" },
                        // Add other items here
                    };
            }
            else
            {
                Debug.WriteLine("NavigationListView is null.");
            }

            if (MainFrame != null)
            {
                MainFrame.Navigate(Start.Instance);
            }
            else
            {
                Debug.WriteLine("MainFrame is null.");
            }
        }

        // Define the pages to navigate to
        private Start startPage = Start.Instance;
        private Settings settingsPage = Settings.Instance;
        private Default_Overlay Overlay = Default_Overlay.Instance;
        private Default_Keybinds Keybinds = Default_Keybinds.Instance;
        private Default_Gamesettings gamesettingsPage = Default_Gamesettings.Instance;

        // Method to handle navigation list view selection changed
        private void NavigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationListView.SelectedItem is NavigationItem selectedItem)
            {
                // Reset IsActive for all items
                foreach (NavigationItem item in NavigationListView.Items)
                {
                    item.IsActive = false;
                }

                // Set the selected item as active
                selectedItem.IsActive = true;
                Page navigateToPage = null;

                // Navigate based on the selected item's Text property
                switch (selectedItem.Text)
                {
                    case "Home":
                        navigateToPage = startPage;
                        break;
                    case "Settings":
                        navigateToPage = settingsPage;
                        break;
                    case "Overlay":
                        navigateToPage = Overlay;
                        break;
                    case "Keybinds":
                        navigateToPage = Keybinds;
                        break;
                    case "Game settings":
                        navigateToPage = gamesettingsPage;
                        break;
                }

                // Check if we're navigating to a different page
                if (navigateToPage != null && navigateToPage != lastNavigatedPage)
                {
                    MainFrame.Navigate(navigateToPage);
                    lastNavigatedPage = navigateToPage; // Update the last navigated page
                }
            }
        }
    }
}
