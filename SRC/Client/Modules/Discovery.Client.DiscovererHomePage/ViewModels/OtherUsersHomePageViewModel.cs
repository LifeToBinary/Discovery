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

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersHomePageViewModel : BindableBase, INavigationAware
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private readonly IRegionManager _regionManager;
        public OtherUsersHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigationToProfileContentRegionCommand =
                new DelegateCommand<string>(NavigationToProfileContentRegion);
        }
        public DelegateCommand<string> NavigationToProfileContentRegionCommand { get; }

        private void NavigationToProfileContentRegion(string viewName)
            => _regionManager.RequestNavigate(
                                  RegionNames.OtherUsersProfileContent, 
                                  viewName,
                                  new NavigationParameters
                                  {
                                      { "Discoverer", Discoverer }
                                  });

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
