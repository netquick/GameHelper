using System.ComponentModel;

namespace GameHelper.ViewModels
{
    public class OverlayButtonViewModel : INotifyPropertyChanged
    {
        private string name;
        private string function;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Function
        {
            get => function;
            set
            {
                if (function != value)
                {
                    function = value;
                    OnPropertyChanged(nameof(Function));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
