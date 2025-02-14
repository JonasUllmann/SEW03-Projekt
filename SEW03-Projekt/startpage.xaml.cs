using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls;
using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;

namespace SEW03_Projekt
{
    public partial class startpage : TabbedPage
    {
        string botdifficulty = "";

        private Player1Drawable _player1Drawable;

        private Point p1pos;
        private Point p2pos;

        private Playerobject player1;
        private Playerobject player2;

        public Playerobject Player1 { get => player1; set => player1 = value; }
        public Playerobject Player2 { get => player2; set => player2 = value; }

        public startpage()
        {
            InitializeComponent();

            // Bildschirmgröße über DeviceDisplay.MainDisplayInfo abrufen
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            float screenWidth = (float)displayInfo.Width;
            float screenHeight = (float)displayInfo.Height;


            // p1pos initialisieren
            p1pos = new Point(screenWidth * 0.15f, screenHeight * 0.85f);

            // Player1 mit p1pos initialisieren
            player1 = new Playerobject("Gert", 100, 100, p1pos);
            player2 = new Playerobject("Gundula", 100, 100, new Point(700, 200));
        }

        public string Botdifficulty { get => botdifficulty; set => botdifficulty = value; }

        private void btnbot_Clicked(object sender, EventArgs e)
        {
            Botdifficulty = DifficultyPicker.SelectedItem?.ToString();
            if (entplayername.Text != null && entplayername.Text != "")
            {
                Navigation.PushAsync(new MainPage(player1, player2));
            }
        }
    }
}