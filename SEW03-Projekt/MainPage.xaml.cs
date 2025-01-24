using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        string playername = "";

        private Playerobject player1;
        private Playerobject player2;
        Ammunition apple;
        Ammunition melon;
        Ammunition dung;

        public Playerobject Player1 { get => player1; set => player1 = value; }
        public Playerobject Player2 { get => player2; set => player2 = value; }

        public float Player1X => (float)player1.Playerpos.X;
        public float Player1Y => (float)player1.Playerpos.Y;


        public enum Ammo {apple, melon, dung};

        public MainPage()
        {
            InitializeComponent();

            Player1 = new Playerobject("Gert", 100, 100, new Point(100, 200));
            var player1Drawable = new Player1Drawable(this);
            /*
                        player1 = new Playerobject(playername, 100, 100);
                        apple = new Ammunition("apple", 2, 10, 1);
                        melon = new Ammunition("melon", 5, 30, 7);
                        dung = new Ammunition("dung", 3, 20, 3);
            */

        }









    }

}
