namespace SEW03_Projekt;
using SEW03_Projekt.Classes;

public partial class startpage : TabbedPage
{
    string botdifficulty = "";

    private Playerobject player1;
    private Playerobject player2;

    public Playerobject Player1 { get => player1; set => player1 = value; }
    public Playerobject Player2 { get => player2; set => player2 = value; }

    public float Player1X => (float)player1.Playerpos.X;
    public float Player1Y => (float)player1.Playerpos.Y;

    public startpage()
	{
        
        InitializeComponent();

        }

    public string Botdifficulty { get => botdifficulty; set => botdifficulty = value; }

    private void btnbot_Clicked(object sender, EventArgs e)
    {
        
        if (entplayername.Text != null && entplayername.Text != "")
        {
            Navigation.PushAsync(new MainPage());
        }

    }

    private void btnplayer_Clicked(object sender, EventArgs e)
    {
        Botdifficulty = DifficultyPicker.SelectedItem?.ToString();
        if (entplayer1name.Text != null && entplayer1name.Text != "")
        {
            if (entplayer2name.Text != null && entplayer2name.Text != "")
            {
                Navigation.PushAsync(new MainPage());
            }
        }
    }


}