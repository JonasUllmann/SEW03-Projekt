using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SEW03_Projekt.Classes;
using SEW03_Projekt.Drawables;
using System.Globalization;
using System.Timers;

namespace SEW03_Projekt
{
    public partial class MainPage : ContentPage
    {
        private float windspeed;
        private bool playerturn; // true player1 ist am Zug, false der andere
        private byte selectedAmmo = 0; // 0 -> apple, 1 -> melon, 2 -> wrench, 3 -> dung
        private Button pressedbutton;
        private Color standardbordercolor;
        
        private bool valuestimesten = false;

        private bool saveloaded;


        private float scale;

        private Playerobject player1;
        private Playerobject player2;

        private MainPageViewModel viewModel;

        Random rand;

        private TaskCompletionSource<bool> fireButtonPressedTcs;
        private TaskCompletionSource<bool> projectilegone;





        public MainPage(Playerobject player1, Playerobject player2, bool saveloaded)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            scale = Math.Min(
                (float)canvasView.Width / 500f,
                (float)canvasView.Height / 500f
            );

            standardbordercolor = Btn_dung.BorderColor;

            // ViewModel instanziieren und BindingContext setzen
            viewModel = new MainPageViewModel
            {
                Power = player1.Power,
                Angle = player1.Angle,
                Wind = windspeed,
                Player1Health = player1.Health,
                Player2Health = player2.Health 
            };
            BindingContext = viewModel;

            this.player1 = player1;
            this.player2 = player2;
            this.saveloaded = saveloaded;

            rand = new Random();

            startpage startPageInstance = new startpage();
            Player1Drawable player1Drawable = new Player1Drawable(player1);
            Player2Drawable player2Drawable = new Player2Drawable(player2);
            Draweverything draweverything = new Draweverything(player1Drawable, player2Drawable);

            canvasView.Drawable = draweverything;

            canvasView.SizeChanged += OnCanvasViewSizeChanged;

            Gamestart();
        }

        public void Gamestart()
        {
         
            unlockplayerbuttons();

            if (saveloaded)
            {
                LoadGameFromCsv();
            }

            btn_fire.IsEnabled = false;
            UpdateWindSpeed(randomizewind());

            Gameloop();
        }

        public async void Gameloop()
        {
            if (player1.Health <= 0 || player2.Health <= 0)
            {
                lockplayerbuttons();
                reset();
                    
            }
            while (true)
            {


                playerturn = true;

                // Warte darauf, dass der Fire-Button gedrückt wird
                fireButtonPressedTcs = new TaskCompletionSource<bool>();
                await fireButtonPressedTcs.Task;

                //warten bis das projektil weg ist

                projectilegone = new TaskCompletionSource<bool>();
                await projectilegone.Task;

                if (player1.Health <= 0 || player2.Health <= 0)
                {
                    lockplayerbuttons();
                    reset();
                    break;
                }

                

                if (!playerturn)
                {
                    await Task.Delay(2000);
                    botturn();
                }

                if (player1.Health <= 0 || player2.Health <= 0)
                {
                    lockplayerbuttons();
                    reset();
                    break;
                }

                UpdateWindSpeed(randomizewind());
                unlockplayerbuttons();
            }
        }

        public void reset()
        {
            DisplayAlert("Game ended.", "The game has ended", "Reset");
            player1.Health = player1.Maxhealth;
            player2.Health = player2.Maxhealth;

            btn_fire.IsEnabled = false;

            unlockplayerbuttons();
            Gameloop();




        }

