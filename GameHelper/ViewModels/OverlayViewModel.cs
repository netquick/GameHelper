using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GameHelper.ViewModels
{
    public class OverlayViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<OverlayButtonViewModel> overlayButtons;

        public ObservableCollection<OverlayButtonViewModel> OverlayButtons
        {
            get => overlayButtons;
            set
            {
                if (overlayButtons != value)
                {
                    overlayButtons = value;
                    OnPropertyChanged(nameof(OverlayButtons));
                }
            }
        }

        public OverlayViewModel()
        {
            // Initialize with sample data or load from configuration
            OverlayButtons = new ObservableCollection<OverlayButtonViewModel>
                {
                    new OverlayButtonViewModel { Name = "Autowalk", Function = "W/Key" },
                    // Add more sample data or load from configuration
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
