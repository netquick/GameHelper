using System.Windows.Controls;

namespace GameHelper.GameViews
{
    /// <summary>
    /// Interaction logic for Default_Overlay.xaml
    /// </summary>
    public partial class Default_Overlay : Page
    {
        private static readonly Lazy<Default_Overlay> _instance = new Lazy<Default_Overlay>(() => new Default_Overlay());


        public Default_Overlay()
        {
            InitializeComponent();
        }

        public static Default_Overlay Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
