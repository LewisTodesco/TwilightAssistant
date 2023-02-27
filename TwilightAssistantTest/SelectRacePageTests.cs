using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;
using TwilightAssistant.Models;
using Moq;

namespace TwilightAssistantTest
{
    public class SelectRacePageTests
    {
        [Fact]
        void Creating_A_Game_Should_Overwrite_Currently_Active_Game_But_Ignore_Inactive_Games()
        {
            //Arrange
            GamePlayerServices gamePlayerServices = new GamePlayerServices();
            GameServices gameServices = new GameServices();
            DialogueServices dialogueServices = new DialogueServices();
            SelectRaceViewModel selectRaceViewModel = new SelectRaceViewModel(gamePlayerServices, gameServices, dialogueServices);

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
            // Create a mock MainPage object that returns a dummy Page instance
            //var mockMainPage = new Mock<Page>();
            //Application.Current.MainPage = mockMainPage.Object;

            gamePlayers[0].RaceLogo = "Unique1";
            gamePlayers[1].RaceLogo = "Unique2";
            gamePlayers[2].RaceLogo = "Unique3";
            gamePlayers[3].RaceLogo = "Unique4";
            gamePlayers[4].RaceLogo = "Unique5";
            gamePlayers[5].RaceLogo = "Unique6";
            gamePlayers[6].RaceLogo = "Unique7";
            gamePlayers[7].RaceLogo = "Unique8";



            //Create dummy games
            ObservableCollection<Game> games = new ObservableCollection<Game> {
                {new Game(gamePlayers) },
                {new Game(gamePlayers) }
            };
            //Set first game to inactive
            games[0].IsActive = false;    

            //Save dummy data to be accessed by the tested method
            gamePlayerServices.SaveOfflineData(gamePlayers, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\gameplayers.json");
            gameServices.SaveOfflineData(games, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Use dummy list
            selectRaceViewModel.GamePlayers = gamePlayers;

            //Declare expected games after method call.
            int expectedGames = 2;
            string oldGameID = games[1].Id;

            //Act
            selectRaceViewModel.GotoGamePage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert - Test failed due to no race selection returning the method.
            Assert.True(selectRaceViewModel.Games.Count == expectedGames);
            Assert.NotEqual(selectRaceViewModel.Games[1].Id, oldGameID);

        }
        [Fact]
        void Game_Will_Not_Be_Created_If_A_Player_Hasnt_Selected_A_Race()
        {
            //Arrange
            GamePlayerServices gamePlayerServices = new GamePlayerServices();
            GameServices gameServices = new GameServices();
            DialogueServices dialogueServices = new DialogueServices();
            SelectRaceViewModel selectRaceViewModel = new SelectRaceViewModel(gamePlayerServices, gameServices, dialogueServices);

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
            gamePlayerServices.SaveOfflineData(gamePlayers, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\gameplayers.json");
            gameServices.SaveOfflineData(games, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Use dummy list
            selectRaceViewModel.GamePlayers = gamePlayers;

            //Act
            selectRaceViewModel.GotoGamePage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert
            Assert.True(selectRaceViewModel.Games == null);

        }
        [Fact]
        void Game_Will_Not_Be_Created_If_Two_Players_Have_Selected_The_Same_Race()
        {
            //Arrange
            GamePlayerServices gamePlayerServices = new GamePlayerServices();
            GameServices gameServices = new GameServices();
            DialogueServices dialogueServices = new DialogueServices();
            SelectRaceViewModel selectRaceViewModel = new SelectRaceViewModel(gamePlayerServices, gameServices, dialogueServices);

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

            //Assign duplicate races
            gamePlayers[0].RaceLogo = "Unique1";
            gamePlayers[1].RaceLogo = "Unique2";
            gamePlayers[2].RaceLogo = "Unique3";
            gamePlayers[3].RaceLogo = "Unique4";
            gamePlayers[4].RaceLogo = "Duplicate";
            gamePlayers[5].RaceLogo = "Unique5";
            gamePlayers[6].RaceLogo = "Unique6";
            gamePlayers[7].RaceLogo = "Duplicate";

            //Create dummy games
            ObservableCollection<Game> games = new ObservableCollection<Game> {
                {new Game(gamePlayers) },
                {new Game(gamePlayers) }
            };

            //Set first game to inactive
            games[0].IsActive = false;

            //Save dummy data to be accessed by the tested method
            gamePlayerServices.SaveOfflineData(gamePlayers, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\gameplayers.json");
            gameServices.SaveOfflineData(games, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Use dummy list
            selectRaceViewModel.GamePlayers = gamePlayers;

            //Act
            selectRaceViewModel.GotoGamePage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert
            Assert.True(selectRaceViewModel.Games == null);
            
            
        }
        [Fact]
        void Creating_A_Game_Should_Add_A_New_ActiveGame_To_The_List_And_Resave_The_Json_File()
        {
            //Arrange
            GamePlayerServices gamePlayerServices = new GamePlayerServices();
            GameServices gameServices = new GameServices();
            DialogueServices dialogueServices = new DialogueServices();
            SelectRaceViewModel selectRaceViewModel = new SelectRaceViewModel(gamePlayerServices, gameServices, dialogueServices);

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
            // Create a mock MainPage object that returns a dummy Page instance
            //var mockMainPage = new Mock<Page>();
            //Application.Current.MainPage = mockMainPage.Object;

            gamePlayers[0].RaceLogo = "Unique1";
            gamePlayers[1].RaceLogo = "Unique2";
            gamePlayers[2].RaceLogo = "Unique3";
            gamePlayers[3].RaceLogo = "Unique4";
            gamePlayers[4].RaceLogo = "Unique5";
            gamePlayers[5].RaceLogo = "Unique6";
            gamePlayers[6].RaceLogo = "Unique7";
            gamePlayers[7].RaceLogo = "Unique8";



            //Create dummy games
            ObservableCollection<Game> games = new ObservableCollection<Game> {
                {new Game(gamePlayers) },
                {new Game(gamePlayers) }
            };
            //Set first game to inactive
            games[0].IsActive = false;

            //Save dummy data to be accessed by the tested method
            gamePlayerServices.SaveOfflineData(gamePlayers, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\gameplayers.json");
            gameServices.SaveOfflineData(games, "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Use dummy list
            selectRaceViewModel.GamePlayers = gamePlayers;


            //Act
            selectRaceViewModel.GotoGamePage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");
            ObservableCollection<Game> newGames = gameServices.GetOfflineData("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");


            //Assert - Test failed due to no race selection returning the method.
            Assert.Equal(selectRaceViewModel.Games[1].Id, newGames[1].Id);

        }

    }
}
