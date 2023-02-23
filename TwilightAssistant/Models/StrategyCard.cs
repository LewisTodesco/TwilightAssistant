using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class StrategyCard
    {
        public string StrategyName { get; set; }
        public int Initiative { get; set; }
        public string StrategyLogo { get; set; }

        public StrategyCard(string name, int initiative, string strategylogo)
        {
            StrategyName = name;
            Initiative = initiative;
            StrategyLogo = strategylogo;
        }
        
    }
}
