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
    public class GameServices : IGetSaveServices<Game>
    {
        public GameServices()
        { 
        }

        public ObservableCollection<Game> GetOfflineData(string targetFile)
        {
            ObservableCollection<Game> games = new ObservableCollection<Game>();

            try
            {
                /*
                string fileName = "games.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                */
                var gamesjson = File.ReadAllText(targetFile);
                games = JsonConvert.DeserializeObject<ObservableCollection<Game>>(gamesjson);
                return games;
            }
            catch (FileNotFoundException)
            {
                /*
                string fileName = "games.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                */
                var gamesjson = JsonConvert.SerializeObject(games);
                File.WriteAllText(targetFile, gamesjson);
                return games;
            }

        }

        public ObservableCollection<Game> GetOnlineData()
        {
            throw new NotImplementedException();
        }

        public void SaveOfflineData(ObservableCollection<Game> collection_to_save)
        {
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, "games.json");
            var gamesjson = JsonConvert.SerializeObject(collection_to_save);
            File.WriteAllText(targetFile, gamesjson);
        }

        public ObservableCollection<Game> SaveOnlineData()
        {
            throw new NotImplementedException();
        }
    }
}
