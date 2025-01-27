using System.ComponentModel;
using System.Windows.Input;

namespace GameHelper.ViewModels
{
    public class KeybindViewModel : INotifyPropertyChanged
    {
        private string name;
        private string keybind;
        private string action;
        private int interval;
        private Key key;

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

        public string Keybind
        {
            get => keybind;
            set
            {
                if (keybind != value)
                {
                    keybind = value;
                    OnPropertyChanged(nameof(Keybind));
                }
            }
        }

        public string Action
        {
            get => action;
            set
            {
                if (action != value)
                {
                    action = value;
                    OnPropertyChanged(nameof(Action));
                }
            }
        }

        public int Interval
        {
            get => interval;
            set
            {
                if (interval != value)
                {
                    interval = value;
                    OnPropertyChanged(nameof(Interval));
                }
            }
        }

        public Key Key
        {
            get => key;
            set
            {
                if (key != value)
                {
                    key = value;
                    OnPropertyChanged(nameof(Key));
                }
            }
        }

        public IEnumerable<Key> AllKeys => GetRelevantKeys();

        private IEnumerable<Key> GetRelevantKeys()
        {
            return new List<Key>
                {
                    Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z,
                    Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                    Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12,
                    Key.LeftShift, Key.RightShift, Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt, Key.RightAlt,
                    Key.Space, Key.Enter, Key.Back, Key.Tab, Key.Escape, Key.Delete, Key.Insert, Key.Home, Key.End, Key.PageUp, Key.PageDown,
                    Key.Up, Key.Down, Key.Left, Key.Right
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
