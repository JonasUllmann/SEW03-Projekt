namespace SEW03_Projekt;
using SEW03_Projekt.Classes;

public partial class startpage : TabbedPage
{

    string botdifficulty = "";

	public startpage()
	{
        
        InitializeComponent();

        var window = Application.Current?.MainPage?.Window;
        if (window != null)
        {
            // Momentane Größe
            double currentWidth = window.Width;
            double currentHeight = window.Height;

            // Mindestgröße setzen
            window.MinimumWidth = currentWidth;
            window.MinimumHeight = currentHeight;

            //maximumgröße
            window.MaximumWidth = currentWidth;
            window.MaximumHeight = currentHeight;
        }
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