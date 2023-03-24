using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class GamePlayer
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string RaceLogo { get; set; }
        public string ElapsedTime { get; set; }
        public string Id { get; set; }
        public Color WinColour { get; set; }
        public StrategyCard StrategyCard { get; set; }
        public GamePlayer(string name, string id)
        {
            Name = name;
            Id = id;
            ElapsedTime = "00:00:00";
            RaceLogo = "selectrace.png";
            StrategyCard = new("Unknown", 9, "unknown.png");
            WinColour = Colors.White;
        }   

        //At the end of a game, create a GameStats object from the Collection of GamePlayers and then call the UpdateStats method, passing in the newly created objects.
    }
}
