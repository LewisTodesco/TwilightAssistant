using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwilightAssistant.Models;

namespace TwilightAssistant.ViewModels
{
    //Passed winner
    [QueryProperty("Winner","Winner")]

    public class WinnerPageViewModel : BaseViewModel
    {

        //Winner to display
        private GamePlayer winner;
        public GamePlayer Winner
        {
            get => winner;
            set 
            {
                winner = value;
                OnPropertyChanged();
            }
        }

        //Button to go home
        private ICommand homeCommand;
        public ICommand HomeCommand => homeCommand ??= new Command(Home);
        public async void Home()
        {
            //var navigationStack = Shell.Current.Navigation.NavigationStack;

            //for(int i = navigationStack.Count -1; i>=1; i--)
            //{
            //    Shell.Current.Navigation.RemovePage(navigationStack[i]);
            //}
            
            await Shell.Current.Navigation.PopToRootAsync();
            //Shell.Current.GoToAsync("../../../../../../..");
        }

    }
}
