using System.Windows.Controls;

namespace GameHelper.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>

    public partial class Settings : Page
    {
        private static readonly Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());

        private Settings()
        {
            InitializeComponent();
        }

        // Public static property to get the single instance of the class
        public static Settings Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }










}
