using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;

namespace Assignment1
{
    public class mainDB
    {
        // Connection to database
        readonly SQLiteConnection database;

        public mainDB(string dbPath)
        {
            database = new SQLiteConnection(dbPath); // Initialize the DB connection
            database.CreateTable<Opponents>();
            database.CreateTable<Matches>();
            database.CreateTable<Games>();
            createGames();
            
        }

        /*****************************************************
                  Game Section of Database
         *****************************************************/
        // Save or update game
        public int SaveGame(Games game)
        {
            if (game.gID != 0)
            {
                return database.Update(game);
            }
            else
            {
                return database.Insert(game);
            }
        }

        // Delete one game
        public int DeleteGame(Games game)
        {
            return database.Delete(game);
        }

        // Get all games
        public List<Games> GetAllGames()
        {
            return database.Table<Games>().ToList<Games>();
        }

        // Get one game
        public Games GetGame(int matchID)
        {
            return database.Table<Games>().Where(i => i.gID == matchID).FirstOrDefault();
        }

     

        /*****************************************************
                  Match Section of Database
         *****************************************************/

        // Save or update match
        public int SaveMatch(Matches match)
        {
            if (match.mID != 0)
            {
                return database.Update(match);
            }
            else
            {
                return database.Insert(match);
            }
        }

        // Delete one match
        public int DeleteMatch(Matches match)
        {
            return database.Delete(match);
        }

        // Delete all matches for specific opponent
        public void DeleteAllMatches(int id)
        {
            List<Matches> listToDelete = GetMatchesByID(id);
            foreach(Matches match in listToDelete)
            {
                database.Delete(match);
            }
        }

        // Get all matches
        public List<Matches> GetAllMatches()
        {
            return database.Table<Matches>().ToList<Matches>();
        }

        // Get one match
        public Matches GetMatch(int id)
        {
            return database.Table<Matches>().Where(i => i.mID == id).FirstOrDefault();
        }

        // Get matches from specific opponent
        public List<Matches> GetMatchesByID(int id)
        {
            return database.Table<Matches>().Where(i => i.opponent_id == id).ToList<Matches>();
            //.ToList<Matches>()
        }

        // Get one game
        public string GetGameByID(int id)
        {
            return database.Table<Games>().Where(i => i.gID == id).FirstOrDefault().gName;
        }

        /*****************************************************
                  Opponent Section of Database
         *****************************************************/

        // Save or update opponent
        public int SaveOpponent(Opponents opponent)
        {
            if (opponent.ID != 0)
            {
                return database.Update(opponent);
            }
            else
            {
                return database.Insert(opponent);
            }
        }

        // Delete one opponent
        public int DeleteOpponent(Opponents opponent)
        {
            return database.Delete(opponent);
        }

        
        // Get all opponents
        public List<Opponents> GetAllOpponents()
        {
            return database.Table<Opponents>().ToList<Opponents>();
        }

        // Get one opponent
        public Opponents GetOpponent(int id)
        {
            return database.Table<Opponents>().Where(i => i.ID == id).FirstOrDefault();
        }

        /*****************************************************
                  Clear Database
         *****************************************************/
         public void clearTables()
        {
            database.DropTable<Opponents>();
            database.DropTable<Matches>();
            database.DropTable<Games>();
            database.CreateTable<Opponents>();
            database.CreateTable<Matches>();
            database.CreateTable<Games>();
            createGames();
        }

        // Create static games that will always remain in database
        public void createGames()
        {
            if (database.Table<Games>().Count() == 0)
            {
                Games game = new Games
                {
                    gName = "Chess",
                    gDescription = "Simple grid game",
                    gRating = 9.5
                };
                Games game2 = new Games
                {
                    gName = "Checkers",
                    gDescription = "Simpler grid game",
                    gRating = 5.0
                };
                Games game3 = new Games
                {
                    gName = "Dominoes",
                    gDescription = "Blocks game",
                    gRating = 6.75
                };
                SaveGame(game);
                SaveGame(game2);
                SaveGame(game3);
            }
        }
    }
}
