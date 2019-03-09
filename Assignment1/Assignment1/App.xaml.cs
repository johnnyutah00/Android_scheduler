using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Assignment1
{
	public partial class App : Application
	{

        //// Connection to database
        //// Available to all classes in shared project
        static mainDB database;
        public static mainDB Database
        {
            get
            {
                // If DB is not initializaed
                if (database == null)
                {
                    database = new mainDB(DependencyService.Get<IFileHelper>().GetLocalFilePath("mainDB.db3")); 
                    // Use dependency service to find the "right" file helper to use based on platform
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
            // Creating ToolBar items
            ToolbarItem tbGame = new ToolbarItem { Text = "Games" };
            ToolbarItem tbSettings = new ToolbarItem { Text = "Settings" };
            // Functionality when toolbar items have been clicked
            tbGame.Clicked += (sender, e) =>
            {
                MainPage.Navigation.PushAsync(new DisplayGamesPage());
            };

            tbSettings.Clicked += (sender, e) =>
            {
                MainPage.Navigation.PushAsync(new SettingsPage());
            };
            // Adding two items to ToolBar
            MainPage.ToolbarItems.Add(tbGame);
            MainPage.ToolbarItems.Add(tbSettings);
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

    // Needed for SQLite. returns where database should be
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
