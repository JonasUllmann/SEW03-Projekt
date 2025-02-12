using Microsoft.Maui.Controls.PlatformConfiguration;
using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;
using System.Timers;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {

        private sbyte windspeed = 0;

        private string playername = "";

        private bool player1turn; //true player1 ist am zug, false der andere

        private byte selectedAmmo = 0; // 0 -> apple, 1 -> melon, 2 -> wrench, 3 -> dung
        private Button pressedbutton;
        private Color lastbordercolor;

        private Playerobject player1;
        private Playerobject player2;






        public MainPage(Playerobject player1, Playerobject player2)
        {
            InitializeComponent();

            this.player1 = player1;
            this.player2 = player2;

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

            switch (pressedbutton.Text)
            {
                case "Apple":
                    selectedAmmo = 0;
                    break;
                case "Melon":
                    selectedAmmo = 1;
                    break;
                case "Wrench":
                    selectedAmmo = 2;
                    break;
                case "Dung":
                    selectedAmmo = 3;
                    break;
            }

            lastbordercolor = pressedbutton.BorderColor;
            pressedbutton.BorderColor = Color.FromRgb(255,255,0);

        }

        private void projectileFired(Ammunition proj)
        {
            Ammunition amm = new Ammunition();
            float t = 0;

            Image projectile = new Image
            {
                Source = proj.source,
                WidthRequest = 50,
                HeightRequest = 50,
                Aspect = Aspect.AspectFill
            };

            // Erstellen des Rahmens
            Frame frame = new Frame
            {
                WidthRequest = 20,
                HeightRequest = 20,
                CornerRadius = 50,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = projectile
            };

            frame.TranslationX = player1.Playerpos.X;
            frame.TranslationY = player1.Playerpos.Y;

            stlayoutgame.Children.Add(frame);

            // Timer erstellen
            System.Timers.Timer timer = new System.Timers.Timer(100); // 100 Millisekunden = 0,1 Sekunden
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, proj, frame, ref t);
            timer.AutoReset = true; // Timer soll sich wiederholen
            timer.Enabled = true; // Timer starten
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e, Ammunition proj, Frame frame, ref float t)
        {
            if (t >= 150) // Stoppen, wenn t >= 150
            {
                ((System.Timers.Timer)sender).Stop(); // Timer stoppen
                return;
            }

            PointF pos = proj.projectilepath(t, player1.Power, player1.Angle, windspeed, false, ((float)player1.Playerpos.X), ((float)player1.Playerpos.Y), proj.weight);

            
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                frame.TranslationX = pos.X;
                frame.TranslationY = pos.Y;
            });


            t++; // Inkrementieren Sie t
        }

        private void PowerChanged(object sender, EventArgs e)
        {
            Button pressedbutton = (Button)sender;

            switch (pressedbutton.Text)
            {
                case "Power +":
                    player1.Power++;
                    break;
                case "Power -":
                    player1.Power--;
                    break;
            }
                
        }

        private void AngleChanged(object sender, EventArgs e)
        {
            Button pressedbutton = (Button)sender;

            switch (pressedbutton.Text)
            {
                case "Angle +":
                    player1.Angle++;
                    break;
                case "Angle -":
                    player1.Power--;
                    break;
            }

        }
    }

}
