namespace SEW03_Projekt
{
    public partial class App : Application
    {

        public static MainPage MainPageInstance { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPageInstance = new MainPage();
            MainPage = new NavigationPage(new startpage());

        }
    }
}
