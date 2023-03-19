using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;
using TwilightAssistant.Models;

namespace TwilightAssistantTest
{
    public class MainPageTests
    {
        [Fact]
        void Player_Profiles_And_Games_Should_Be_Loaded_From_UpdateMainPage_Method()
        {
            //Arrange
            PlayerProfileServices playerProfileServices = new PlayerProfileServices();
            GameServices gameServices = new GameServices();

            //Act
            MainPageViewModel mpvm = new MainPageViewModel(playerProfileServices, gameServices);
            mpvm.UpdateMainPage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\playerprofiles.json", "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert
            Assert.True(mpvm.PlayerProfiles != null);
            Assert.True(mpvm.Games != null);
        }

        [Fact]
        void Games_List_Should_Only_Contain_Inactive_Games_And_ActiveGames_List_Should_Only_Contain_ActiveGames()
        {
            //Arrange
            PlayerProfileServices playerProfileServices = new PlayerProfileServices();
            GameServices gameServices = new GameServices();

            //Create dummy playerprofiles
            ObservableCollection<PlayerProfile> playerProfiles = new ObservableCollection<PlayerProfile> {
                {new PlayerProfile("TestPlayer1")},
                {new PlayerProfile("TestPlayer2")},
                {new PlayerProfile("TestPlayer3")},
                {new PlayerProfile("TestPlayer4")},
                {new PlayerProfile("TestPlayer5")},
                {new PlayerProfile("TestPlayer6")},
                {new PlayerProfile("TestPlayer7")},
                {new PlayerProfile("TestPlayer8")},
                {new PlayerProfile("TestPlayer9")},
                {new PlayerProfile("TestPlayer10")},
                {new PlayerProfile("TestPlayer11")},
            };

            //Create dummy gameplayers from the dummy playerprofiles
            ObservableCollection<GamePlayer> gamePlayers = new ObservableCollection<GamePlayer> {
                {new GamePlayer(playerProfiles[0].Name,playerProfiles[0].Id)},
                {new GamePlayer(playerProfiles[2].Name,playerProfiles[2].Id)},
                {new GamePlayer(playerProfiles[3].Name,playerProfiles[3].Id)},
                {new GamePlayer(playerProfiles[5].Name,playerProfiles[5].Id)},
                {new GamePlayer(playerProfiles[6].Name,playerProfiles[6].Id)},
                {new GamePlayer(playerProfiles[8].Name,playerProfiles[8].Id)},
                {new GamePlayer(playerProfiles[9].Name,playerProfiles[9].Id)},
                {new GamePlayer(playerProfiles[10].Name,playerProfiles[10].Id)}
            };

            //alter GamePlayer[0]'s id to indicate the method call is from a test. 
            gamePlayers[0].Id = "METHOD CALLED FROM TEST";

            //Create dummy games
            ObservableCollection<Game> games = new ObservableCollection<Game> {
                {new Game(gamePlayers) },
                {new Game(gamePlayers) }
            };
            //Set first game to inactive
            games[0].IsActive = false;

            //Save dummy data to be accessed by the tested method
            gameServices.SaveOfflineData(games, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Act
            MainPageViewModel mpvm = new MainPageViewModel(playerProfileServices, gameServices);
            mpvm.UpdateMainPage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\playerprofiles.json", "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert
            Assert.True(mpvm.Games.Count == 1 && mpvm.Games[0].IsActive == false);
            Assert.True(mpvm.ActiveGames.Count == 1 && mpvm.ActiveGames[0].IsActive == true);

        }

        //[Fact]
        //void SelectingTheActiveGameWillTakeYouToGamePageToContinueTheGame()
        //{ 
            



        //}

    }
}
