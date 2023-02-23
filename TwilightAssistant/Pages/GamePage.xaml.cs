using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TwilightAssistant.Models;
using TwilightAssistant.ViewModels;

namespace TwilightAssistant;

public partial class GamePage : ContentPage
{
    //Create all timers
    public IDispatcherTimer timer0;
    public IDispatcherTimer timer1;
    public IDispatcherTimer timer2;
    public IDispatcherTimer timer3;
    public IDispatcherTimer timer4;
    public IDispatcherTimer timer5;
    public IDispatcherTimer timer6;
    public IDispatcherTimer timer7;

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

    public GamePage(GamePageViewModel gpvm)
	{
        //ViewModel
        BindingContext = gpvm; //Set the binding context to the view model so we can pull in our List<GamePlayer> GamePlayers.

        GamePlayers = gpvm.GamePlayers;

        /*
        //List<IDispatcherTimer> timers
        timers = new List<IDispatcherTimer>(); //Create a list of DispatcherTimers, this can be done based on the number of players in GamePlayers list.

        for (int i = 0; i < GamePlayers.Count; i++)
        {
            timers.Add(Dispatcher.CreateTimer());   //**IMPORTANT: a list of timers did not work as there is no identifier for each timer so the OnTickEvent wont know which ticks in the
                                                    //tick array to update. Instead, create 8 timers using the timer1, timer2 etc. and use Lambda expression to resolve the TickEvent.
                                                    //The lambda expression can call a method called TickCalculator(int seconds) and use the ticks[] to process the minutes and hours,
                                                    //then call the TickLogic method. MAYBE IT CAN ALL BE DONE IN ONE TICK LOGIC METHOD?
        }
        
        foreach (IDispatcherTimer timer in timers) //Set the properties of each timer and subscribe to the OnTickEvent method.
        {
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnTickEvent;
            //timer.Start();
        }
        */

        //Initialize
        InitializeComponent();

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
            playerTime0.Text = TickLogic(tickArray[0, 0], tickArray[0, 1], tickArray[0, 2]);
        };

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
            playerTime1.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
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
            //playerTime2.Text = TickLogic(tickArray[2, 0], tickArray[2, 1], tickArray[2, 2]);
        };
        //New Timer
        timer3 = Dispatcher.CreateTimer();
        timer3.Interval = TimeSpan.FromMilliseconds(1000);
        timer3.Tick += (s, e) =>
        {
            tickArray[3, 0]++;
            if (tickArray[3, 0] == 60)
            {
                tickArray[3, 0] = 0;
                tickArray[3, 1]++;
                if (tickArray[3, 1] == 60)
                {
                    tickArray[3, 1] = 0;
                    tickArray[3, 2]++;
                }
            }
            //playerTime3.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
        //New Timer
        timer4 = Dispatcher.CreateTimer();
        timer4.Interval = TimeSpan.FromMilliseconds(1000);
        timer4.Tick += (s, e) =>
        {
            tickArray[4, 0]++;
            if (tickArray[4, 0] == 60)
            {
                tickArray[4, 0] = 0;
                tickArray[4, 1]++;
                if (tickArray[4, 1] == 60)
                {
                    tickArray[4, 1] = 0;
                    tickArray[4, 2]++;
                }
            }
            //playerTime4.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
        //New Timer
        timer5 = Dispatcher.CreateTimer();
        timer5.Interval = TimeSpan.FromMilliseconds(1000);
        timer5.Tick += (s, e) =>
        {
            tickArray[5, 0]++;
            if (tickArray[5, 0] == 60)
            {
                tickArray[5, 0] = 0;
                tickArray[5, 1]++;
                if (tickArray[5, 1] == 60)
                {
                    tickArray[5, 1] = 0;
                    tickArray[5, 2]++;
                }
            }
            //playerTime5.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
        //New Timer
        timer6 = Dispatcher.CreateTimer();
        timer6.Interval = TimeSpan.FromMilliseconds(1000);
        timer6.Tick += (s, e) =>
        {
            tickArray[6, 0]++;
            if (tickArray[6, 0] == 60)
            {
                tickArray[6, 0] = 0;
                tickArray[6, 1]++;
                if (tickArray[6, 1] == 60)
                {
                    tickArray[6, 1] = 0;
                    tickArray[6, 2]++;
                }
            }
            //playerTime6.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };
        //New Timer
        timer7 = Dispatcher.CreateTimer();
        timer7.Interval = TimeSpan.FromMilliseconds(1000);
        timer7.Tick += (s, e) =>
        {
            tickArray[7, 0]++;
            if (tickArray[7, 0] == 60)
            {
                tickArray[7, 0] = 0;
                tickArray[7, 1]++;
                if (tickArray[7, 1] == 60)
                {
                    tickArray[7, 1] = 0;
                    tickArray[7, 2]++;
                }
            }
            //playerTime7.Text = TickLogic(tickArray[1, 0], tickArray[1, 1], tickArray[1, 2]);
        };

        player1.Text = GamePlayers[0].Name;
    }

    //This method didnt work as I couldnt detemine which timer used the event.
    /* 
    private void OnTickEvent(object s, EventArgs e)
    {
        int playerNumber = 1;
        
        secondTicks1++; //Increment seconds by 1 each time, then use the logic below to calculate minutes and hours.
        if (secondTicks1 == 60)
        {
            secondTicks1 = 0;
            minuteTicks1++;
            if (minuteTicks1 == 60)
            {
                minuteTicks1 = 0;
                hourTicks1++;
            }
        }

        playerTime1.Text = TickLogic(secondTicks1, minuteTicks1, hourTicks1);
    }
    */


    //Method to start the timer associated with the button pressed.
    private void StartTime(object sender, EventArgs e)
    {
        //Cast the sender into the button object so we can access the ID. Allows us to know which button was pressed.
        Button passedButton = (Button)sender;
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
                    break;
                }
            case "StartBtn1":
                {
                    if (!timer1.IsRunning)
                    {
                        timer1.Start();
                    }
                    break;
                }
            case "StartBtn2":
                {
                    if (!timer2.IsRunning)
                    {
                        timer2.Start();
                    }
                    break;
                }
            case "StartBtn3":
                {
                    if (!timer3.IsRunning)
                    {
                        timer3.Start();
                    }
                    break;
                }
            case "StartBtn4":
                {
                    if (!timer4.IsRunning)
                    {
                        timer4.Start();
                    }
                    break;
                }
            case "StartBtn5":
                {
                    if (!timer5.IsRunning)
                    {
                        timer5.Start();
                    }
                    break;
                }
            case "StartBtn6":
                {
                    if (!timer6.IsRunning)
                    {
                        timer6.Start();
                    }
                    break;
                }
            case "StartBtn7":
                {
                    if (!timer7.IsRunning)
                    {
                        timer7.Start();
                    }
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
        Button passedButton = (Button)sender;
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
                    break;
                }
            case "StopBtn1":
                {
                    if (timer1.IsRunning)
                    {
                        timer1.Stop();
                    }
                    break;
                }
            case "StopBtn2":
                {
                    if (timer2.IsRunning)
                    {
                        timer2.Stop();
                    }
                    break;
                }
            case "StopBtn3":
                {
                    if (timer3.IsRunning)
                    {
                        timer3.Stop();
                    }
                    break;
                }
            case "StopBtn4":
                {
                    if (timer4.IsRunning)
                    {
                        timer4.Stop();
                    }
                    break;
                }
            case "StopBtn5":
                {
                    if (timer5.IsRunning)
                    {
                        timer5.Stop();
                    }
                    break;
                }
            case "StopBtn6":
                {
                    if (timer6.IsRunning)
                    {
                        timer6.Stop();
                    }
                    break;
                }
            case "StopBtn7":
                {
                    if (timer7.IsRunning)
                    {
                        timer7.Stop();
                    }
                    break;
                }
            default:
                break;
        }
    }

    //Method to stop and record the game state.
    private void EndGame(object sender, EventArgs e)
    {
        //End all the timers.
        timer0.Stop();
        timer1.Stop();
        timer2.Stop();
        timer3.Stop();
        timer4.Stop();
        timer5.Stop();
        timer6.Stop();
        timer7.Stop();

        //Update all GamePlayers final ElapsedTime.
        for (int i = 0; i < GamePlayers.Count; i++)
        {
            GamePlayers[i].ElapsedTime = TickLogic(tickArray[i, 0], tickArray[i, 1], tickArray[i, 2]);
        }

        //Open file stream to write a Json file for the GamePlayers list. This can then be accessed by another class to UpdateStats on Player Profile.
        using (StreamWriter file = File.CreateText(@"C:\temp\timingtest.txt"))
        {
            var gamePlayersjson = JsonConvert.SerializeObject(GamePlayers);
            file.Write(gamePlayersjson);
        }

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