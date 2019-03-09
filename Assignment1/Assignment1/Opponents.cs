using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace Assignment1
{
    public class Opponents : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string address;
        private string phone;
        private string email;

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

        // Getters and Setters for Opponent class
        [PrimaryKey, AutoIncrement]
        // This ID will be Autoincrement
        public int ID { get; set; }

        public string oFirstName
        {
            get { return this.firstName; }
            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string oLastName
        {
            get { return this.lastName; }
            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string oAddress
        {
            get { return this.address; }
            set
            {
                if (value != this.address)
                {
                    this.address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string oPhone
        {
            get { return this.phone; }
            set
            {
                if (value != this.phone)
                {
                    this.phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public string oEmail
        {
            get { return this.email; }
            set
            {
                if (value != this.email)
                {
                    this.email = value;
                    OnPropertyChanged();
                }
            }
        }

       
    }
}
