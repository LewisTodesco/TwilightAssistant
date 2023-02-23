using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;

namespace TwilightAssistant.Services
{
    public class RaceServices
    {
        public RaceServices()
        { 
        }
        //Create a method that can be called to load the list of Races. Makes the code cleaner and abstracts it from the other classes that may need to use it.
        //If multiple classes need this list and it needs to be updated in the future, only this one method needs to be changed.
        public ObservableCollection<Race> GetRaces()
        {
            ObservableCollection<Race> races = new ObservableCollection<Race>() 
            { 
                new Race("Arborec", "arborec.png"),
                new Race("The Barony of Letnev", "barony.png"),
                new Race("The Clan of Saar", "saar.png"),
                new Race("The Embers of Muaat", "muaat.png"),
                new Race("The Emirates of Hacan", "hacan.png"),
                new Race("The Federation of Sol", "sol.png"),
                new Race("The Ghosts of Creuss", "ghosts.png"),
                new Race("The L1Z1X Mindnet", "l1z1x.png"),
                new Race("The Mentak Coalition", "mentak.png"),
                new Race("The Naalu Collective", "naalu.png"),
                new Race("The Nekro Virus", "nekro.png"),
                new Race("Sardakk N'orr", "sardakk.png"),
                new Race("The Universities of Jol_Nar", "jolnar.png"),
                new Race("The Winnu", "winnu.png"),
                new Race("The Xxcha Kingdom", "xxcha.png"),
                new Race("The Yin Brotherhood", "yin.png"),
                new Race("The Yssarill Tribes", "yssaril.png"),
                new Race("The Argent Flight", "argentfactionsymbol.png"),
                new Race("The Empyrean", "empyreanfactionsymbol.png"),
                new Race("The Mahact Gene-Sorcerers", "mahactfactionsymbol.png"),
                new Race("The Naaz-Rokha Alliance", "naazrokhafactionsymbol.png"),
                new Race("The Nomad", "nomadfactionsheet.png"),
                new Race("The Titans of Ul", "ulfactionsymbol.png"),
                new Race("The Vuil'Raith Cabal", "cabalfactionsymbol.png"),
                new Race("The Council Keleres", "council.png")
            };
            return races;
        }

    }
}
