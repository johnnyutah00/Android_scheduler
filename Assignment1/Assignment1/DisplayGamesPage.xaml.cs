using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisplayGamesPage : ContentPage
	{
        // Connection to database
        static mainDB database;    

        /**
         *  This page will display all games and how many times they've been played.
         *  When a match has been deleted, it will update the Games page as well.
         * */
        public DisplayGamesPage ()
		{
			InitializeComponent ();
            database = App.Database;
            Title = "Previous";
            ListView gameList = new ListView
            {
                ItemsSource = database.GetAllGames(),
                RowHeight = 100,
                ItemTemplate = new DataTemplate(typeof(gameCell))
            };

            Content = gameList;
        }
	}

    /**
     *  GameCell to display how games will appear on the page
     * */
    public class gameCell : ViewCell
    {
        // Connection to database       
        static mainDB database;
        private int counter = 0;
        Label totalGames;
       
        /**
         *  Displaying the game name, description, rating and number of matches
         * */
        public gameCell()
        {
            database = App.Database;
            
            Label gameName = new Label { FontAttributes = FontAttributes.Bold};
            gameName.SetBinding(Label.TextProperty, "gName");

            Label gameDescription = new Label();
            gameDescription.SetBinding(Label.TextProperty, "gDescription");

            Label gameRating = new Label ();
            gameRating.SetBinding(Label.TextProperty, "gRating");

            Label numMatches = new Label { Text = "# Matches: "};
            totalGames = new Label();

            StackLayout numMatchLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { numMatches, totalGames }
            };

           
            StackLayout descRatingLayout = new StackLayout
            {
              Orientation = StackOrientation.Horizontal,
              Children = {gameDescription, gameRating}
            };
           
            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Spacing = 10,
                Padding = new Thickness(25,5,0,15),
                Children = { gameName , descRatingLayout, numMatchLayout}
                
            };
        }

        /**
         * Counting the total amount of Matches that have been played and
         * displaying it on the totalGames text field
         * */
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Games game = (Games)this.BindingContext;
            List<Matches> allMatches = database.GetAllMatches();
            foreach (Matches match in allMatches)
            {
                if (match.mGameID == game.gID)
                {
                    counter++;
                }
            }
            totalGames.Text = Convert.ToString(counter);
        }


    }
}