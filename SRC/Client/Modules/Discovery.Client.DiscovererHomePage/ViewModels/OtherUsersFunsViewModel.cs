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
    public class OtherUsersFunsViewModel : BindableBase, INavigationAware
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
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
        private ObservableCollection<Discoverer> _funs;
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        private IRegionManager _regionManager;
        public OtherUsersFunsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetFunsOfTheIdol(_discoverer.BasicInfo.ID);
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                Funs = new ObservableCollection<Discoverer>(LoadData());
            }
        }
    }
}
