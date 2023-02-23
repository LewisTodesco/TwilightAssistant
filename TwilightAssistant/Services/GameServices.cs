using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;

namespace TwilightAssistant.Services
{
    public class GameServices
    {
        public GameServices()
        { 
        }

        public ObservableCollection<Game> GetGames()
        { 
            ObservableCollection<Game> games = new ObservableCollection<Game>();

            try
            {
                string fileName = "games.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                var gamesjson = File.ReadAllText(targetFile);
                games = JsonConvert.DeserializeObject<ObservableCollection<Game>>(gamesjson);
                return games;
            }
            catch (FileNotFoundException)
            {
                string fileName = "games.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                var gamesjson = JsonConvert.SerializeObject(games);
                File.WriteAllText(targetFile, gamesjson);
                return games;
            }

        }

    }
}
