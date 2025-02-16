using System.ComponentModel;

internal class MainPageViewModel : INotifyPropertyChanged
{
    private int _power;
    private int _angle;
    private float _wind;
    private int _player1Health;
    private int _player2Health;

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

    public int Player1Health
    {
        get => _player1Health;
        set
        {
            if (_player1Health != value)
            {
                _player1Health = value;
                OnPropertyChanged(nameof(Player1Health));
            }
        }
    }

    public int Player2Health
    {
        get => _player2Health;
        set
        {
            if (_player2Health != value)
            {
                _player2Health = value;
                OnPropertyChanged(nameof(Player2Health));
            }
        }
    }

    public MainPageViewModel()
    {
        // Initialwerte setzen
        Power = 50;
        Angle = 45;
        Wind = 0;
        Player1Health = 100; // Startwert für Spieler 1
        Player2Health = 100; // Startwert für Spieler 2
    }

    public event PropertyChangedEventHandler PropertyChanged;

    //wird ausgeführt wenn eine der Obenbeschriebenen Properties geändert wird
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}