using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;

namespace TwilightAssistantTest
{
    public class MainPageTests
    {
        [Fact]
        void Player_Profiles_And_Games_Should_Be_Loaded_From_UpdateMainPage_Method()
        {
            //Arrange
            PlayerProfileServices playerProfileServices= new PlayerProfileServices();
            GameServices gameServices= new GameServices();

            //Act
            MainPageViewModel mpvm = new MainPageViewModel(playerProfileServices, gameServices);
            mpvm.UpdateMainPage("C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\playerprofiles.json", "C:\\Users\\lewis\\source\\repos\\TwilightAssistant\\TwilightAssistantTest\\JsonTestFiles\\games.json");

            //Assert
            Assert.True(mpvm.PlayerProfiles != null);
            Assert.True(mpvm.Games != null);
        }



    }
}
