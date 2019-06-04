using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using Discovery.Core.RelationalModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discovery.Core.GlobalData;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    class OtherUsersConcernViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private ObservableCollection<DiscovererRelationshipModel> _idols;
        public ObservableCollection<DiscovererRelationshipModel> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        private readonly IRegionManager _regionManager;
        public OtherUsersConcernViewModel(IRegionManager regionManager)
        {
            Idols = new ObservableCollection<DiscovererRelationshipModel>();
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = 
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
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
                LoadData();
            }
        }
        private async void LoadData()
        {
            KeyValuePair<Discoverer, bool>[] thisUsersRelationshipWithAnotherUsersIdols;
            using (var databaseService = new DataBaseServiceClient())
            {
                thisUsersRelationshipWithAnotherUsersIdols =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersIdolsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
            }
            Idols = new ObservableCollection<DiscovererRelationshipModel>(
                        thisUsersRelationshipWithAnotherUsersIdols.Select(
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
