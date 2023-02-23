using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public class Race
    {
        //Race Name
        public string Name { get; set; }
        //Race Logo
        public string Logo { get; set; }
        //Race constructor
        public Race(string name, string logo)
        { 
            Name = name;
            Logo = logo;
        }
    }
}
