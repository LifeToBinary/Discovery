using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class MyPostDetailViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        /// <summary>
        /// 帖子
        /// </summary>
        private Post _currentPost;
        public Post CurrentPost
        {
            get => _currentPost;
            set => SetProperty(ref _currentPost, value);
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        private Discoverer _currentUser;
        public Discoverer CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        /// <summary>
        /// 导航到 当前用户的主页
        /// </summary>
        public DelegateCommand NavigateMyHomePageViewToMainMenuRegionCommand { get; }
        private void NavigateMyHomePageViewToMainMenuRegion()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public MyPostDetailViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            UpdatePostCommand = new DelegateCommand(UpdatePost);
            RemoveThisPostCommand = new DelegateCommand(RemoveThisPost);
            NavigateMyHomePageViewToMainMenuRegionCommand =
                new DelegateCommand(NavigateMyHomePageViewToMainMenuRegion);
        }

        /// <summary>
        /// 删除此帖子
        /// </summary>
        public DelegateCommand RemoveThisPostCommand { get; }
        private void RemoveThisPost()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.RemoveAPost(_currentPost.ID);
            }
            _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.Recommended);
        }
        

        /// <summary>
        /// 编辑此帖子
        /// </summary>
        public DelegateCommand UpdatePostCommand { get; }

        /// <summary>
        /// 导航离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

        private void UpdatePost()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.UpdatePostInfo,
                new NavigationParameters
                {
                    { "Post", _currentPost}
                });

        /// <summary>
        /// 导航到此视图时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                CurrentPost = post;
            }
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
    }
}
