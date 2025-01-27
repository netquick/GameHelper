using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameHelper.GameViews
{
    /// <summary>
    /// Interaction logic for Default_Gamesettings.xaml
    /// </summary>
    public partial class Default_Gamesettings : Page
    {
        private static readonly Lazy<Default_Gamesettings> _instance = new Lazy<Default_Gamesettings>(() => new Default_Gamesettings());


        public Default_Gamesettings()
        {
            InitializeComponent();
        }

        public static Default_Gamesettings Instance
        {
            get
            {
                return _instance.Value;
            }
        }

    }
}
