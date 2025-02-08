using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        string playername = "";

        bool player1turn; //true player1 ist am zug, false der andere
        
        byte selectedAmmo; // 0 -> apple, 1 -> melon, 2 -> wrench, 3 -> dung
        Button pressedbutton;
        Color lastbordercolor;  //




        public MainPage()
        {
            InitializeComponent();


            startpage startPageInstance = new startpage();


            Player1Drawable player1Drawable = new Player1Drawable(startPageInstance);

            
            canvasView.Drawable = player1Drawable;


            /*
                        player1 = new Playerobject(playername, 100, 100);

            */

        }



        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {

            switch(selectedAmmo)
            {
                case 0:
                    Apple apple = new Apple();
                    projectileFired(apple);
                    break;
                case 1:
                    Melon melon = new Melon();
                    projectileFired(melon);
                    break;

                case 2:
                    Wrench wrench = new Wrench();
                    projectileFired(wrench);
                    break;
                case 3:
                    Dung dung = new Dung();
                    projectileFired(dung);
                    break;
            }
            

        }

        private void Btn_ammo_Clicked(object sender, EventArgs e)
        {
            if (pressedbutton != null)
            {
                pressedbutton.BorderColor = lastbordercolor;
            }

                pressedbutton = (Button)sender;

            switch (pressedbutton.AutomationId)
            {
                case "Btn_apple":
                    selectedAmmo = 0;
                    break;
                case "Btn_melon":
                    selectedAmmo = 1;
                    break;
                case "Btn_wrench":
                    selectedAmmo = 2;
                    break;
                case "Btn_dung":
                    selectedAmmo = 3;
                    break;
            }

            lastbordercolor = pressedbutton.BorderColor;
            pressedbutton.BorderColor = Color.FromRgb(255,255,0);

        }

        private void projectileFired(Ammunition proj)
        {


          
        }
    }

}
