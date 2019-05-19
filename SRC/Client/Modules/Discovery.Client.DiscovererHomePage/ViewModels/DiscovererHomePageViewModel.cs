using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class DiscovererHomePageViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        public DiscovererHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigationToProfileContentRegionCommand =
                new DelegateCommand<string>(NavigationToProfileContentRegion);
            CurrentUser = GlobalObjectHolder.CurrentUser;
        }
        public DelegateCommand<string> NavigationToProfileContentRegionCommand { get; }
        private void NavigationToProfileContentRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.ProfileContent, viewName);
    }
}
