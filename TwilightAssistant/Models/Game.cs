using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class Game
    {
        public ObservableCollection<GamePlayer> GamePlayers { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string GameDate { get; set; }
        public bool Exists { get; set; }

        //Store a tick array in the game object. Can be used to store the last known version of the tick array to load the game from memory and retain timings.
        public int[,] TickArray = new int[8, 3] {
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0}
        };

        public Game(ObservableCollection<GamePlayer> gameplayers)
        {
            GamePlayers = gameplayers;

            if (!Exists)
            { 
                Guid guid = Guid.NewGuid();
                Id = guid.ToString();
                Exists = true;
            }

            GameDate = DateTime.Now.ToString();
            IsActive = true;
        }

    }
}
