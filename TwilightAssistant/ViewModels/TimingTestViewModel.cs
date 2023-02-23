using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;

namespace TwilightAssistant.ViewModels
{
    public class TimingTestViewModel
    {
        
        public ObservableCollection<GamePlayer> GamePlayers { get; set; }

        public TimingTestViewModel()
        {
            //GamePlayers = GetGamePlayers();
            GamePlayers = new ObservableCollection<GamePlayer>();
            GamePlayer gamePlayer1 = new GamePlayer("Lewis", "Argent");
            GamePlayer gamePlayer2 = new GamePlayer("Joseph", "Pooat");
            GamePlayers.Add(gamePlayer1);
            GamePlayers.Add(gamePlayer2);
        }

    }
}
