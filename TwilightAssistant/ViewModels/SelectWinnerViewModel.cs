using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwilightAssistant.Models;
using TwilightAssistant.Services;

namespace TwilightAssistant.ViewModels
{
    [QueryProperty("Games","Games")]
    [QueryProperty("Index","Index")]
    [QueryProperty("GamePlayers","GamePlayers")]

    public class SelectWinnerViewModel : BaseViewModel 
    {

        //Can use {Binding Game.GamePlayers} to display the GamePlayers on the UI
        private ObservableCollection<Game> games;
        public ObservableCollection<Game> Games 
        {
            get 
            {
                return games;
            }
            set 
            {
                if (games == value)
                    return;
                games = value;
                OnPropertyChanged();
            }
        }

        //Index of the Active Game
        private int index;
        public int Index 
        {
            get 
            {
                return index;
            }
            set 
            {
                if (index == value)
                    return;
                index = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GamePlayer> gamePlayers;
        public ObservableCollection<GamePlayer> GamePlayers 
        {
            get 
            {
                return gamePlayers;
            }
            set 
            {
                if (gamePlayers == value)
                    return;
                gamePlayers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerProfile> PlayerProfiles { get; set; }
        public SelectWinnerViewModel(PlayerProfileServices pps)
        {
            PlayerProfiles = pps.GetOfflineData();
        }

        //Assign the winner and update all players stats (will need a list of PlayerProfiles).
        //Make a GameStats object from each GamePlayer, then call the UpdateStats method from the PlayerProfiles with matching Id's.
        private ICommand assignWinnerCommand;
        public ICommand AssignWinnerCommand => assignWinnerCommand ??= new Command(AssignWinner);
        public void AssignWinner(object winner)
        {
            //PlayerProfileServices pps = new PlayerProfileServices();
            //PlayerProfiles = pps.GetPlayerProfiles();

            GamePlayer winningPlayer = (GamePlayer)winner;
            string winningID = Games[Index].GamePlayers[Games[Index].GamePlayers.IndexOf(winningPlayer)].Id;

            foreach (GamePlayer player in Games[Index].GamePlayers)
            {
                foreach (PlayerProfile profile in PlayerProfiles)
                {
                    if (profile.Id == winningID && profile.Id == player.Id)
                    {
                        GameStats gameStats = new GameStats(player.RaceLogo, player.Race, Games[Index].GameDate, player.ElapsedTime, true);
                        profile.UpdateStats(gameStats);
                    }
                    else if (profile.Id == player.Id)
                    {
                        GameStats gameStats = new GameStats(player.RaceLogo, player.Race, Games[Index].GameDate, player.ElapsedTime, false);
                        profile.UpdateStats(gameStats);
                    }
                }
            }

            string targetFilePlayerProfiles = Path.Combine(FileSystem.Current.AppDataDirectory, "playerprofiles.json");
            string targetFileGames = Path.Combine(FileSystem.Current.AppDataDirectory, "games.json");

            var playerprofilesjson = JsonConvert.SerializeObject(PlayerProfiles);
            var gamesjson = JsonConvert.SerializeObject(Games);

            File.WriteAllText(targetFilePlayerProfiles, playerprofilesjson);
            File.WriteAllText(targetFileGames, gamesjson);

            Dictionary<string, object> passedwinner = new Dictionary<string, object>();
            passedwinner.Add("Winner", winningPlayer);

            Shell.Current.GoToAsync(nameof(WinnerPage), passedwinner);
            //Navigate to an animation page? XX IS THE WINNER with a spin and a turn etc.
        }

        //For a back button in case it was missclicked.
        private ICommand backCommand;
        public ICommand BackCommand => backCommand ??= new Command(Back);

        public void Back()
        {
            Shell.Current.GoToAsync("..");
        }

    }
}