        public void botturn()
        {
            

            int botammo = rand.Next(1, 5);

            player2.Power = rand.Next(65, 101);
            player2.Angle = rand.Next(45, 75);



            switch (botammo)
            {
                case 0:
                    projectileFired(new Apple(), player2, player1);
                    break;
                case 1:
                    projectileFired(new Melon(), player2, player1);
                    break;
                case 2:
                    projectileFired(new Wrench(), player2, player1);
                    break;
                case 3:
                    projectileFired(new Dung(), player2, player1);
                    break;
            }


            
            
        }


        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {
            lockplayerbuttons();

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

            playerturn = false;

            // Signalisiere, dass der Fire-Button gedrückt wurde
            fireButtonPressedTcs?.TrySetResult(true);
        }

        private void Btn_ammo_Clicked(object sender, EventArgs e)
        {
            btn_fire.IsEnabled = true;


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

            // Erstellen des Rahmens
            Frame frame = new Frame
            {
                WidthRequest = 10 * scale, // Kleinere Breite
                HeightRequest = 10 * scale, // Kleinere Höhe
                CornerRadius = 10, // Halbe Höhe/Breite für eine runde Form
                BorderColor = Colors.Transparent,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = proj.color, // Rahmenhintergrund ist transparent
                MinimumWidthRequest = 20, // Gleicher Wert wie WidthRequest
                MinimumHeightRequest = 20, // Gleicher Wert wie HeightRequest
            };



            MainThread.InvokeOnMainThreadAsync(() =>
            {
                frame.TranslationX = attackingplayer.Playerpos.X;
                frame.TranslationY = attackingplayer.Playerpos.Y * 0.9f;

                stlayoutgame.Children.Add(frame);
            });



            // Timer erstellen
            System.Timers.Timer timer = new System.Timers.Timer(20); // 100 Millisekunden = 0,1 Sekunden
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, proj, ref t, canvasView.Width, canvasView.Height, attackingplayer, attackedplayer, frame); //Event wird bei jedem Timertick ausgeführt
            timer.AutoReset = true; // Timer soll sich wiederholen
            timer.Enabled = true; // Timer starten
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e, Ammunition proj, ref float t, double gwidth, double gheight, Playerobject attackingplayer, Playerobject attackedplayer, Frame frame)
        {
            float powerscale = 1;

            if(scale < 1)
            {
                powerscale = scale * 1.77f;
            }
            

            PointF pos = proj.projectilepath(t, Convert.ToInt32(attackingplayer.Power * powerscale), attackingplayer.Angle, windspeed, attackingplayer.Isshootingleft, (float)attackingplayer.Playerpos.X, (float)attackingplayer.Playerpos.Y * 0.9f, proj.weight);

            if (attackedplayer.Hitbox.Contains(pos))
            {
                attackedplayer.Health -= proj.damage;
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (frame.Parent is Layout layout)
                    {
                        layout.Children.Remove(frame); // Frame entfernen
                    }

                    // Aktualisieren der Lebensanzeige im ViewModel
                    if (attackedplayer == player1)
                    {
                        viewModel.Player1Health = player1.Health;
                    }
                    else if (attackedplayer == player2)
                    {
                        viewModel.Player2Health = player2.Health;
                    }
                });
                ((System.Timers.Timer)sender).Stop();

                projectilegone?.TrySetResult(true);

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

                projectilegone?.TrySetResult(true);

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
                    if(player1.Power < 100)
                    {
                        if (valuestimesten) player1.Power += 10;
                        else player1.Power++;
                    }

                    if (player1.Power > 100) player1.Power = 100;
                    
                    viewModel.Power = player1.Power; // ViewModel aktualisieren
                    break;
                case "Power -":
                    
                    if(player1.Power > 1)
                    {
                        if (valuestimesten) player1.Power -= 10;
                        else player1.Power--;
                    }

                    if (player1.Power < 0) player1.Power = 1;
                    
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

            
            wind = rand.Next(-5, 5);

            return wind;
        }

        private void OnCanvasViewSizeChanged(object sender, EventArgs e)
        {
            
            if (canvasView?.Width == null || canvasView?.Height == null) return;

            // Skalierungsfaktor berechnen
            scale = Math.Min(
                (float)canvasView.Width / 500f,
                (float)canvasView.Height / 500f
            );

            // Neue Position für p1 (15% vom Rand, 82% von oben)
            PointF newPos1 = new PointF(
                (float)canvasView.Width * 0.15f,
                (float)canvasView.Height * 0.82f
            );
            // Neue Position für p2 (85% vom Rand, 82% von oben)
            PointF newPos2 = new PointF(
                (float)canvasView.Width * 0.85f,
                (float)canvasView.Height * 0.82f
);

            // Hitbox aktualisieren
            player1.UpdatePosition(newPos1, scale);
            player2.UpdatePosition(newPos2, scale);
            canvasView.Invalidate();
        }

        public void UpdateWindSpeed(float newWindSpeed)
        {
            windspeed = newWindSpeed;
            viewModel.Wind = windspeed; // ViewModel aktualisieren
        }

        public void lockplayerbuttons()
        {

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                btn_fire.IsEnabled = false;

                Btn_apple.IsEnabled = false;
                Btn_dung.IsEnabled = false;
                Btn_melon.IsEnabled = false;
                Btn_wrench.IsEnabled = false;

                Btn_timesten.IsEnabled = false;

                btn_powerp.IsEnabled = false;
                btn_powerm.IsEnabled = false;

                btn_anglep.IsEnabled = false;
                btn_anglem.IsEnabled = false;
            });


        }

        public void unlockplayerbuttons()
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                btn_fire.IsEnabled = true;

                Btn_apple.IsEnabled = true;
                Btn_dung.IsEnabled = true;
                Btn_melon.IsEnabled = true;
                Btn_wrench.IsEnabled = true;

                Btn_timesten.IsEnabled = true;

                btn_powerp.IsEnabled = true;
                btn_powerm.IsEnabled = true;

                btn_anglep.IsEnabled = true;
                btn_anglem.IsEnabled = true;
            });
        }

        private void btn_save_Clicked(object sender, EventArgs e)
        {
            SaveGameToCsv();
        }

        private void SaveGameToCsv()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savegame.csv");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine("Player1Health,Player2Health,Player1Power,Player1Angle");
                    writer.WriteLine($"{player1.Health.ToString(CultureInfo.InvariantCulture)}," +
                                    $"{player2.Health.ToString(CultureInfo.InvariantCulture)}," +
                                    $"{player1.Power.ToString(CultureInfo.InvariantCulture)}," +
                                    $"{player1.Angle.ToString(CultureInfo.InvariantCulture)}");
                }
                DisplayAlert("Erfolg", "Spielstand gespeichert!", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", $"Speichern fehlgeschlagen: {ex.Message}", "OK");
            }
        }

        private void LoadGameFromCsv()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savegame.csv");

            if (!File.Exists(filePath))
            {
                DisplayAlert("Warnung", "Kein Spielstand gefunden", "OK");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length < 2)
                {
                    DisplayAlert("Fehler", "Ungültiges Dateiformat", "OK");
                    return;
                }

                string[] values = lines[1].Split(',');

                if (values.Length == 4 &&
                    int.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out int p1Health) &&
                    int.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out int p2Health) &&
                    int.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out int p1Power) &&
                    int.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out int p1Angle))
                {
                    // Spielerdaten setzen
                    player1.Health = p1Health;
                    player2.Health = p2Health;
                    player1.Power = p1Power;
                    player1.Angle = p1Angle;

                    // ViewModel aktualisieren
                    viewModel.Player1Health = p1Health;
                    viewModel.Player2Health = p2Health;
                    viewModel.Power = p1Power;
                    viewModel.Angle = p1Angle;

                    DisplayAlert("Erfolg", "Spielstand geladen!", "OK");
                }
                else
                {
                    DisplayAlert("Fehler", "Ungültige Daten in der Datei", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", $"Laden fehlgeschlagen: {ex.Message}", "OK");
            }
        }
    }
}