using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwilightAssistant.Models;
using TwilightAssistant.Services;
using TwilightAssistant.Pages;
using Microsoft.Maui.Storage;

namespace TwilightAssistant.ViewModels
{
    //Pass the Id of the tapped player
    [QueryProperty("PassedPlayerId","PassedPlayerId")]

    public partial class GetRaceViewModel : BaseViewModel
    {
        //Id of the tapped player to assign the race to.
        public string PassedPlayerId { get; set; }

        //Full list of GamePlayers is needed as the JSON file can then be updated when a Race is selected for that player. Wont be used in the UI.
        public ObservableCollection<GamePlayer> GamePlayers = new ObservableCollection<GamePlayer>();

        //List of races to display and interact with in the UI.
        private ObservableCollection<Race> races;
        public ObservableCollection<Race> Races 
        {
            get => races;
            set
            { 
                races = value;
                OnPropertyChanged();
            }
        }

        //Services used to populate properties.
        GamePlayerServices gamePlayerServices;
        RaceServices raceServices;

        public GetRaceViewModel(GamePlayerServices gps, RaceServices races)
        { 
            //Instantiate the Services object
            gamePlayerServices = gps;
            //Call method to get GamePlayers
            GamePlayers = gamePlayerServices.GetOfflineData(Path.Combine(FileSystem.Current.AppDataDirectory, "gameplayers.json"));
            raceServices = races;
            Races = raceServices.GetRaces();
        }

        //Assign Race Command
        private ICommand assignRaceCommand;
        public ICommand AssignRaceCommand => assignRaceCommand ??= new Command(AssignRace);
        public async void AssignRace(object tappedRace)
        {
            //Cast object into Race
            Race race = (Race)tappedRace;

            //Index counter
            int x = 0;

            foreach (GamePlayer player in GamePlayers)
            {
                //Find the player with the matching ID
                if (player.Id == PassedPlayerId)
                {
                    //Update the player with new Race and RaceLogo
                    GamePlayers[x].Race = race.Name;
                    GamePlayers[x].RaceLogo = race.Logo;
                    //Exit the loop
                    continue;
                }
                x++;
            }

            //Update the GamePlayers JSON file in the cashe
            gamePlayerServices.SaveOfflineData(GamePlayers, Path.Combine(FileSystem.Current.AppDataDirectory, "gameplayers.json"));

            //Navigate back
            await Shell.Current.GoToAsync(nameof(SelectRacePage));

        }

    }
}
