using TwilightAssistant;
using TwilightAssistant.Models;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;

namespace TwilightAssistantTest
{
    public class GamePagesTest
    {

        [Fact]
        public void Game_Page_Time_Display_Test()
        {
            //GameServices gameServices = new GameServices();
            //GamePageViewModel gamePageViewModel = new GamePageViewModel(gameServices);
            //GamePage3 testPage = new GamePage3(gamePageViewModel, gameServices);

            //if (testPage.TickLogic(6, 7, 8) != "08:07:06")
            //{
            //    throw new Exception();
            //}
        }

        [Fact]
        void Single_Digit_Inputs_Add_Leading_Zeros()
        {
            Assert.True(Tick.TickLogic(6, 7, 8) == "08:07:06");
        }
        [Fact]
        void Double_Digit_Inputs_Dont_Add_Leading_Zeros_For_Seconds()
        {
            Assert.True(Tick.TickLogic(28, 7, 8) == "08:07:28");
        }
        [Fact]
        void Double_Digit_Inputs_Dont_Add_Leading_Zeros_For_Minutes()
        {
            Assert.True(Tick.TickLogic(6, 29, 8) == "08:29:06");
        }
        [Fact]
        void Double_Digit_Inputs_Are_Out_Of_Scope_For_Hours()
        {
            Assert.True(Tick.TickLogic(6, 7, 10) == "OUT OF RANGE");
        }
        [Fact]
        void Single_Digit_Hours_But_Double_Digit_Mins_And_Seconds()
        {
            Assert.True(Tick.TickLogic(28, 29, 9) == "09:29:28");
        }




        [Fact]
        public void Tick_Array_Test() 
        {
            //60 Seconds
            int[,] sixtyseconds = new int[,] {
            {0,1,0}
            };
            CycleTickArray(60);
            if (tickArray[0,0] != sixtyseconds[0,0] || tickArray[0,1] != sixtyseconds[0,1] || tickArray[0,2] != sixtyseconds[0,2])
            {
                throw new Exception();
            }

            //100 Seconds
            int[,] onehundredseconds = new int[,] {
            {40,1,0}
            };
            CycleTickArray(100);
            if (tickArray[0, 0] != onehundredseconds[0, 0] || tickArray[0, 1] != onehundredseconds[0, 1] || tickArray[0, 2] != onehundredseconds[0, 2])
            {
                throw new Exception();
            }

            //120 Seconds
            int[,] onehundredandtwentyseconds = new int[,] {
            {0,2,0}
            };
            CycleTickArray(120);
            if (tickArray[0, 0] != onehundredandtwentyseconds[0, 0] || tickArray[0, 1] != onehundredandtwentyseconds[0, 1] || tickArray[0, 2] != onehundredandtwentyseconds[0, 2])
            {
                throw new Exception();
            }

            //1 Hour
            int[,] onehour = new int[,] {
            {0,0,1}
            };
            CycleTickArray(3600);
            if (tickArray[0, 0] != onehour[0, 0] || tickArray[0, 1] != onehour[0, 1] || tickArray[0, 2] != onehour[0, 2])
            {
                throw new Exception();
            }

            //3 Hours, 49 minutes, 58 seconds
            int[,] threehoursfourtynineminutesandfiftyeightseconds = new int[,] {
            {58,49,3}
            };
            CycleTickArray(13798);
            if (tickArray[0, 0] != threehoursfourtynineminutesandfiftyeightseconds[0, 0] || tickArray[0, 1] != threehoursfourtynineminutesandfiftyeightseconds[0, 1] || tickArray[0, 2] != threehoursfourtynineminutesandfiftyeightseconds[0, 2])
            {
                throw new Exception();
            }
            
        }

        public int[,] tickArray = new int[,] {
        {0,0,0},
        };

        void CycleTickArray(int numberOfCycles)
        {
            tickArray = new int[,] {
            {0,0,0},
            };

            for (int i = 0; i < numberOfCycles; i++)
            {
                tickArray[0, 0]++;
                if (tickArray[0, 0] == 60)
                {
                    tickArray[0, 0] = 0;
                    tickArray[0, 1]++;
                    if (tickArray[0, 1] == 60)
                    {
                        tickArray[0, 1] = 0;
                        tickArray[0, 2]++;
                    }
                }
            }
        }

    }
}