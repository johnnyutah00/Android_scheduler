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
	public partial class SettingsPage : ContentPage
	{
        static mainDB database;
        
        /**
         * This page will simply reset the database and all matches. Games will remain.
         * */
        public SettingsPage ()
		{
			InitializeComponent ();
            database = App.Database;
            Title = "Settings";

            Button btnReset = new Button { Text = "Reset" };
            Label lblreset = new Label { Text = "Press to reset" };
            
            //Main layout for the reset button
            StackLayout mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { lblreset, btnReset }
            };

            btnReset.Clicked += (sender, e) =>
            {
                // Call clear tables function in database
                database.clearTables();
                // Go back to main screen after reset
                Navigation.PopToRootAsync();
            };

            Content = mainLayout;
		}
	}
}