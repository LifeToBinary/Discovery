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
    public class MainMenuViewModel : BindableBase, INavigationAware
    {
        private Discoverer _currentUser;
        public Discoverer CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
            => CurrentUser = navigationContext.Parameters["CurrentUser"] as Discoverer;
    }
}
