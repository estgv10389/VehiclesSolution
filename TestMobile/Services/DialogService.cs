using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMobile.Interfaces;

namespace TestMobile.Services
{
    public class DialogService : IDialogService
    {
        private Page _page;

        public DialogService(Page page)
        {
            _page = page;
        }

        public Task<string> ShowActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return _page.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public Task ShowAlert(string title, string message, string cancel)
        {
            return _page.DisplayAlert(title, message, cancel);
        }
    }

}
