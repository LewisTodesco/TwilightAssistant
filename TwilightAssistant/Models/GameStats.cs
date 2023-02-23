using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class GameStats
    {
        public string RaceLogo { get; set; } //Store race logo filename
        public string RacePlayed { get; set; } //Track what race was played
        public string GameDate { get; set; } //Track game date
        public string PlayTime { get; set; } //Track how long player played
        public bool IsWin { get; set; } //Track if win
        public Color WinLossColour //Change the colour of the frame border depending on Win or Loss
        {
            get 
            {
                if (IsWin)
                    return Colors.Green;
                else
                    return Colors.Red;
            }
        }

        //Create object and add to list in PlayerProfile on game end.
        public GameStats(string raceLogo,string racePlayed,string gameDate,string playTime,bool isWin) 
        { 
            RaceLogo = raceLogo;
            RacePlayed = racePlayed;
            GameDate = gameDate;
            PlayTime = playTime;
            IsWin = isWin;
        }

    }
}
