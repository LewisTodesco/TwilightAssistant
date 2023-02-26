using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwilightAssistant.Models;
using TwilightAssistant.Services;
using TwilightAssistant.Pages;

namespace TwilightAssistant.ViewModels
{

    [QueryProperty("PlayerProfiles","PlayerProfiles")]

    public class SelectPlayersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PlayerProfile> playerProfiles;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName=null)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public ObservableCollection<PlayerProfile> PlayerProfiles
        {
            get
            {
                return playerProfiles;
            }
            set 
            {
                if (playerProfiles == value)
                    return;
                playerProfiles = value;
                OnPropertyChanged();
            }

        }

        GamePlayerServices gamePlayerServices;
        public SelectPlayersViewModel(GamePlayerServices gps)
        {
            gamePlayerServices = gps;
        }

        //Create an ObservableCollection to store the selected profiles.
        public ObservableCollection<PlayerProfile> SelectedProfiles = new ObservableCollection<PlayerProfile>();
        //Create a Command method to add and remove players from the SelectedProfiles list.
        private ICommand selectedPlayersCommand;
        public ICommand SelectedPlayersCommand => selectedPlayersCommand ??= new Command(SelectedPlayers);
        public void SelectedPlayers(object selectedPlayer)
        {
            //Cast the CommandPerameter object into a PlayerProfile
            PlayerProfile tappedProfile = (PlayerProfile)selectedPlayer;
            //Null check the list, add to it if null
            if (SelectedProfiles.Count == 0)
            {
                SelectedProfiles.Add(tappedProfile);
            }
            else //Add if it doesnt exist, remove if it does.
            {
                if (SelectedProfiles.Contains(tappedProfile))
                {
                    SelectedProfiles.Remove(tappedProfile);
                }
                else 
                {
                    SelectedProfiles.Add(tappedProfile);
                }
            }
        }        


        //Create an ObservableCollection to store GamePlayer objects from the selected profiles.
        public ObservableCollection<GamePlayer> GamePlayers { get; set; }

        private ICommand gotoSelectRaceCommand;
        public ICommand GotoSelectRaceCommand => gotoSelectRaceCommand ??= new Command(GotoSelectRace);

        public async void GotoSelectRace()
        {
            //If no profiles are selected, we dont want the button to do anything.
            if (SelectedProfiles.Count == 0 || SelectedProfiles.Count == 1 || SelectedProfiles.Count == 2)
                //ADD A POP UP HERE TO SELECT AT LEAST 3 PLAYERS
                return;

            GamePlayers = new ObservableCollection<GamePlayer>();

            //Go through each of the SelectedProfiles and create GamePlayers from it. Save this list to the Cashe to be obtainable.
            //We can save the Game to the AppData which will include the list of GamePlayers etc. but we dont want to save a new file every time we try to create a game.
            foreach (PlayerProfile profile in SelectedProfiles)
            { 
                //Create GamePlayer objects based on the Name and Id of the selected PlayerProfiles. The Id is the link back to updating all stats etc.
                GamePlayer gamePlayer = new GamePlayer(profile.Name, profile.Id);
                GamePlayers.Add(gamePlayer);
            }

            //Write the GamePlayers list to cashe
            gamePlayerServices.SaveOfflineData(GamePlayers);

            //Shell navigate to the select race page
            await Shell.Current.GoToAsync(nameof(SelectRacePage));
        }

        
    }
}
