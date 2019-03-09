using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace Assignment1
{
    public class Matches : INotifyPropertyChanged
    {
        private int oppID;
        private DateTime date;
        private string comments;
        private int gameID;
        private bool win;

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

        // Getters and Setters for Matches class
        [PrimaryKey, AutoIncrement]
        // This ID will be Autoincrementing
        public int mID { get; set; }

        public int opponent_id
        {
            get { return this.oppID; }
            set
            {
                if (value != this.oppID)
                {
                    this.oppID = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime mDate
        {
            get { return this.date; }
            set
            {
                if (value != this.date)
                {
                    this.date = value;
                    OnPropertyChanged();
                }
            }
        }

        public string mComments
        {
            get { return this.comments; }
            set
            {
                if (value != this.comments)
                {
                    this.comments = value;
                    OnPropertyChanged();
                }
            }
        }

        public int mGameID
        {
            get { return this.gameID; }
            set
            {
                if (value != this.gameID)
                {
                    this.gameID = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool mWin
        {
            get { return this.win; }
            set
            {
                if (value != this.win)
                {
                    this.win = value;
                    OnPropertyChanged();
                }
            }
        }

        
    }
}
