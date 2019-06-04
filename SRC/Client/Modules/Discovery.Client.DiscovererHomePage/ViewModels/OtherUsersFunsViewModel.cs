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
    public class OtherUsersFunsViewModel
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
        /// 关注此用户的人
        /// </summary>
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        public ObservableCollection<DiscovererRelationshipModel> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public OtherUsersFunsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Funs = new ObservableCollection<DiscovererRelationshipModel>();
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
        /// 查询关注此用户的人
        /// </summary>
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

        /// <summary>
        /// 是否可以导航到此视图
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// 导航离开时
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
