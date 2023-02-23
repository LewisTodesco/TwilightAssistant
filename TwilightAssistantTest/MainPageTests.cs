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
        void Player_Profiles_Should_Be_Loaded()
        {
            PlayerProfileServices playerProfileServices= new PlayerProfileServices();
            GameServices gameServices= new GameServices();
            MainPageViewModel mpvm = new MainPageViewModel(playerProfileServices, gameServices);

            

        }



    }
}
