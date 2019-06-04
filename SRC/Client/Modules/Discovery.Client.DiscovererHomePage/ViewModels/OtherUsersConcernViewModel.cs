using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersConcernViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        /// <summary>
        /// 用户
        /// </summary>
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }

        /// <summary>
        /// 此用户关注的人
        /// </summary>
        private ObservableCollection<DiscovererRelationshipModel> _idols;
        public ObservableCollection<DiscovererRelationshipModel> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;
        public OtherUsersConcernViewModel(IRegionManager regionManager)
        {
            Idols = new ObservableCollection<DiscovererRelationshipModel>();
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = 
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }

        /// <summary>
        /// 查看一个用户的主页
        /// </summary>
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

        /// <summary>
        /// 导航到此视图时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }

        /// <summary>
        /// 查询此用户关注的人
        /// </summary>
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

        /// <summary>
        /// 是否可以导航到此视图
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// 导航从此视图离开时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
