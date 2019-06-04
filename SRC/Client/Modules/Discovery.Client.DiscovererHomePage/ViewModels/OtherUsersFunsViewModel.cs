using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel;
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
    class OtherUsersFunsViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        public ObservableCollection<DiscovererRelationshipModel> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        private readonly IRegionManager _regionManager;
        public OtherUsersFunsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Funs = new ObservableCollection<DiscovererRelationshipModel>();
            ViewThisUsersHomePageCommand = 
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
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
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }
        private async void LoadData()
        {
            KeyValuePair<Discoverer, bool>[] thisUsersRelationshipWithAnotherUsersFuns;
            using (var databaseService = new DataBaseServiceClient())
            {
                thisUsersRelationshipWithAnotherUsersFuns =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersFunsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
            }
            Funs = new ObservableCollection<DiscovererRelationshipModel>(
                       thisUsersRelationshipWithAnotherUsersFuns.Select(
                           item => new DiscovererRelationshipModel(
                                       item.Key,
                                       item.Value)));
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        public bool KeepAlive => false;
    }
}
