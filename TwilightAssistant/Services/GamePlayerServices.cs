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
    //Made the class static so the GetGamePlayers method can be called from any class
    public class GamePlayerServices : IGetSaveServices<GamePlayer>
    {

        public ObservableCollection<GamePlayer> GetOfflineData()
        {
            //Instatiate an ObservableCollection
            ObservableCollection<GamePlayer> gamePlayers = new ObservableCollection<GamePlayer>();

            try
            {
                //IsBusy = true;

                //Get the file location from the AppDataDirectory
                string fileName = "gameplayers.json";
                string targetFile = Path.Combine(FileSystem.Current.CacheDirectory, fileName);
                //Generate read the json file
                var gameplayersjson = File.ReadAllText(targetFile);
                //Deserialise the json file and allocate it to the PlayerProfiles observable collection
                gamePlayers = JsonConvert.DeserializeObject<ObservableCollection<GamePlayer>>(gameplayersjson);
                //return it
                return gamePlayers;
            }
            //If no file exists (First time opening), catch the exeption and make a null file.
            catch (FileNotFoundException)
            {
                string fileName = "gameplayers.json";
                string targetFile = Path.Combine(FileSystem.Current.CacheDirectory, fileName);
                File.WriteAllText(targetFile, "");
                return gamePlayers = null;
            }
            finally
            {
                //IsBusy = false;
            }
        }

        public ObservableCollection<GamePlayer> GetOnlineData()
        {
            throw new NotImplementedException();
        }

        public void SaveOfflineData(ObservableCollection<GamePlayer> collection_to_save)
        {
            string fileName = "gameplayers.json";
            string targetFile = Path.Combine(FileSystem.Current.CacheDirectory, fileName);
            var gameplayersjson = JsonConvert.SerializeObject(collection_to_save);
            File.WriteAllText(targetFile, gameplayersjson);
        }

        public ObservableCollection<GamePlayer> SaveOnlineData()
        {
            throw new NotImplementedException();
        }
    }
}
