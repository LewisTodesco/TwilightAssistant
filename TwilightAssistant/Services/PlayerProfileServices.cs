using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;
using Newtonsoft.Json;
using Microsoft.Maui.ApplicationModel;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace TwilightAssistant.Services
{
    public class PlayerProfileServices : IGetSaveServices<PlayerProfile>
    {

        public List<PlayerProfileDb> DatabaseProfiles { get; private set; }
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        //Default constructor
        public PlayerProfileServices()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy= JsonNamingPolicy.CamelCase,
                WriteIndented= true,
            };
        }

        public ObservableCollection<PlayerProfile> GetOfflineData(string targetFile)
        {
            ObservableCollection<PlayerProfile> playerProfiles = new ObservableCollection<PlayerProfile>();

            try
            {
                //IsBusy = true;
                /*
                //Get the file location from the AppDataDirectory
                string fileName = "playerprofiles.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                */

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
                /*
                string fileName = "playerprofiles.json";
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
                */
                var playerprofilesjson = JsonConvert.SerializeObject(playerProfiles);
                File.WriteAllText(targetFile, playerprofilesjson);
                return playerProfiles;
            }
            finally
            {
                //IsBusy = false;
            }

        }

        public async Task<ObservableCollection<PlayerProfile>> GetOnlineData()
        {
            DatabaseProfiles = new List<PlayerProfileDb>();
            ObservableCollection<PlayerProfile> ConvertedDatabaseProfiles = new ObservableCollection<PlayerProfile>();

            Uri uri = new Uri("https://localhost:7112/api/PlayerProfile");

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    DatabaseProfiles = System.Text.Json.JsonSerializer.Deserialize<List<PlayerProfileDb>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            
            foreach (var profile in DatabaseProfiles)
            {
                PlayerProfile appProfile = new PlayerProfile(profile.Name);
                appProfile.Id = profile.GuidId;
                ConvertedDatabaseProfiles.Add(appProfile);
            }
            
            return ConvertedDatabaseProfiles;
        }

        public void SaveOfflineData(ObservableCollection<PlayerProfile> collection_to_save, string targetFile)
        {
            var playerProfilesJson = JsonConvert.SerializeObject(collection_to_save);
            File.WriteAllText(targetFile, playerProfilesJson);
        }

        public async Task<ObservableCollection<PlayerProfile>> SaveOnlineData(object appProfile)
        {

            //Create a new lost of profiles
            DatabaseProfiles = new List<PlayerProfileDb>();
            ObservableCollection<PlayerProfile> ConvertedDatabaseProfiles = new ObservableCollection<PlayerProfile>();

            //ASP.NET Web API
            Uri uri = new Uri("https://localhost:7112/api/PlayerProfile");

            //Convert passed profile into a playerprofile object
            PlayerProfile passedProfile = (PlayerProfile)appProfile;

            //Create a new dbProfile object using the PP
            PlayerProfileDb dbProfile = new PlayerProfileDb()
            {
                Name = passedProfile.Name,
                GuidId = passedProfile.Id
            };

            //Try HttpPost
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize<PlayerProfileDb>(dbProfile, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                   
                response = await _client.PostAsync(uri, content);
                
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    DatabaseProfiles = System.Text.Json.JsonSerializer.Deserialize<List<PlayerProfileDb>>(responseContent, _serializerOptions);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            //Convert response body into PlayerProfiles to display
            foreach (var profile in DatabaseProfiles)
            {
                PlayerProfile appConvertedProfile = new PlayerProfile(profile.Name);
                appConvertedProfile.Id = profile.GuidId;
                ConvertedDatabaseProfiles.Add(appConvertedProfile);
            }

            //Return converted profiles
            return ConvertedDatabaseProfiles;
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
