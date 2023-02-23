using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Models
{
    public static class Tick
    {
        public static string TickLogic(int seconds, int minutes, int hours)
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
}
