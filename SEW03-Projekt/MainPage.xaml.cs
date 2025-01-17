using SEW03_Projekt.Classes;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        string playername = "";

        Playerobject player1;
        Playerobject player2;
        Ammunition apple;
        Ammunition melon;
        Ammunition dung;
        
        public enum Ammo {apple, melon, dung};

        public MainPage()
        {
            InitializeComponent();
/*
            player1 = new Playerobject(playername, 100, 100);
            apple = new Ammunition("apple", 2, 10, 1);
            melon = new Ammunition("melon", 5, 30, 7);
            dung = new Ammunition("dung", 3, 20, 3);
*/



        }









    }

}
