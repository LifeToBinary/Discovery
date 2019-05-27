using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Discovery.Client.Search.ViewModels
{
    public class SearchedDiscoverersResultViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Discoverer> _discoverers;
        public ObservableCollection<Discoverer> Discoverers
        {
            get => _discoverers;
            set => SetProperty(ref _discoverers, value);
        }

        private readonly IRegionManager _regionManager;
        public SearchedDiscoverersResultViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewThisDiscoverersHomePageCommand = 
                new DelegateCommand<Discoverer>(ViewThisDiscoverersHomePage);
        }

        public DelegateCommand<Discoverer> ViewThisDiscoverersHomePageCommand { get; }
        private void ViewThisDiscoverersHomePage(Discoverer discoverer)
        {
            if (discoverer.BasicInfo.ID == GlobalObjectHolder.CurrentUser.BasicInfo.ID)
            {
                _regionManager.RequestNavigate(
                    RegionNames.MainMenuContent,
                    ViewNames.DiscovererHomePage);
                return;
            }
            _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverers"] 
                is IEnumerable<Discoverer> discoverers)
            {
                Discoverers = new ObservableCollection<Discoverer>(discoverers);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
