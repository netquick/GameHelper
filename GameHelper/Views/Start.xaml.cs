using System.Windows.Controls;

namespace GameHelper.Views
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        // Private static instance of the class
        private static readonly Lazy<Start> _instance = new Lazy<Start>(() => new Start());

        // Private constructor to prevent external instantiation
        private Start()
        {
            InitializeComponent();
        }

        // Public static property to get the single instance of the class
        public static Start Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
