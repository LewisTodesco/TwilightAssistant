using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwilightAssistant.Models;
using TwilightAssistant.Services;

namespace TwilightAssistant.ViewModels
{
    //[QueryProperty("NewGame","NewGame")]

    public class GamePageViewModel : BaseViewModel
    {
        
        //Use the active game to dictate the displayed Game info.
        private Game activeGame;
        public Game ActiveGame
        {
            get => activeGame;
            set
            {
                activeGame = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GamePlayer> gamePlayers;
        public ObservableCollection<GamePlayer> GamePlayers 
        {
            get => gamePlayers;
            set
            { 
                gamePlayers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Game> Games { get; set; }

        GameServices gameServices;
        public GamePageViewModel(GameServices gs)
        {
            gameServices = gs;
            ObservableCollection<Game> Games = gameServices.GetOfflineData(Path.Combine(FileSystem.Current.AppDataDirectory, "games.json"));
            //Set NewGame as the Active game
            foreach (Game game in Games)
            {
                if (game.IsActive)
                { 
                    ActiveGame = game;
                }
            }

            //Use the Game.GamePlayers incase a game is loaded from memory.
            GamePlayers = ActiveGame.GamePlayers;

            this.Games = Games;

        }

        ////Button to go home
        //private ICommand homeCommand;
        //public ICommand HomeCommand => homeCommand ??= new Command(Home);
        //public void Home()
        //{
        //    var currentStack = Shell.Current.Navigation.NavigationStack;
        //    MainThread.BeginInvokeOnMainThread(() =>
        //        {
        //            Shell.Current.Navigation.PopToRootAsync(true);
        //        });
            
        //}

    }
}
