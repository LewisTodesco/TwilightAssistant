using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;

namespace TwilightAssistant.Services
{
    public class StrategyCardServices
    {
        public ObservableCollection<StrategyCard> GetStrategyCards()
        {
            ObservableCollection<StrategyCard> strategyCards = new ObservableCollection<StrategyCard>()
            {
                new StrategyCard("Leadership", 1, "leadership.png"),
                new StrategyCard("Deplomacy", 2, "deplomacy.png"),
                new StrategyCard("Politics", 3, "politics.png"),
                new StrategyCard("Construction", 4, "construction.png"),
                new StrategyCard("Trade", 5, "trade.png"),
                new StrategyCard("Warfare", 6, "warfare.png"),
                new StrategyCard("Technology", 7, "technology.png"),
                new StrategyCard("Imperial", 8, "imperial.png")
            };

            return strategyCards;
        }
    }
}
