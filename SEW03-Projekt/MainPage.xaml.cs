using SEW03_Projekt.Classes;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        string playername = "";

        playerobject player1;
        ammunition apple;
        ammunition melon;
        ammunition dung;
        
        public enum Ammo {apple, melon, dung};

        public MainPage()
        {
            InitializeComponent();

            player1 = new playerobject(playername, 100, 100);
            apple = new ammunition("apple", 2, 10, 1);
            melon = new ammunition("melon", 5, 30, 7);
            dung = new ammunition("dung", 3, 20, 3);


            

        }

        







    }

}
