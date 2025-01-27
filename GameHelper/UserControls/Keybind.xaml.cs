using GameHelper.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameHelper.UserControls
{
    /// <summary>
    /// Interaction logic for Keybind.xaml
    /// </summary>
    public partial class Keybind : UserControl
    {
        public Keybind()
        {
            InitializeComponent();
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            KeybindTextBox.Text = "Press a key or mouse button...";
            KeybindTextBox.Focus();
            KeybindTextBox.PreviewKeyDown += KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewMouseDown += KeybindTextBox_PreviewMouseDown;
        }

        private void KeybindTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var keybindViewModel = DataContext as KeybindViewModel;
            if (keybindViewModel != null)
            {
                keybindViewModel.Keybind = e.Key.ToString();
                KeybindTextBox.Text = keybindViewModel.Keybind;
            }
            KeybindTextBox.PreviewKeyDown -= KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewMouseDown -= KeybindTextBox_PreviewMouseDown;
        }

        private void KeybindTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var keybindViewModel = DataContext as KeybindViewModel;
            if (keybindViewModel != null)
            {
                keybindViewModel.Keybind = e.ChangedButton.ToString();
                KeybindTextBox.Text = keybindViewModel.Keybind;
            }
            KeybindTextBox.PreviewKeyDown -= KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewMouseDown -= KeybindTextBox_PreviewMouseDown;
        }
    }
}
