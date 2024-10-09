using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Interfaces
{
    public interface IDialogService
    {
        Task<string> ShowActionSheet(string title, string cancel, string destruction, params string[] buttons);
        Task ShowAlert(string title, string message, string cancel);
    }
}
