using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Services
{
    public class DialogueServices : IDialogueServices
    {
        public async Task DisplayAlert(string title, string message, string accept)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept);
        }

        public async Task<bool> DisplayYesNo(string title, string message, string yes, string no)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, yes, no);
        }


    }
}
