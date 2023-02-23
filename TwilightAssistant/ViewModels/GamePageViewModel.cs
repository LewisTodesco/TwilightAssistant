using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        GameServices gameServices;
        public GamePageViewModel(GameServices gs)
        {
            gameServices = gs;
            ObservableCollection<Game> Games = gameServices.GetGames();
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

        }

    }
}
