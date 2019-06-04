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
    public class MyFunsViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        public ObservableCollection<DiscovererRelationshipModel> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public MyFunsViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            Funs = new ObservableCollection<DiscovererRelationshipModel>();
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            _regionManager = regionManager;
            LoadData();
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        public bool KeepAlive => false;

        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer" , discoverer }
                });
        private async void LoadData()
        {
            KeyValuePair<Discoverer, bool>[] currentUsersRelationshipWithAnotherUsersFuns;
            using (var databaseService = new DataBaseServiceClient())
            {
                currentUsersRelationshipWithAnotherUsersFuns =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersFunsAsync(
                        CurrentUser.BasicInfo.ID,
                        CurrentUser.BasicInfo.ID);
            }
            Funs = new ObservableCollection<DiscovererRelationshipModel>(
                       currentUsersRelationshipWithAnotherUsersFuns.Select(item =>
                           new DiscovererRelationshipModel(
                               item.Key,
                               item.Value)));
        }
    }
}
