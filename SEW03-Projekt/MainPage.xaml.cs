using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        string playername = "";


        Ammunition apple;
        Ammunition melon;
        Ammunition dung;

        




        public enum Ammo {apple, melon, dung};

        public MainPage()
        {
            InitializeComponent();

            // Instanziieren der startpage
            var startPageInstance = new startpage();

            // Korrekte Initialisierung von Player1Drawable mit dem startpage-Parameter
            var player1Drawable = new Player1Drawable(startPageInstance);

            // Zuweisung des Drawable zu canvasView
            canvasView.Drawable = player1Drawable;


            /*
                        player1 = new Playerobject(playername, 100, 100);
                        apple = new Ammunition("apple", 2, 10, 1);
                        melon = new Ammunition("melon", 5, 30, 7);
                        dung = new Ammunition("dung", 3, 20, 3);
            */

        }



        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {

        }
    }

}
