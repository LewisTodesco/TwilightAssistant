using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;
using System.Collections.ObjectModel;
using TwilightAssistant.Models;

namespace TwilightAssistantTest
{
    public class SelectWinnerPageTests
    {

        [Fact]
        void Selecting_A_Winner_Should_Update_All_Player_Stats_For_All_GamePlayers_Only()
        {
            //Arrange
            PlayerProfileServices playerProfileServices = new PlayerProfileServices();
            GameServices gameServices = new GameServices();
            SelectWinnerViewModel selectWinnerViewModel = new SelectWinnerViewModel(playerProfileServices, gameServices);

            //selectWinnerViewModel.PlayerProfiles.Clear();
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer1"));
            selectWinnerViewModel.PlayerProfiles[0].Id = "METHOD CALLED FROM TEST";
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer2"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer3"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer4"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer5"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer6"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer7"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer8"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer9"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer10"));
            selectWinnerViewModel.PlayerProfiles.Add(new PlayerProfile("TestPlayer11"));

            selectWinnerViewModel.Games = new ObservableCollection<Game>
            {
                {new Game(new ObservableCollection<GamePlayer> {
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[0].Name,selectWinnerViewModel.PlayerProfiles[0].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[2].Name,selectWinnerViewModel.PlayerProfiles[2].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[3].Name,selectWinnerViewModel.PlayerProfiles[3].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[5].Name,selectWinnerViewModel.PlayerProfiles[5].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[6].Name,selectWinnerViewModel.PlayerProfiles[6].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[8].Name,selectWinnerViewModel.PlayerProfiles[8].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[9].Name,selectWinnerViewModel.PlayerProfiles[9].Id)},
                {new GamePlayer(selectWinnerViewModel.PlayerProfiles[10].Name,selectWinnerViewModel.PlayerProfiles[10].Id)}
                }) }
            };
            
            selectWinnerViewModel.Index = 0;

            //Act
            selectWinnerViewModel.AssignWinner(selectWinnerViewModel.Games[selectWinnerViewModel.Index].GamePlayers[2]);

            //Assert
            Assert.True(selectWinnerViewModel.PlayerProfiles[0].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[1].GamesPlayed == 0);
            Assert.True(selectWinnerViewModel.PlayerProfiles[2].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[3].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[4].GamesPlayed == 0);
            Assert.True(selectWinnerViewModel.PlayerProfiles[5].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[6].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[7].GamesPlayed == 0);
            Assert.True(selectWinnerViewModel.PlayerProfiles[8].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[9].GamesPlayed == 1);
            Assert.True(selectWinnerViewModel.PlayerProfiles[10].GamesPlayed == 1);

        }

        [Fact]
        void Selecting_A_Winner_Should_Change_The_Active_Game_To_Inactive()
        { 
        
        }

        [Fact]
        void Selecting_A_Winner_Should_Add_That_Game_To_The_Games_Collection()
        { 
        
        }

    }
}
