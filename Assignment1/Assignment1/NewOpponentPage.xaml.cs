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
    public partial class NewOpponentPage : ContentPage
    {
        static mainDB database;
        /**
         *  This page will handle adding a new opponent to the database
         * */
        public NewOpponentPage()
        {
            InitializeComponent();
            Title = "Opponents";
            database = App.Database;

            // Main layout to add components to when finished
            StackLayout mainLayout = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

            StackLayout tableLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center
            };

            TableView tableView = new TableView { Intent = TableIntent.Form };

            // Cells have to go in sections and sections in Root
            EntryCell eFirstName = new EntryCell { Label = "First Name: " };
            EntryCell eLastName = new EntryCell { Label = "Last Name: " };
            EntryCell eAddress = new EntryCell { Label = "Address: " };
            EntryCell ePhone = new EntryCell { Label = "Phone: " };
            EntryCell eEmail = new EntryCell { Label = "Email: " };

            TableSection section = new TableSection("Add New Opponent") { eFirstName, eLastName, eAddress, ePhone, eEmail };

            // Root
            tableView.Root = new TableRoot { section };

            // Add to table layout
            tableLayout.Children.Add(tableView);

            Button btnSave = new Button { Text = "Save" };
            btnSave.Clicked += (sender, e) =>
            {
                // Create new opponent to be saved in database
                Opponents newOpp = new Opponents
                {
                    oFirstName = eFirstName.Text,
                    oLastName = eLastName.Text,
                    oAddress = eAddress.Text,
                    oPhone = ePhone.Text,
                    oEmail = eEmail.Text
                    
                };
                // Persiste new opponent to database                
                database.SaveOpponent(newOpp);
                // Reset string fields to empty                                
                eFirstName.Text = string.Empty;
                eLastName.Text = string.Empty;
                eAddress.Text = string.Empty;
                ePhone.Text = string.Empty;
                eEmail.Text = string.Empty;
            };
            // Add to main layout
            mainLayout.Children.Add(tableLayout);
            mainLayout.Children.Add(btnSave);

            Content = mainLayout;
        }
    }
}