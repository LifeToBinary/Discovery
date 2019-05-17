using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.MainMenu.ViewModels
{
    public class MainMenuViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        public MainMenuViewModel()
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
        }
    }
}
