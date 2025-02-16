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
        private float windspeed; // Windgeschwindigkeit - nach links + nach rechts
        private bool playerturn; // true player1 ist am Zug, false der andere
        private byte selectedAmmo = 0; // 0 -> apple, 1 -> melon, 2 -> wrench, 3 -> dung
        private Button pressedbutton;
        private Color standardbordercolor; //speichert die normale Bordercolor um bei munitionswechsel wieder auf altes design zu switchen
        
        private bool valuestimesten = false; //wichtig für den x10 button alle werte bei winkel und power werden 10fach gewertet

        private bool saveloaded; //von startpage mitgegeben wenn true wird ein ehemaliger save geladen, sonst nicht


        private float scale; //skalierung von fast allen objekten relativ zur bildschirmgröße

        private Playerobject player1;
        private Playerobject player2;

        private MainPageViewModel viewModel; 

        Random rand; //random objekt gebraucht für windspeed und Bot

        //Taskcheckpoints es wird gewartet bis diese erfüllt sind
        private TaskCompletionSource<bool> fireButtonPressedTcs; 
        private TaskCompletionSource<bool> projectilegone;





        public MainPage(Playerobject player1, Playerobject player2, bool saveloaded)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false); // entfernt die Navigationbar am Handy

            //rechnet die Skalierung aus
            scale = Math.Min(
                (float)canvasView.Width / 500f,
                (float)canvasView.Height / 500f
            );

            //initialisiert die standard border color
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

            //verweist die draweverything klasse auf die Graphicsview im XAML -> Beide Figuren werden gezeichnet
            canvasView.Drawable = draweverything;

            //Wenn die größe des Graphicsview (also auch die des ganzen fensters) ändert wird die skalierung neu ausgerechnet und alles neugezeichnet
            canvasView.SizeChanged += OnCanvasViewSizeChanged;

            //eintritt in den Gameloop
            Gamestart();
        }

        public void Gamestart()
        {
         
            unlockplayerbuttons();

            //wenn ganz am anfang der Knopf zum laden eines Saves gedrückt wurde wird jetzt alles geladen
            if (saveloaded)
            {
                LoadGameFromCsv();
            }

            //Feuer button wird gesperrt bis eine Munition ausgewählt wurde
            btn_fire.IsEnabled = false;
            //windgeschwindigkeit wird random ausgewählt
            UpdateWindSpeed(randomizewind());

            Gameloop();
        }

        public async void Gameloop()
        {

            while (true)
            {
                //Überprüfung ob noch beide Spieler am Leben sind
                if (player1.Health <= 0 || player2.Health <= 0)
                {

                    lockplayerbuttons();
                    reset();

                }

                //Der spieler ist am zug
                playerturn = true;

                // Warte darauf, dass der Fire-Button gedrückt wird
                fireButtonPressedTcs = new TaskCompletionSource<bool>();
                await fireButtonPressedTcs.Task;

                //warten bis das projektil weg ist
                projectilegone = new TaskCompletionSource<bool>();
                await projectilegone.Task;

                //Überprüfung ob noch beide Spieler am Leben sind
                if (player1.Health <= 0 || player2.Health <= 0)
                {
                    lockplayerbuttons();
                    reset();
                    break;
                }


                //wenn Spieler nicht am zug ist
                if (!playerturn)
                {
                    //2sekunden warten
                    await Task.Delay(2000);
                    //zug des Bots
                    botturn();

                    //Überprüfung ob noch beide Spieler am Leben sind
                    if (player1.Health <= 0 || player2.Health <= 0)
                    {
                        lockplayerbuttons();
                        reset();
                        break;
                    }
                }
                



                UpdateWindSpeed(randomizewind());
                unlockplayerbuttons();
            }
        }

        public void reset()
        {
            //Popup das das Spiel vorbei ist und zurücksetzen der entsprechenden variablen
            DisplayAlert("Game ended.", "The game has ended", "Reset");
            player1.Health = player1.Maxhealth;
            player2.Health = player2.Maxhealth;

            btn_fire.IsEnabled = false;

            unlockplayerbuttons();
            //wiedereintritt in den Gameloop
            Gameloop();




        }

        public void botturn()
        {
            
            //zuerst zufällige ermittlung eines Munitionstypen
            int botammo = rand.Next(1, 5);

            //Dann je nach gewählter munition nocheinmal zufällige auswahl in begrenztem wertebereich je nach Projektil
            switch (botammo)
            {
                case 0:
                    player2.Power = rand.Next(60, 90);
                    player2.Angle = rand.Next(25, 45);
                    projectileFired(new Apple(), player2, player1);
                    break;
                case 1:
                    player2.Power = rand.Next(85, 101);
                    player2.Angle = rand.Next(35, 55);
                    projectileFired(new Melon(), player2, player1);
                    break;
                case 2:
                    player2.Power = rand.Next(85, 101);
                    player2.Angle = rand.Next(35, 55);
                    projectileFired(new Wrench(), player2, player1);
                    break;
                case 3:
                    player2.Power = rand.Next(60, 90);
                    player2.Angle = rand.Next(25, 45);
                    projectileFired(new Dung(), player2, player1);
                    break;
            }
        }


        private void FIRE_BTN_clicked(object sender, EventArgs e)
        {
            playerturn = false;
            lockplayerbuttons();


            //je nachdem welche munition ausgewählt wird jetzt das Projektil abgefeuert
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



            // Signalisiere, dass der Fire-Button gedrückt wurde
            fireButtonPressedTcs?.TrySetResult(true);
        }

        private void Btn_ammo_Clicked(object sender, EventArgs e)
        {
            //einer der 4 munitionsbuttons wurde gedrückt
            btn_fire.IsEnabled = true;

            //wenn vorher schon ein button ausgewählt war wird sein aussehen zurückgesetzt
            if (pressedbutton != null)
            {
                pressedbutton.BorderColor = standardbordercolor;
            }

            pressedbutton = (Button)sender;

            //ermittlung welcher der buttons gedrückt wurde
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
            //Färbung des Rahmens des gedrückten buttons gelb
            pressedbutton.BorderColor = Color.FromRgb(255, 255, 0);
        }
         
        private void projectileFired(Ammunition proj, Playerobject attackingplayer, Playerobject attackedplayer)
        {
            Ammunition amm = new Ammunition();
            
            float t = 0;

            // Erstellen des Projektils als Rahmen
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


            //bewegunng des Projektils an Spielerposition und hinzufügen zum parent layout
            //alles grafische muss im Mainthread passieren deswegen die komische methode
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                frame.TranslationX = attackingplayer.Playerpos.X;
                frame.TranslationY = attackingplayer.Playerpos.Y * 0.9f;

                stlayoutgame.Children.Add(frame);
            });



            // Timer erstellen
            System.Timers.Timer timer = new System.Timers.Timer(20); // 20 Millisekunden = 0,02 Sekunden
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, proj, ref t, canvasView.Width, canvasView.Height, attackingplayer, attackedplayer, frame); //Event wird bei jedem Timertick ausgeführt
            timer.AutoReset = true; // Timer soll sich wiederholen
            timer.Enabled = true; // Timer starten
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e, Ammunition proj, ref float t, double gwidth, double gheight, Playerobject attackingplayer, Playerobject attackedplayer, Frame frame)
        {
            //skaliert die Power abhängig davon wie groß der Bildschirm ist
            float powerscale = 1.6f;

            if(scale < 1)
            {
                powerscale = scale * 1.77f;
            }

            
            //berechnung der Flugbahn
            PointF pos = proj.projectilepath(t, Convert.ToInt32(attackingplayer.Power * powerscale), attackingplayer.Angle, windspeed, attackingplayer.Isshootingleft, (float)attackingplayer.Playerpos.X, (float)attackingplayer.Playerpos.Y * 0.9f, proj.weight);

            //wenn das Projektil den Spieler trifft werden ihm dementsprechend leben abgezogen
            if (attackedplayer.Hitbox.Contains(pos))
            {
                attackedplayer.Health -= proj.damage;
                //wieder können ui dinge nur im Mainthread verändert werden
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
                //stoppt den timer
                ((System.Timers.Timer)sender).Stop();

                //markiert den Task aus dem gameloop als erledigt -> programm läuft weiter
                projectilegone?.TrySetResult(true);

                return;
            }
            //wenn das projektil den Bildschirm nach unten links oder rechts verlässt
            else if (pos.X < 0 || pos.X > gwidth || pos.Y > gheight * 0.84f)
            {
                //wieder können ui dinge nur im Mainthread verändert werden
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (frame.Parent is Layout layout)
                    {
                        layout.Children.Remove(frame); // Frame aus dem Layout entfernen
                    }
                });
                //stoppt den Timer
                ((System.Timers.Timer)sender).Stop();

                //markiert den Task aus dem gameloop als erledigt -> programm läuft weiter
                projectilegone?.TrySetResult(true);

                return;
            }

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                //updatet das Projektil
                frame.TranslationX = pos.X;
                frame.TranslationY = pos.Y;
            });

            //zeitpunkt wird leicht erhöht damit die flugbahn flüssig bleibt 
            t += 0.2f;
        }


        private void PowerChanged(object sender, EventArgs e)
        {
            Button pressedbutton = (Button)sender;

            switch (pressedbutton.Text)
            {

                case "Power +":
                    //wenn power kleiner als hundert ist dementsprechend erhöht 
                    if(player1.Power < 100)
                    {
                        if (valuestimesten) player1.Power += 10;
                        else player1.Power++;
                    }

                    if (player1.Power > 100) player1.Power = 100;
                    
                    viewModel.Power = player1.Power; // ViewModel aktualisieren
                    break;
                case "Power -":
                    //wenn power größer als 1 ist dementsprechend verringert 
                    if (player1.Power > 1)
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
                //winkel erhöht oder verringert
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
            //invertiert den boolean -> wenn variable im dann true ist wird winkel und power *10 gemacht
            valuestimesten = !valuestimesten;


            //gelber rand
            if (valuestimesten)
            {
                Btn_timesten.BorderColor = Color.FromRgb(255, 255, 0);
            }
            else
            {
                Btn_timesten.BorderColor = standardbordercolor;
            }

        }

        //generiert random zahl zwischen -5 und 5 für wind
        public float randomizewind()
        {
            float wind;

            
            wind = rand.Next(-5, 5);

            return wind;
        }

        //Wenn Bildschirmgröße verändert wird die ganze Skalierung aktualisiert
        private void OnCanvasViewSizeChanged(object sender, EventArgs e)
        {
            //wenn canvasview existiert aber null ist wird abgebrochen
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

        //aktualisiert das den Windspeed und das Viewmodel dazu
        public void UpdateWindSpeed(float newWindSpeed)
        {
            windspeed = newWindSpeed;
            viewModel.Wind = windspeed; // ViewModel aktualisieren
        }

        //sperrt alle buttons für den spieler
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

        //entsperrt alle buttons für den Spieler
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

        //button zum speichern
        private void btn_save_Clicked(object sender, EventArgs e)
        {
            SaveGameToCsv();
        }

        //speichert das Spiel in eine CSV
        private void SaveGameToCsv()
        {
            //Pfad abhängig von Betriebssystem
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savegame.csv");

            try
            {
                //schreibt zuerst die Spaltenüberschriften und dann die Werte in die 2. zeile
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

            //gibt feedback über popups
        }

        //lädt das Spiel von einer CSV
        private void LoadGameFromCsv()
        {
            //wieder os abhängiger Pfad
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savegame.csv");

            //Wenn keine Datei gefunden nur Warnung und abbruch
            if (!File.Exists(filePath))
            {
                DisplayAlert("Warnung", "Kein Spielstand gefunden", "OK");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                //wenn es mehr als 2 zeilen gibt wird abgebrochen
                if (lines.Length < 2)
                {
                    DisplayAlert("Fehler", "Ungültiges Dateiformat", "OK");
                    return;
                }

                string[] values = lines[1].Split(',');
                //wenn es gelingt alle werte in ints zu konvertieren werden die Werte eingesetzt
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