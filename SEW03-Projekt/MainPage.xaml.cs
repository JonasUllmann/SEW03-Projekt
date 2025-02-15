using Microsoft.Maui.Controls;
using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;
using System.Timers;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {
        private float windspeed;
        private string playername = "";
        private bool player1turn; // true player1 ist am Zug, false der andere
        private byte selectedAmmo = 0; // 0 -> apple, 1 -> melon, 2 -> wrench, 3 -> dung
        private Button pressedbutton;
        private Color standardbordercolor;
        private bool valuestimesten = false;

        private Playerobject player1;
        private Playerobject player2;

        private MainPageViewModel viewModel;



        public MainPage(Playerobject player1, Playerobject player2)
        {
            InitializeComponent();

            standardbordercolor = Btn_dung.BorderColor;

            // ViewModel instanziieren und BindingContext setzen
            viewModel = new MainPageViewModel
            {
                Power = player1.Power,
                Angle = player1.Angle,
                Wind = windspeed
            };
            BindingContext = viewModel;

            this.player1 = player1;
            this.player2 = player2;

            startpage startPageInstance = new startpage();
            Player1Drawable player1Drawable = new Player1Drawable(player1);
            Player2Drawable player2Drawable = new Player2Drawable(player2);
            Draweverything draweverything = new Draweverything(player1Drawable, player2Drawable);

            canvasView.Drawable = draweverything;

            canvasView.SizeChanged += OnCanvasViewSizeChanged;
        }

        public void gameloop()
        {
            while (true)
            {




            }



        }





        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {
            switch (selectedAmmo)
            {
                case 0:
                    projectileFired(new Apple(), player1, player2);
                    break;
                case 1:
                    projectileFired(new Melon(), player1, player2);
                    break;
                case 2:
                    projectileFired(new Wrench(), player1, player2);
                    break;
                case 3:
                    projectileFired(new Dung(), player1, player2);
                    break;
            }
        }

        private void Btn_ammo_Clicked(object sender, EventArgs e)
        {
            if (pressedbutton != null)
            {
                pressedbutton.BorderColor = standardbordercolor;
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
            pressedbutton.BorderColor = Color.FromRgb(255, 255, 0);
        }

        private void projectileFired(Ammunition proj, Playerobject attackingplayer, Playerobject attackedplayer)
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

            frame.TranslationX = attackingplayer.Playerpos.X;
            frame.TranslationY = attackingplayer.Playerpos.Y * 0.9f;

            stlayoutgame.Children.Add(frame);

            // Timer erstellen
            System.Timers.Timer timer = new System.Timers.Timer(20); // 100 Millisekunden = 0,1 Sekunden
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, proj, ref t, canvasView.Width, canvasView.Height, attackingplayer, attackedplayer, frame); //Event wird bei jedem Timertick ausgeführt
            timer.AutoReset = true; // Timer soll sich wiederholen
            timer.Enabled = true; // Timer starten
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e, Ammunition proj, ref float t, double gwidth, double gheight, Playerobject attackingplayer, Playerobject attackedplayer, Frame frame)
        {
            PointF pos = proj.projectilepath(t, attackingplayer.Power, attackingplayer.Angle, windspeed, attackingplayer.Isshootingleft, (float)attackingplayer.Playerpos.X, (float)attackingplayer.Playerpos.Y * 0.9f, proj.weight);

            if (attackedplayer.Hitbox.Contains(pos))
            {
                attackedplayer.Health -= proj.damage;
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (frame.Parent is Layout layout)
                    {
                        layout.Children.Remove(frame); // Frame aus dem Layout entfernen
                    }
                });
                ((System.Timers.Timer)sender).Stop();
                return;
            }
            else if (pos.X < 0 || pos.X > gwidth || pos.Y > gheight * 0.84f)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (frame.Parent is Layout layout)
                    {
                        layout.Children.Remove(frame); // Frame aus dem Layout entfernen
                    }
                });
                ((System.Timers.Timer)sender).Stop();
                return;
            }

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                frame.TranslationX = pos.X;
                frame.TranslationY = pos.Y;
            });

            t += 0.2f;
        }


        private void PowerChanged(object sender, EventArgs e)
        {
            Button pressedbutton = (Button)sender;

            switch (pressedbutton.Text)
            {
                case "Power +":
                    if (valuestimesten) player1.Power += 10;
                    else player1.Power++;
                    viewModel.Power = player1.Power; // ViewModel aktualisieren
                    break;
                case "Power -":
                    if (valuestimesten) player1.Power -= 10;
                    else player1.Power--;
                    viewModel.Power = player1.Power; // ViewModel aktualisieren
                    break;
            }
        }

        private void AngleChanged(object sender, EventArgs e)
        {
            Button pressedbutton = (Button)sender;

            switch (pressedbutton.Text)
            {
                case "Angle +":
                    if (valuestimesten) player1.Angle += 10;
                    else player1.Angle++;
                    viewModel.Angle = player1.Angle; // ViewModel aktualisieren
                    break;
                case "Angle -":
                    if (valuestimesten) player1.Angle -= 10;
                    else player1.Angle--;
                    viewModel.Angle = player1.Angle; // ViewModel aktualisieren
                    break;
            }
        }

        private void Btn_timesten_Clicked(object sender, EventArgs e)
        {
            valuestimesten = !valuestimesten;

            if (valuestimesten)
            {
                Btn_timesten.BorderColor = Color.FromRgb(255, 255, 0);
            }
            else
            {
                Btn_timesten.BorderColor = standardbordercolor;
            }

        }

        public float randomizewind()
        {
            float wind;

            Random rand = new Random();
            wind = rand.Next(-10, 10);

            return wind;
        }

        private void OnCanvasViewSizeChanged(object sender, EventArgs e)
        {
            GraphicsView graphicsView = sender as GraphicsView;
            if (graphicsView?.Width == null || graphicsView.Height == null) return;

            // Skalierungsfaktor berechnen
            float scale = Math.Min(
                (float)graphicsView.Width / 500f,
                (float)graphicsView.Height / 500f
            );

            // Neue Position für p1 (15% vom Rand, 82% von oben)
            PointF newPos1 = new PointF(
                (float)graphicsView.Width * 0.15f,
                (float)graphicsView.Height * 0.82f
            );
            // Neue Position für p2 (85% vom Rand, 82% von oben)
            PointF newPos2 = new PointF(
                (float)graphicsView.Width * 0.85f,
                (float)graphicsView.Height * 0.82f
);

            // Hitbox aktualisieren
            player1.UpdatePosition(newPos1, scale);
            player2.UpdatePosition(newPos2, scale);
            graphicsView.Invalidate();
        }
    }
}