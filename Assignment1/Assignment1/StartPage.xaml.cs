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
    public partial class StartPage : ContentPage
    {

        // Connection to database       
        static mainDB database;       
        public ListView opplistView;

        /**
         * Main start page. This page will display current opponents in the database
         * by first name, last name, and phone number
         * */
        public StartPage()
        {
            InitializeComponent();
            database = App.Database;
            
            // Set Title
            Title = "Opponents";

            // Use all opponents list for source in ListView
            opplistView = new ListView
            {
                ItemsSource = database.GetAllOpponents(),
                RowHeight = 50,
                ItemTemplate = new DataTemplate(typeof(OpponentCell))
            };

            // When Opponent gets tapped
            opplistView.ItemTapped += (sender, e) =>
            {
                opplistView.SelectedItem = null;
                // Go to matches page
                Navigation.PushAsync(new NewMatchesPage(e.Item as Opponents));
                

            };

            // Add "Add new Opponent Button"
            Button btnNewOpponent = new Button { Text = "Add New Opponent" };

            btnNewOpponent.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new NewOpponentPage());
            };
            
            // Main StackView layout
            StackLayout mainLayout = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 25),
                Children = { opplistView, btnNewOpponent}
            };

            
            Content = mainLayout;
            
        }
        /**
         * Reload ItemsSource after new Opponents have been added
         * */
        protected override void OnAppearing()
        {
            opplistView.ItemsSource = database.GetAllOpponents();
            base.OnAppearing();
        }
    }

    /**
     * ViewCell for Opponents: How they will appear in the list
     * */
    public class OpponentCell : ViewCell
    {
        // Connection to database
        static mainDB database;
        
        public OpponentCell()
        {
            database = App.Database;
            
            // Display Opponents first name, last name, and phone number on main page
            var firstName = new Label();
            firstName.SetBinding(Label.TextProperty, "oFirstName");

            var lastName = new Label();
            lastName.SetBinding(Label.TextProperty, "oLastName");         

            var phone = new Label();
            phone.SetBinding(Label.TextProperty, "oPhone");


            // Adding menu item for long click or swipe
            MenuItem menuItem = new MenuItem
            {
                Text = "Delete Opponent?",
                IsDestructive = true
            };

            menuItem.Clicked += (sender, e) =>
            {
                // Get the parent's ListView
                ListView parent = (ListView)this.Parent;
                // Remove all matches and opponents
                database.DeleteAllMatches((this.BindingContext as Opponents).ID);
                database.DeleteOpponent(this.BindingContext as Opponents);
                //Update ItemsSource list
                parent.ItemsSource = database.GetAllOpponents();               
                
            };

            ContextActions.Add(menuItem);
            // Place names side-by-side
            var nameStack = new StackLayout
            {
                Spacing = 4,
                Orientation = StackOrientation.Horizontal,
                Children = { firstName, lastName }
            };

            // Define what the content will look like
            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 25,
                Padding = 10,
                Children = { nameStack, phone }
            };

        }

    }
}