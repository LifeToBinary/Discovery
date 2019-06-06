using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersFavoritedPostsViewModel 
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
        /// 此用户的收藏
        /// </summary>
        private ObservableCollection<Post> _favoritedPosts;
        public ObservableCollection<Post> FavoritedPosts
        {
            get => _favoritedPosts;
            set => SetProperty(ref _favoritedPosts, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public OtherUsersFavoritedPostsViewModel(IRegionManager regionManager)
        {
            FavoritedPosts = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewThisPostDetailCommand = 
                new DelegateCommand<Post>(ViewThisPostDetail);
            ViewThisUsersHomePageCommand =
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }

        /// <summary>
        /// 查看帖子
        /// </summary>
        public DelegateCommand<Post> ViewThisPostDetailCommand { get; }
        private void ViewThisPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; set; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });
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
        /// 查询此用户收藏的帖子
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in await databaseService.GetFavoritePostsAsync(
                    Discoverer.BasicInfo.ID))
                {
                    FavoritedPosts.Add(post);
                }
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

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
