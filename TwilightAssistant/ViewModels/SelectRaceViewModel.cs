using Newtonsoft.Json;
using System;
using System.Collections;
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
    //Make the class partial to allow the source generators from the MVVM Community Toolkit to work.
    public class SelectRaceViewModel : INotifyPropertyChanged //MVVM Community toolkit class, this allows us to use atributes to generate the boilerplate code for INotifyProperyChanged and ICommands. 
    {
        //[ObservablePropery] attribute impliments the INotifyPropertyChanged interface for the attributed property. Making code MUCH cleaner/quicker.

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<GamePlayer> gamePlayers; //Auto generates a public ObservableCollection<GamePlayer> GamePlayers
        public ObservableCollection<GamePlayer> GamePlayers
        {
            get => gamePlayers;
            set 
            { 
                gamePlayers = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }


        //Create a GamePlayerServices to use for dependancy injection
        GamePlayerServices gamePlayerServices;
        GameServices gameServices;
        public SelectRaceViewModel(GamePlayerServices gps, GameServices gs)
        {
            //Dependancy injection
            gamePlayerServices = gps;
            //Get the GamePlayers list from the GamePlayerServices GetGamePlayers method.
            GamePlayers = gamePlayerServices.GetOfflineData();
            gameServices = gs;
        }

        //Create a method to be called OnAppearing to update to UI with the newly selected Races.
        public void UpdateGamePlayers()
        {
            GamePlayers = gamePlayerServices.GetOfflineData();
        }

        //Pass the Id of the tapped player and naviate to the GetRace page.
        private ICommand gotoGetRaceCommand;
        public ICommand GotoGetRaceCommand => gotoGetRaceCommand ??= new Command(GotoGetRacePage);
        public async void GotoGetRacePage(object tappedPlayer)
        {
            GamePlayer passedPlayer = (GamePlayer)tappedPlayer;
            IDictionary<string,object> passedGamePlayer = new Dictionary<string,object>();
            passedGamePlayer.Add("PassedPlayerId", passedPlayer.Id);
            await Shell.Current.GoToAsync(nameof(GetRacePage),passedGamePlayer); 
        }


        public ObservableCollection<Game> Games { get; set; }
        //Button command to goto the game page when all races have been selected.
        public Game ActiveGame { get; set; }
        private ICommand gotoGamePageCommand;
        public ICommand GotoGamePageCommand => gotoGamePageCommand ??= new Command(GotoGamePage);
        public async void GotoGamePage()
        {
            //Check if all players have a race assigned.
            foreach (GamePlayer player in GamePlayers)
            {
                if (player.RaceLogo == "selectrace.png")
                {
                    //Add a pop up saying not all races selected?
                    return;
                }
            }
            //Check if duplicate races have been selected.
            for (int i = 0; i < GamePlayers.Count; i++)
            {
                for (int x = 0; x < GamePlayers.Count; x++)
                {
                    if (i != x)
                    {
                        if (GamePlayers[i].RaceLogo == GamePlayers[x].RaceLogo)
                        {
                            //Add a pop up saying duplicate races selected.
                            return;
                        }
                    }
                }
            }

            //Make a Game object. If an existing object is still active (game hasnt finished), overwrite. Save this game object to the AppData directory. Set this as the current game (ActiveGame property?)
            //Can show the active game in the main screen incase the app closes? And if the Create game button is pressed, prompt to say are you sure as it will
            //overwrite the existing game.
            Games = gameServices.GetOfflineData();
            //Remove the currently active game. Games will be made innactive on completion.
            foreach (Game game in Games)
            {
                if (game.IsActive)
                {
                    ActiveGame = game;
                }
            }
            if(ActiveGame!=null)
                Games.Remove(ActiveGame);

            //Create a new game and Add it to the list of Games. Write the list of Games to the AppData and pass the Game object to the GamePageViewModel.
            Game newGame = new Game(GamePlayers);
            Games.Add(newGame);
            gameServices.SaveOfflineData(Games);

            //Tried passing the newly created Game object, but it wasnt working great.
            //IDictionary<string, object> passedGame = new Dictionary<string, object>();
            //passedGame.Add("NewGame",newGame);

            int playercount = GamePlayers.Count;
            switch (playercount)
            {
                case 3:
                    await Shell.Current.GoToAsync(nameof(GamePage3));
                    break;
                case 4:
                    //await Shell.Current.GoToAsync(nameof(GamePage4), passedGame);
                    break;
                case 5:
                    //await Shell.Current.GoToAsync(nameof(GamePage5), passedGame);
                    break;
                case 6:
                    //await Shell.Current.GoToAsync(nameof(GamePage6), passedGame);
                    break;
                case 7:
                    //await Shell.Current.GoToAsync(nameof(GamePage7), passedGame);
                    break;
                case 8:
                    await Shell.Current.GoToAsync(nameof(GamePage));
                    break;
            }
            
        }

    }
}
