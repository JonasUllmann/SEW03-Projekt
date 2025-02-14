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
            canvasView.Drawable = player1Drawable;

            canvasView.SizeChanged += OnCanvasViewSizeChanged;
        }

        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {
            switch (selectedAmmo)
            {
                case 0:
                    projectileFired(new Apple());
                    break;
                case 1:
                    projectileFired(new Melon());
                    break;
                case 2:
                    projectileFired(new Wrench());
                    break;
                case 3:
                    projectileFired(new Dung());
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
            System.Timers.Timer timer = new System.Timers.Timer(20); // 100 Millisekunden = 0,1 Sekunden
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, proj, frame, ref t); //Event wird bei jedem Timertick ausgeführt
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

            PointF pos = proj.projectilepath(t, player1.Power, player1.Angle, windspeed, false, (float)player1.Playerpos.X, (float)player1.Playerpos.Y, proj.weight);

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                frame.TranslationX = pos.X;
                frame.TranslationY = pos.Y;
            });

            t += 0.2f; // Inkrementieren Sie t
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
            var graphicsView = sender as GraphicsView;
            if (graphicsView != null)
            {
                float screenWidth = (float)graphicsView.Width;
                float screenHeight = (float)graphicsView.Height;

                // p1pos basierend auf der GraphicsView-Größe aktualisieren
                player1.UpdatePosition(new Point(screenWidth * 0.15f, screenHeight * 0.82f));

                // GraphicsView neu zeichnen
                graphicsView.Invalidate();
            }

        }
    }
}