using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMatchesPage : ContentPage
    {
        static mainDB database;
       
        private Matches currentMatch;
       /**
        * This page will display current matches and add ability to add a new match
        * */
        public NewMatchesPage(Opponents opponent)
        {
            InitializeComponent();
            database = App.Database;
            Title = "Opponents";     
            
            // Layout for top items
            StackLayout topLayout = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

            // Add matches to listview
            ListView matchListView = new ListView
            {
                ItemsSource = database.GetMatchesByID(opponent.ID),
                //ItemsSource = matchList,
                RowHeight = 75,
                ItemTemplate = new DataTemplate(typeof(matchCell)),
                HeightRequest = 750
            };

            
            // Add to top layout
            topLayout.Children.Add(matchListView);

            /////////// Table Section Below /////////////////////
            StackLayout tableLayout = new StackLayout { VerticalOptions = LayoutOptions.Center };
            TableView tableView = new TableView { Intent = TableIntent.Form };
            tableView.HeightRequest = 700;

            // Cells go in sections, sections in root
            DatePicker dPicker = new DatePicker { Date = DateTime.Now, Format = "D" };

            ViewCell vDate = new ViewCell();
            vDate.View = dPicker;

            // Comments section
            EntryCell eComments = new EntryCell { Label = "Comment: " };
            // Picker for Game selection
            Picker pGame = new Picker
            {
                ItemsSource = database.GetAllGames(),
                ItemDisplayBinding = new Binding("gName"),
                Title = "Game:"
            };
            // Picker must be placed in ViewCell
            ViewCell vPicker = new ViewCell();
            vPicker.View = pGame;
            SwitchCell sWin = new SwitchCell { Text = "Win?" };

            TableSection tableSection = new TableSection("Add Match") { vDate, eComments, vPicker, sWin };
            tableView.Root = new TableRoot { tableSection };
            tableLayout.Children.Add(tableView);

            // Create button to add matches
            Button btnAdd = new Button { Text = "Add", HorizontalOptions = LayoutOptions.Center };

            btnAdd.Clicked += (sender, e) =>
            {
                // Check to make sure that we're updating an item already in database
                if (currentMatch != null)
                {
                    currentMatch.mDate = dPicker.Date;
                    currentMatch.mComments = eComments.Text;
                    currentMatch.mWin = sWin.On;
                    currentMatch.mGameID = ((Games)pGame.SelectedItem).gID;
                    // Update match     
                    database.SaveMatch(currentMatch);
                    // Update list 
                    matchListView.ItemsSource = database.GetMatchesByID(currentMatch.opponent_id);

                    currentMatch = null;
                }
                else
                {
                    // Create new match to save
                    Matches newMatch = new Matches
                    {
                        mDate = dPicker.Date,
                        mComments = eComments.Text,
                        mGameID = ((Games)pGame.SelectedItem).gID,
                        opponent_id = opponent.ID,
                        mWin = sWin.On
                    };
                    // Save new match to database
                    database.SaveMatch(newMatch);
                    // Update list
                    matchListView.ItemsSource = database.GetMatchesByID(newMatch.opponent_id);
                }
            };

            matchListView.ItemTapped += (sender, e) =>
            {
                matchListView.SelectedItem = null;
                // Set current item equal to the object which was tapped
                // Used to be able to know if we need to update, or create new match
                currentMatch = (Matches)e.Item;
                dPicker.Date = currentMatch.mDate;
                eComments.Text = currentMatch.mComments;
                // Index needs to be offset by one, since picker and database do not
                // start at the same numbers. One start at 0 and the other other 1.
                pGame.SelectedIndex = currentMatch.mGameID - 1;
                sWin.On = currentMatch.mWin;
                
            };

            // Create main layout
            StackLayout mainLayout = new StackLayout
            {
                Children = { topLayout, tableLayout, btnAdd }
            }; 

            Content = mainLayout;

        }
    }

    public class matchCell : ViewCell
    {
        static mainDB database;
       
        private Label lblFullName;
        private Label lblGameName;
        
        /**
         * Display option for the match cell and how it will be displayed on page
         * */
        public matchCell()
        {
            database = App.Database;

            lblFullName = new Label { FontAttributes = FontAttributes.Bold };
            
            Label lblDate = new Label { FontSize = 12 };
            lblDate.SetBinding(Label.TextProperty, "mDate", stringFormat:"{0:D}");

            lblGameName = new Label { FontSize = 12 };
            // Menu item to be able to delete a match
            MenuItem menuItem = new MenuItem
            {
                Text = "Delete Match?",
                IsDestructive = true
            };

            menuItem.Clicked += (sender, e) =>
            {
                // Get parent's list to remove item
                ListView parent = (ListView)this.Parent;
                // Remove item from database
                database.DeleteMatch(this.BindingContext as Matches);
                // Update list
                parent.ItemsSource = database.GetAllMatches();
            };

            ContextActions.Add(menuItem);

            View = new StackLayout
            {
                Spacing = 2,
                Padding = 5,
                Children = { lblFullName, lblDate, lblGameName }
            };
        }

        /**
         * This function will allow us to be able to dispaly the names properly.
         * I does so since the object hasn't been bound on click, so it would normally
         * return null. Using OnBindingContextChanged allows us to use an 
         * object to get results.
         * */
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Matches match = (Matches)this.BindingContext;
            if (match != null)
            {
                Opponents opponent = database.GetOpponent(match.opponent_id);
                Games game = database.GetGame(match.mGameID);

                lblFullName.Text = opponent.oFirstName + " " + opponent.oLastName;
                lblGameName.Text = game.gName;
            }
    
        }
    }
}