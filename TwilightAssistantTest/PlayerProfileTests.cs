using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;

namespace TwilightAssistantTest
{
    public class PlayerProfileTests
    {
        [Fact]
        void Update_Stats_Method_Should_Increase_The_Games_Played_By_1()
        {
            //Arrange
            PlayerProfile playerProfile = new PlayerProfile("Test");
            int currentGamesPlayed = playerProfile.GamesPlayed;
            int expectedGamesPlayed = currentGamesPlayed + 1;

            //Act
            playerProfile.UpdateStats(new GameStats("testLogo","testRace","testDate","testPlayTime",false));

            //Assert
            Assert.True(playerProfile.GamesPlayed == expectedGamesPlayed);
        }
        [Fact]
        void Update_Stats_Method_With_IsWin_As_True_Should_Increase_Wins_By_1()
        {
            //Arrange
            PlayerProfile playerProfile = new PlayerProfile("Test");
            int currentWins = playerProfile.Wins;
            int expectedWins = currentWins + 1;

            //Act
            playerProfile.UpdateStats(new GameStats("testLogo", "testRace", "testDate", "testPlayTime", true));

            //Assert
            Assert.True(playerProfile.Wins == expectedWins);
        }
        [Fact]
        void Update_Stats_Method_With_IsWin_As_False_Should_Not_Increase_Wins_By_1()
        {
            //Arrange
            PlayerProfile playerProfile = new PlayerProfile("Test");
            int currentWins = playerProfile.Wins;
            int expectedWins = currentWins;

            //Act
            playerProfile.UpdateStats(new GameStats("testLogo", "testRace", "testDate", "testPlayTime", false));

            //Assert
            Assert.True(playerProfile.Wins == expectedWins);
        }
        [Fact]
        void Making_Two_Player_Profile_Objects_With_The_Same_Name_Will_Create_Unique_Ids()
        { 
            PlayerProfile playerProfile1 = new PlayerProfile("Test");
            PlayerProfile playerProfile2 = new PlayerProfile("Test");

            Assert.NotEqual(playerProfile1.Id, playerProfile2.Id);
        }
        [Fact]
        void Property_LastRacePlayed_Should_Be_Equal_To_The_RaceLogo_Property_Of_The_Most_Recent_GameStats_Object_In_GameHistory_Collection()
        {
            //Arrange
            PlayerProfile playerProfile = new PlayerProfile("Test");
            string expectedLogo = "CorrectRaceLogo";

            //Act
            playerProfile.UpdateStats(new GameStats("wrongLogo", "testRace", "testDate", "testPlayTime", true));
            playerProfile.UpdateStats(new GameStats("wrongLogo", "testRace", "testDate", "testPlayTime", true));
            playerProfile.UpdateStats(new GameStats("wrongLogo", "testRace", "testDate", "testPlayTime", true));
            playerProfile.UpdateStats(new GameStats("wrongLogo", "testRace", "testDate", "testPlayTime", true));
            playerProfile.UpdateStats(new GameStats("CorrectRaceLogo", "testRace", "testDate", "testPlayTime", true));

            //Assert
            Assert.True(playerProfile.LastRacePlayed == expectedLogo);
        }

    }
}
