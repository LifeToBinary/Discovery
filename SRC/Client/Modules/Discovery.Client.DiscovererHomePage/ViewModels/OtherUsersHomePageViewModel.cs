using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersHomePageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

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
        /// 导航一个视图到 MainMenuContent Region 中
        /// </summary>
        public DelegateCommand<string> NavigateViewToMainMenuContentRegionCommand { get; }
        private void NavigateViewToMainMenuContentRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                viewName,
                new NavigationParameters
                {
                    { "Discoverer", Discoverer }
                });

        /// <summary>
        /// 当前用户是否已经关注了此用户
        /// </summary>
        private bool _isConcernedThisUser;
        public bool IsConcernedThisUser
        {
            get => _isConcernedThisUser;
            set => SetProperty(ref _isConcernedThisUser, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public OtherUsersHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateViewToMainMenuContentRegionCommand =
                new DelegateCommand<string>(NavigateViewToMainMenuContentRegion);
            ConcernOrCancelConcernCommand = new DelegateCommand(ConcernOrCancelConcern);
        }

        /// <summary>
        /// 当前用户关注/取消关注此用户
        /// </summary>
        public DelegateCommand ConcernOrCancelConcernCommand { get; }
        private void ConcernOrCancelConcern()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (IsConcernedThisUser)
                {
                    databaseService.CancelConcern(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcernedThisUser = false;
                }
                else
                {
                    databaseService.ConcernADiscoverer(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcernedThisUser = true;
                }
            }
        }

        /// <summary>
        /// 导航到此视图时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"]
                is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }

        /// <summary>
        /// 查询当前用户是否已经关注了此用户
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                IsConcernedThisUser = 
                    await databaseService.IsFunsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
            }
        }

        /// <summary>
        /// 是否可以导航到此视图
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(
            NavigationContext _)
            => true;

        /// <summary>
        /// 导航离开时
        /// </summary>
        /// <param name="_"></param>
        public void OnNavigatedFrom(
            NavigationContext _)
        {
        }

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
