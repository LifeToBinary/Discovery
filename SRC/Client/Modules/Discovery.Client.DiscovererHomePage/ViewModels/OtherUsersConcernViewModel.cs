using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersConcernViewModel : BindableBase, INavigationAware
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }

        private IRegionManager _regionManager;
        private ObservableCollection<Discoverer> _idols;
        public ObservableCollection<Discoverer> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        public OtherUsersConcernViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetIdols(Discoverer.BasicInfo.ID);
            }
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent,
                                  ViewNames.OtherUsersHomePage,
                                  new NavigationParameters
                                  {
                                      { "Discoverer", discoverer }
                                  });
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                Idols = new ObservableCollection<Discoverer>(LoadData());
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
