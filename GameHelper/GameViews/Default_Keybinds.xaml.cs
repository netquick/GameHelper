using GameHelper.Config;
using GameHelper.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel; // Add this using directive

namespace GameHelper.GameViews;



/// <summary>
/// Interaction logic for Default_Keybinds.xaml
/// </summary>
public partial class Default_Keybinds : Page
{
    private static readonly Lazy<Default_Keybinds> _instance = new Lazy<Default_Keybinds>(() => new Default_Keybinds());

    public ObservableCollection<KeybindViewModel> Keybinds { get; set; }

    public Default_Keybinds()
    {
        InitializeComponent();
        Keybinds = new ObservableCollection<KeybindViewModel>(ConfigManager.GetKeybinds());
        foreach (var keybind in Keybinds)
        {
            keybind.PropertyChanged += Keybind_PropertyChanged;
        }
        DataContext = this;
    }

    public static Default_Keybinds Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        var keybind = new KeybindViewModel
        {
            Name = "New Keybind",
            Key = Key.None,
            Keybind = "None",
            Action = "None",
            Interval = 0
        };
        keybind.PropertyChanged += Keybind_PropertyChanged;
        Keybinds.Add(keybind);
        ConfigManager.SaveKeybinds(Keybinds.ToList());
        UpdateOverlayButtons();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (KeybindsListView.SelectedItem is KeybindViewModel selectedKeybind)
        {
            selectedKeybind.PropertyChanged -= Keybind_PropertyChanged;
            Keybinds.Remove(selectedKeybind);
            ConfigManager.SaveKeybinds(Keybinds.ToList());
            UpdateOverlayButtons();
        }
    }

    private void Keybind_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        ConfigManager.SaveKeybinds(Keybinds.ToList());
        UpdateOverlayButtons();
    }

    private void UpdateOverlayButtons()
    {
        var overlayWindow = Application.Current.Windows.OfType<GameHelper.Overlay.Overlay_Window>().FirstOrDefault();
        if (overlayWindow != null)
        {
            var overlayViewModel = overlayWindow.DataContext as OverlayViewModel;
            if (overlayViewModel != null)
            {
                overlayViewModel.OverlayButtons.Clear();
                foreach (var keybind in Keybinds)
                {
                    overlayViewModel.OverlayButtons.Add(new OverlayButtonViewModel
                    {
                        Name = keybind.Name,
                        Function = $"{keybind.Keybind}/{keybind.Key}"
                    });
                }
            }
        }
    }
}
