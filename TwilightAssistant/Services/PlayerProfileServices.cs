using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;
using Newtonsoft.Json;

namespace TwilightAssistant.Services
{
    public class PlayerProfileServices : IGetSaveServices<PlayerProfile>
    {

        //Default constructor
        public PlayerProfileServices()
        { 
        
        }

        public ObservableCollection<PlayerProfile> GetOfflineData()
        {
            ObservableCollection<PlayerProfile> playerProfiles = new ObservableCollection<PlayerProfile>();

            try
            {
                //IsBusy = true;

                //Get the file location from the AppDataDirectory
                string fileName = "playerprofiles.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                //Generate read the json file
                var playerprofilesjson = File.ReadAllText(targetFile);
                //Deserialise the json file and allocate it to the PlayerProfiles observable collection
                playerProfiles = JsonConvert.DeserializeObject<ObservableCollection<PlayerProfile>>(playerprofilesjson);
                //return it
                return playerProfiles;
            }
            //If no file exists (First time opening), catch the exeption and make a null file.
            catch (FileNotFoundException)
            {
                string fileName = "playerprofiles.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                var playerprofilesjson = JsonConvert.SerializeObject(playerProfiles);
                File.WriteAllText(targetFile, playerprofilesjson);
                return playerProfiles;
            }
            finally
            {
                //IsBusy = false;
            }

        }

        public ObservableCollection<PlayerProfile> GetOnlineData()
        {
            throw new NotImplementedException();
        }

        public void SaveOfflineData(ObservableCollection<PlayerProfile> collection_to_save)
        {
            var playerProfilesJson = JsonConvert.SerializeObject(collection_to_save);
            string fileName = "playerprofiles.json";
            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            File.WriteAllText(targetFile, playerProfilesJson);
        }

        public ObservableCollection<PlayerProfile> SaveOnlineData()
        {
            throw new NotImplementedException();
        }


        //Old method not as good.
        //Public ObservableCollection
        //public ObservableCollection<PlayerProfile> PlayerProfiles { get; set; }
        /*
        //public ObservableCollection<PlayerProfile> GetPlayerProfiles()
        //{
        //    try
        //    {
        //        //IsBusy = true;

        //        //Get the file location from the AppDataDirectory
        //        string fileName = "playerprofiles.json";
        //        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        //        //Generate read the json file
        //        var playerprofilesjson = File.ReadAllText(targetFile);
        //        //Deserialise the json file and allocate it to the PlayerProfiles observable collection
        //        PlayerProfiles = JsonConvert.DeserializeObject<ObservableCollection<PlayerProfile>>(playerprofilesjson);
        //        //return it
        //        return PlayerProfiles;
        //    }
        //    //If no file exists (First time opening), catch the exeption and make a null file.
        //    catch (FileNotFoundException)
        //    {
        //        string fileName = "playerprofiles.json";
        //        string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
        //        File.WriteAllText(targetFile, "");
        //        return PlayerProfiles = null;
        //    }
        //    finally
        //    { 
        //        //IsBusy = false;
        //    }

        //}
        */

    }
}
