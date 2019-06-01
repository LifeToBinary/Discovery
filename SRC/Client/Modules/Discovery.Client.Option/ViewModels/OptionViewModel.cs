using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.Option.ViewModels
{
    public class OptionViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        public OptionViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            SignOutCommand = new DelegateCommand(SignOut);
            NavigationSignInViewToMainMenuContentRegionCommand =
                new DelegateCommand(NavigationSignInViewToMainMenuContentRegion);
        }
        public DelegateCommand NavigationSignInViewToMainMenuContentRegionCommand {get;}
        private void NavigationSignInViewToMainMenuContentRegion()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
        public DelegateCommand SignOutCommand { get; }
        public void SignOut()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignIn);
    }
}
