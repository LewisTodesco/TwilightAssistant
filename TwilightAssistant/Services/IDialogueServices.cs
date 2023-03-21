using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightAssistant.Services
{
    public interface IDialogueServices
    {
        public Task DisplayAlert(string title, string message, string accept);

        public Task<bool> DisplayYesNo(string title, string message, string yes, string no);

    }
}
