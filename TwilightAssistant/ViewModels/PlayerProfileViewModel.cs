using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TwilightAssistant.Models;
using TwilightAssistant.Services;

namespace TwilightAssistant.ViewModels
{
    //When the "Profile" Key is passed from the dictionary, update the Property named Profile
    [QueryProperty("Profile","Profile")]

    public class PlayerProfileViewModel : INotifyPropertyChanged
    {
        //Create the private field for the passed PlayerProfile
        private PlayerProfile profile;

        //Impliment the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        //Create the caller method
        public virtual void OnPropertyChanged([CallerMemberName] string properyname = null)
        { 
            //Null check and call the event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyname));
        }

        //Create the public Profile property that the QueryPropery attribute will update
        public PlayerProfile Profile 
        {
            get => profile;
            //Set the value and notify the UI.
            set 
            { 
                if(value == profile)
                    return;
                profile = value;
                OnPropertyChanged();
            } 
        }


        //Defaul Constructor
        public PlayerProfileViewModel()
        { 
            
        }
    }
}
