namespace SEW03_Projekt
{
    using SEW03_Projekt.Classes;
    using SEW03_Projekt.Drawables;

    public partial class startpage : TabbedPage
    {
        string botdifficulty = "";

        private Player1Drawable _player1Drawable;

        private Playerobject player1;
        private Playerobject player2;

        public Playerobject Player1 { get => player1; set => player1 = value; }
        public Playerobject Player2 { get => player2; set => player2 = value; }


        public startpage()
        {
            InitializeComponent();





            player1 = new Playerobject("Gert", 100, 100, new Point(300, 200));
            player2 = new Playerobject("Gundula", 100, 100, new Point(700, 200));

        }

        public string Botdifficulty { get => botdifficulty; set => botdifficulty = value; }

        private void btnbot_Clicked(object sender, EventArgs e)
        {
            if (entplayername.Text != null && entplayername.Text != "")
            {
                Navigation.PushAsync(new MainPage(player1, player2));
            }
        }

       /* private void btnplayer_Clicked(object sender, EventArgs e)
        {
            Botdifficulty = DifficultyPicker.SelectedItem?.ToString();
            if (entplayer1name.Text != null && entplayer1name.Text != "")
            {
                if (entplayer2name.Text != null && entplayer2name.Text != "")
                {
                    Navigation.PushAsync(new MainPage());
                }
            }
        }*/
    }
}
