using System.ComponentModel;

namespace SEW03_Projekt
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private int _power;
        private int _angle;
        private float _wind;

        public int Power
        {
            get => _power;
            set
            {
                if (_power != value)
                {
                    _power = value;
                    OnPropertyChanged(nameof(Power));
                }
            }
        }

        public int Angle
        {
            get => _angle;
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    OnPropertyChanged(nameof(Angle));
                }
            }
        }

        public float Wind
        {
            get => _wind;
            set
            {
                if (_wind != value)
                {
                    _wind = value;
                    OnPropertyChanged(nameof(Wind));
                }
            }
        }

        public MainPageViewModel()
        {
            // Initialwerte setzen
            Power = 50;
            Angle = 45;
            Wind = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}