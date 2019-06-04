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
    public class MyFunsViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public Discoverer CurrentUser { get; }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 关注他(当前用户)的用户
        /// </summary>
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        public ObservableCollection<DiscovererRelationshipModel> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public MyFunsViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            Funs = new ObservableCollection<DiscovererRelationshipModel>();
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            _regionManager = regionManager;
            LoadData();
        }

        /// <summary>
        /// 查看一个用户的主页
        /// </summary>
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer" , discoverer }
                });

        /// <summary>
        /// 查询关注他(当前用户)的人
        /// </summary>
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

        /// <summary>
        /// 从Region离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
