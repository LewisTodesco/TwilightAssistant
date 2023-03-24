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
using TwilightAssistant.Pages;

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

        public string TargetFile { get; set; }

        PlayerProfileServices playerProfileServices;
        GameServices gameServices;
        public ObservableCollection<PlayerProfile> PlayerProfiles { get; set; }
        public SelectWinnerViewModel(PlayerProfileServices pps, GameServices gs)
        {
            playerProfileServices = pps;

            //Removed from the constuctor as the unit tests couldnt perform the GetOfflineData due to assembly reference errors.
            //In actuality it didnt need to be called during the constructor, the data is only needed when a player is selected in the
            //AssignWinner method.
            PlayerProfiles = new ObservableCollection<PlayerProfile>();
            
            gameServices = gs;
        }

        //Assign the winner and update all players stats (will need a list of PlayerProfiles).
        //Make a GameStats object from each GamePlayer, then call the UpdateStats method from the PlayerProfiles with matching Id's.
        private ICommand assignWinnerCommand;
        public ICommand AssignWinnerCommand => assignWinnerCommand ??= new Command(AssignWinner);
        public async void AssignWinner(object winner)
        {
            //Put in place to stop the method being called when run from tests.
            if (PlayerProfiles.Count == 0)
            {
                PlayerProfiles = playerProfileServices.GetOfflineData(Path.Combine(FileSystem.Current.AppDataDirectory, "playerprofiles.json"));
            }
            
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

            //De-activate the game so it doesnt get overwritten
            Games[Index].IsActive = false;
            //Set the winning player colour so the main screen can show who won
            Games[Index].GamePlayers[Games[Index].GamePlayers.IndexOf(winningPlayer)].WinColour = Colors.Green;

            //Put in place to stop tests overwriting the AppData JSON files and errors with Shell Navigation.
            if (PlayerProfiles[0].Id != "METHOD CALLED FROM TEST")
            {
                playerProfileServices.SaveOfflineData(PlayerProfiles, Path.Combine(FileSystem.Current.AppDataDirectory, "playerprofiles.json"));
                gameServices.SaveOfflineData(Games, Path.Combine(FileSystem.Current.AppDataDirectory, "games.json"));

                Dictionary<string, object> passedwinner = new Dictionary<string, object>
                {
                    { "Winner", winningPlayer }
                };

                await Shell.Current.GoToAsync(nameof(WinnerPage), passedwinner);
            }
            
        }

        //For a back button in case it was missclicked.
        private ICommand backCommand;
        public ICommand BackCommand => backCommand ??= new Command(Back);

        public async void Back()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
