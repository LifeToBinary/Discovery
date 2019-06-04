using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyHomePageViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        public MyHomePageViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            NavigateViewToMainMenuRegionCommand = new DelegateCommand<string>(NavigateViewToMainMenuRegion);
        }

        public DelegateCommand<string> NavigateViewToMainMenuRegionCommand { get; }
        private void NavigateViewToMainMenuRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                viewName);

        public bool KeepAlive => false;
    }
}
