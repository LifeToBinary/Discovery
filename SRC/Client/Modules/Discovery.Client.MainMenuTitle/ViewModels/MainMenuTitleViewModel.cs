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

namespace Discovery.Client.MainMenuTitle.ViewModels
{
    public class MainMenuTitleViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        public DelegateCommand<string> NavigationToMainMenuContentRegionCommand { get; }
        private IRegionManager _regionManager;
        public MainMenuTitleViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            NavigationToMainMenuContentRegionCommand = 
                new DelegateCommand<string>(NavigationViewToMainMenuContentRegion);
        }
        private void NavigationViewToMainMenuContentRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.MainMenuContent, viewName);
    }
}
