using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameHelper.UserControls
{
    public partial class UserButton : UserControl
    {
        public UserButton()
        {
            InitializeComponent();
        }

        private void UserButtonControl_Click(object sender, RoutedEventArgs e)
        {
            // Handle the button click event here
            MessageBox.Show("User button clicked!");
        }

        public static readonly DependencyProperty UserImageProperty =
            DependencyProperty.Register("UserImage", typeof(string), typeof(UserButton), new PropertyMetadata(default(string), OnUserImageChanged));

        public string UserImage
        {
            get { return (string)GetValue(UserImageProperty); }
            set { SetValue(UserImageProperty, value); }
        }

        private static void OnUserImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UserButton;
            if (control != null && e.NewValue is string newImagePath)
            {
                var button = control.UserButtonControl;
                var ellipse = button.Template.FindName("UserButtonControlEllipse", button) as Ellipse;
                if (ellipse != null)
                {
                    var imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(newImagePath, UriKind.RelativeOrAbsolute))
                    };
                    ellipse.Fill = imageBrush;
                }
            }
        }
    }
}
