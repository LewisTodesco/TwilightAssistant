using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TwilightAssistant.Models;
using TwilightAssistant.ViewModels;
using TwilightAssistant.Services;
using Microsoft.Maui.Graphics;
using System.Security;

namespace TwilightAssistant;

public partial class GamePage3 : ContentPage
{
    //Create all timers
    public IDispatcherTimer timer0;
    public IDispatcherTimer timer1;
    public IDispatcherTimer timer2;

    ////Handle all the {secondTicks, minuteTicks, hourTicks} in one array.
    //int[,] tickArray = new int[8, 3] {
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0},
    //    {0,0,0}
    //};
    //Handle all the {secondTicks, minuteTicks, hourTicks} in one array.
    int[,] tickArray = new int[8, 3] {
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0},
        {0,0,0}
    };

    //List of GamePlayer's
    public ObservableCollection<GamePlayer> GamePlayers { get; set; }

    public ObservableCollection<Game> Games { get; set; }
    public Game ActiveGame { get; set; }

    GameServices gameServices;
    public GamePage3(GamePageViewModel gpvm, GameServices gs)
    {
        //ViewModel
        BindingContext = gpvm; //Set the binding context to the view model so we can pull in our List<GamePlayer> GamePlayers.

        tickArray = gpvm.ActiveGame.TickArray;

        gameServices = gs;

        //Initialize
        InitializeComponent();

        //***** Add a way of updating the tickArray if loading the game up from memory *****
        playerTime0.Text = Tick.TickLogic(tickArray[0, 0], tickArray[0, 1], tickArray[0, 2]);
        playerTime1.Text = Tick.TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        playerTime2.Text = Tick.TickLogic(tickArray[2, 0], tickArray[2, 1], tickArray[2, 2]);
        playerFrame0.BorderColor = Colors.Gray;
        playerFrame1.BorderColor = Colors.Gray;
        playerFrame2.BorderColor = Colors.Gray;
        

        //Create a dispatcher timer for each player
        timer0 = Dispatcher.CreateTimer();
        //Set Tick interval to 1000 milliseconds
        timer0.Interval = TimeSpan.FromMilliseconds(1000);
        //Subscribe the tick event to the method below. This method incraments the secondTick field and calls the TickLogic method to properly display the time.
        timer0.Tick += (s, e) =>
        {
            //Increment seconds by 1 each time, then use the logic below to calculate minutes and hours.
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
            playerTime0.Text = Tick.TickLogic(tickArray[0, 0], tickArray[0, 1], tickArray[0, 2]);
        };
        /*
        player0.Text = GamePlayers[0].Name;
        race0.Text = GamePlayers[0].Race;
        racelogo0.Source = GamePlayers[0].RaceLogo;
        */

        //New Timer
        timer1 = Dispatcher.CreateTimer();
        timer1.Interval = TimeSpan.FromMilliseconds(1000);
        timer1.Tick += (s, e) =>
        {
            tickArray[1, 0]++;
            if (tickArray[1, 0] == 60)
            {
                tickArray[1, 0] = 0;
                tickArray[1, 1]++;
                if (tickArray[1, 1] == 60)
                {
                    tickArray[1, 1] = 0;
                    tickArray[1, 2]++;
                }
            }
            playerTime1.Text = Tick.TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
        /*
        player1.Text = GamePlayers[1].Name;
        race1.Text = GamePlayers[1].Race;
        racelogo1.Source = GamePlayers[1].RaceLogo;
        */

        //New Timer
        timer2 = Dispatcher.CreateTimer();
        timer2.Interval = TimeSpan.FromMilliseconds(1000);
        timer2.Tick += (s, e) =>
        {
            tickArray[2, 0]++;
            if (tickArray[2, 0] == 60)
            {
                tickArray[2, 0] = 0;
                tickArray[2, 1]++;
                if (tickArray[2, 1] == 60)
                {
                    tickArray[2, 1] = 0;
                    tickArray[2, 2]++;
                }
            }
            playerTime2.Text = Tick.TickLogic(tickArray[2, 0], tickArray[2, 1], tickArray[2, 2]);
        };
        /*
        player2.Text = GamePlayers[2].Name;
        race2.Text = GamePlayers[2].Race;
        racelogo2.Source = GamePlayers[2].RaceLogo;
        */

    }

    //Method to start the timer associated with the button pressed.
    private void StartTime(object sender, EventArgs e)
    {
        //Cast the sender into the button object so we can access the ID. Allows us to know which button was pressed.
        ImageButton passedButton = (ImageButton)sender;
        string buttonID = passedButton.StyleId;

        //Check which button has been pressed
        switch (buttonID)
        {
            case "StartBtn0":
                {
                    //Check if the timer is running already, if it is, nothing will happen.
                    if (!timer0.IsRunning)
                    {
                        timer0.Start();
                    }
                    playerFrame0.BorderColor = Colors.DarkGreen;
                    break;
                }
            case "StartBtn1":
                {
                    if (!timer1.IsRunning)
                    {
                        timer1.Start();
                    }
                    playerFrame1.BorderColor = Colors.DarkGreen;
                    break;
                }
            case "StartBtn2":
                {
                    if (!timer2.IsRunning)
                    {
                        timer2.Start();
                    }
                    playerFrame2.BorderColor = Colors.DarkGreen;
                    break;
                }
            default:
                break;
        }

    }

    //Method to stop the timer associated with the button pressed.
    private void StopTime(object sender, EventArgs e)
    {
        //Cast the sender into the button object so we can access the ID. Allows us to know which button was pressed.
        ImageButton passedButton = (ImageButton)sender;
        string buttonID = passedButton.StyleId;

        //Check which button has been pressed
        switch (buttonID)
        {
            case "StopBtn0":
                {
                    //Check if the timer is running already, if it is, stop it, if its not, nothing will happen.
                    if (timer0.IsRunning)
                    {
                        timer0.Stop();
                    }
                    playerFrame0.BorderColor = Colors.Gray;
                    break;
                }
            case "StopBtn1":
                {
                    if (timer1.IsRunning)
                    {
                        timer1.Stop();
                    }
                    playerFrame1.BorderColor = Colors.Gray;
                    break;
                }
            case "StopBtn2":
                {
                    if (timer2.IsRunning)
                    {
                        timer2.Stop();
                    }
                    playerFrame2.BorderColor = Colors.Gray;
                    break;
                }
            default:
                break;
        }



    }

    public int Index { get; set; }
    //Method to stop and record the game state.
    private void EndGame(object sender, EventArgs e)
    {
        //End all the timers.
        timer0.Stop();
        timer1.Stop();
        timer2.Stop();

        
        Games = gameServices.GetGames();
        

        //Update all GamePlayers times in the Active Game
        for (int x = 0; x < Games.Count; x++)
        {
            if (Games[x].IsActive)
            {
                Index = x;
                for (int i = 0; i < Games[x].GamePlayers.Count; i++)
                {
                    Games[x].GamePlayers[i].ElapsedTime = Tick.TickLogic(tickArray[i, 0], tickArray[i, 1], tickArray[i, 2]);
                }
            }
        }
        IDictionary<string, object> passedGames = new Dictionary<string, object>();
        passedGames.Add("Games",Games);
        passedGames.Add("Index", Index);
        passedGames.Add("GamePlayers", Games[Index].GamePlayers);
        Shell.Current.GoToAsync(nameof(SelectWinnerPage), passedGames);

    }

    //A method to convert second ticks, minute ticks and hour ticks into a readable time.
    public string TickLogic(int seconds, int minutes, int hours)
    {
        if (seconds < 10 && minutes == 0 && hours == 0)
        {
            return "00:00:0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes == 0 && hours == 0)
        {
            return "00:00:" + seconds.ToString();
        }
        else if (seconds < 10 && minutes > 0 && minutes < 10 && hours == 0)
        {
            return "00:0" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes > 0 && minutes < 10 && hours == 0)
        {
            return "00:0" + minutes.ToString() + ":" + seconds.ToString();
        }
        else if (seconds < 10 && minutes >= 10 && hours == 0)
        {
            return "00:" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes >= 10 && hours == 0)
        {
            return "00:" + minutes.ToString() + ":" + seconds.ToString();
        }
        else if (seconds == 0 && minutes == 0 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":00:00";
        }
        else if (seconds < 10 && minutes == 0 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":00" + ":0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes == 0 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":00" + ":" + seconds.ToString();
        }
        else if (seconds < 10 && minutes > 0 && minutes < 10 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":0" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes > 0 && minutes < 10 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":0" + minutes.ToString() + ":" + seconds.ToString();
        }
        else if (seconds < 10 && minutes > 0 && minutes >= 10 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":" + minutes.ToString() + ":0" + seconds.ToString();
        }
        else if (seconds >= 10 && minutes >= 10 && hours > 0 && hours < 10)
        {
            return "0" + hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        }
        else
            return "OUT OF RANGE";
    }
}