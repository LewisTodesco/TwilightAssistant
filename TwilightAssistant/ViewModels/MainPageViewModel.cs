﻿using Newtonsoft.Json;
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
    //Inherit the INotifyPropertyChanged interface to allow UI to update on ProperyChanged events.
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //ObservableCollection<> is a generic collection, similar to List<>, which automatically provides notification to the UI when items get added, removed or refreshed
        //through trhe INotifyCollectionChanged and INotifyPropertyChanged interfaces. 
        public ObservableCollection<PlayerProfile> PlayerProfiles { get; set; }

        PlayerProfileServices playerProfileServices;
        GameServices gameServices;
        DialogueServices dialogueServices;
        //Constructor to do dependancy injection of the PlayerProfileServices and GameServices
        public MainPageViewModel(PlayerProfileServices pps, GameServices gs, DialogueServices ds)
        {
            //Use the PlayerProfile services to get the playerprofiles from memory
            playerProfileServices = pps;

            //Use the Game services to get the games from memory.
            gameServices = gs;

            //Use the Dialogue services for popups
            dialogueServices = ds;   

        }

        //Impliment the PropertyChanged event from INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        //Create the OnPropertyChanged method to raise/call/publish the event. Use the [CallerMemberName] Attribute to automatically pass in the string of the caller.
        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            //?.Invoke performs the null check on the PropertyChanged event. if there are no subscribers, the event wont be published. The sender is this class, and the event args is the CallerMemberName.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        //Create a private field for the getter/setter
        private string nameInput;
        //Create the NameInput property and impliment the getter and setter logic. When set, use the OnPropertyChanged method to raise the event.
        public string NameInput
        {
            get
            {
                return nameInput;
            }

            set
            {
                if (value == nameInput)
                    //No need to set anything new if the input value is the same as the current value.
                    return;
                nameInput = value;
                OnPropertyChanged();
            }
        }

        //The Command class inherits the ICommand interface. Allocate a private field for the command.
        private ICommand addPlayerCommand;
        //Create an ICommand property that gets the addPlayerCommand field. If the addPlayerCommand property is null (??=) create a new Command with the Action AddPlayer()
        public ICommand AddPlayerCommand => addPlayerCommand ??= new Command(AddPlayer);
        //Method to run when the AddPlayerCommand button is pressed. Use the Name from the input box to create a new PlayerProfile object and add it to the observable collection.
        public async void AddPlayer()
        {
            PlayerProfile newProfile = new PlayerProfile(NameInput);
            PlayerProfiles.Add(newProfile);
            //Put the name input box back to default view
            NameInput = "";

            //Update the AppDataDirectory json file to persist the PlayerProfiles.
            playerProfileServices.SaveOfflineData(PlayerProfiles, Path.Combine(FileSystem.Current.AppDataDirectory, "playerprofiles.json"));
            
            //Process below works and will update the Db using the TwilightAssistantAPI. However, due to UI issues, sticking with Offline approach for now.
            /*
            Task<ObservableCollection<PlayerProfile>> savetoDB = playerProfileServices.SaveOnlineData(newProfile);
            PlayerProfiles = await savetoDB;
            */
        }

        //Create a new Command for when a player profile from the CollectionView is tapped.
        private ICommand gotoPlayerProfileCommand;
        public ICommand GotoPlayerProfileCommand => gotoPlayerProfileCommand ??= new Command(GotoPlayerProfile);
        //This method will navigate the user to the PlayerProfilePage and pass a Dictionary with the tapped item from the collectionview.
        //The PlayerProfileViewModel will have a QueryPropery associated with the Key "Profile", and make a new PlayerProfile based on the
        //passedProfile.
        public async void GotoPlayerProfile(object playerProfile)
        {
            //Cast the tapped object into type PlayerProfile
            PlayerProfile tappedProfile = (PlayerProfile)playerProfile;
            //Create the dictionary to use in the GoToAsync method
            IDictionary<string, object> passedProfile = new Dictionary<string, object>();
            passedProfile.Add("Profile", tappedProfile);
            //Use URI shell navigationt to go to the page with the nameof(PlayerProfilePage), and pass the dictionary too.
            await Shell.Current.GoToAsync(nameof(PlayerProfilePage), true, passedProfile);
        }

        private ICommand gotoCreateGameCommand;

        public ICommand GotoCreateGameCommand => gotoCreateGameCommand ??= new Command(GotoCreateGame);

        public async void GotoCreateGame()
        {
            if (activeGames.Count != 0)
            {
                var answer = await dialogueServices.DisplayYesNo("Overwrite Game?", "Warning: When you finish creating a new game, the active game will be overwritten. Do you want to continue?", "Yes", "No");
                if (!answer)
                {
                    return;
                }
            }
             
            IDictionary<string, object> passedProfiles = new Dictionary<string, object>();
            passedProfiles.Add("PlayerProfiles", PlayerProfiles);
            await Shell.Current.GoToAsync(nameof(SelectPlayersPage), true, passedProfiles);
        }

        //Create a Games collection to display game history.
        private ObservableCollection<Game> games;
        public ObservableCollection<Game> Games
        {
            get => games;
            set
            {
                games = value;
                OnPropertyChanged();
            }
        }

        //Create a ActiveGames collection, for now it will always be one, but gives the option to have more in the future.
        private ObservableCollection<Game> activeGames;
        public ObservableCollection<Game> ActiveGames
        {
            get => activeGames;
            set
            {
                activeGames = value;
                OnPropertyChanged();
            }
        }

        //Load all data when the page appears.
        public async void UpdateMainPage(string targetFileProfiles, string targetFileGames)
        {
            PlayerProfiles = playerProfileServices.GetOfflineData(targetFileProfiles);
            ObservableCollection<Game> allGames = gameServices.GetOfflineData(targetFileGames);

            ActiveGames = new ObservableCollection<Game>();
            Games = new ObservableCollection<Game>();
            //Distinguish between active and finished games incase the app crashes, closes etc., or game is played over two sessions, and you want to continue.
            for (int i = allGames.Count - 1; i>=0; i--)
            {
                Game game = allGames[i];
                if (game.IsActive)
                    ActiveGames.Add(game);
                else
                    Games.Add(game);
            }

            //Process below works but the UI will not update with the list of player profiles without navigating away and back to main page.
            //Commented out until fixed, therefore will be utilising Offline services only.
            /*
            Task<ObservableCollection<PlayerProfile>> databaseProfiles = playerProfileServices.GetOnlineData();
            PlayerProfiles = await databaseProfiles;
            */
        }

        //Goto ActiveGame
        private ICommand gotoActiveGameCommand;

        public ICommand GotoActiveGameCommand => gotoActiveGameCommand ??= new Command(GotoActiveGame);

        public async void GotoActiveGame(object tappedGame)
        {

            Game activeGame = (Game)tappedGame;
            int playercount = activeGame.GamePlayers.Count;
            switch (playercount)
            {
                case 3:
                    await Shell.Current.GoToAsync(nameof(GamePage3));
                    break;
                case 4:
                    //await Shell.Current.GoToAsync(nameof(GamePage4));
                    break;
                case 5:
                    //await Shell.Current.GoToAsync(nameof(GamePage5));
                    break;
                case 6:
                    await Shell.Current.GoToAsync(nameof(GamePage6));
                    break;
                case 7:
                    //await Shell.Current.GoToAsync(nameof(GamePage7));
                    break;
                case 8:
                    await Shell.Current.GoToAsync(nameof(GamePage));
                    break;
            }

        }

    }
}
