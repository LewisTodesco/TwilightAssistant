using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class PlayerProfile //This class is used to store player profile information to keep track of wins and past games.
    {
        public string Name { get; set; } //Profile Name
        public string Id { get; set; } //Profile ID
        public string LastRacePlayed //Used to display the logo of the last race played
        { 
            get 
            {   
                if(GameHistory.Count != 0)
                    return GameHistory[GameHistory.Count - 1].RaceLogo;
                else return null;
            } 
        }
        public int GamesPlayed 
        {
            get //Calculate number of games using the List of game stats.
            {
                return GameHistory.Count;
            }
        }
        public int Wins 
        { 
            get //Calculate number of wins using the List of game stats.
            {
                int wins = 0;
                foreach (GameStats gamestats in GameHistory)
                {
                    if (gamestats.IsWin)
                    {
                        wins++;
                    }
                }
                return wins;
            }
            //set { } - Doesnt need to be settable as the property depends on the List of GameStats.
        }
        public bool Exists { get; set; } //Needed to stop new Ids being created everytime the object is substantiated.

        public ObservableCollection<GameStats> GameHistory { get; set; } //Keep a list of Games played (GameStats object holds simple info)

        public PlayerProfile(string name) //Pass a string name to Name the PlayerProfile, create a new Guid.
        { 
            Name = name;
            GameHistory = new ObservableCollection<GameStats>();
            if (!Exists) //Only create a new Id when its a new object.
            {
                Id = Guid.NewGuid().ToString();
                Exists = true;
            }
        }

        public void UpdateStats(GameStats gameStats) //Add a passed GameStats object to the list.
        { 
            GameHistory.Add(gameStats);
        }

    }
}
