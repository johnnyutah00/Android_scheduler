using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace Assignment1
{
    public class Games : INotifyPropertyChanged
    {

        private string name;
        private string description;
        private double rating;
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            // Check to ensure that a delegate is set
            if (PropertyChanged != null)
            {
                // Invoke the event
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                // Purpose of this method is to make sure we don't have to keep passing in the property name
            }
        }

        // Getters and Setters for Games class
        [PrimaryKey, AutoIncrement]
        public int gID { get; set; }

        public string gName
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string gDescription
        {
            get { return this.description; }
            set
            {
                if (value != this.description)
                {
                    this.description = value;
                    OnPropertyChanged();
                }
            }
        }

        public double gRating
        {
            get { return this.rating; }
            set
            {
                if (value != this.rating)
                {
                    this.rating = value;
                    OnPropertyChanged();
                }
            }
        }

        
    }
}
