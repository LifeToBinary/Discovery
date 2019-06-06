using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyFavoritedPostsViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 当前用户收藏的帖子
        /// </summary>
        private ObservableCollection<Post> _myFavoritedPosts;
        public ObservableCollection<Post> MyFavoritedPosts
        {
            get => _myFavoritedPosts;
            set => SetProperty(ref _myFavoritedPosts, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public MyFavoritedPostsViewModel(IRegionManager regionManager)
        {
            MyFavoritedPosts = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewThisPostDetailCommand = new DelegateCommand<Post>(ViewThisPostDetail);
            ViewThisUsersHomePageCommand =
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            LoadData();
        }

        /// <summary>
        /// 查询当前用户收藏的帖子
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in await databaseService.GetFavoritePostsAsync(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    MyFavoritedPosts.Add(post);
                }
            }
        }
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
        /// 查看帖子
        /// </summary>
        public DelegateCommand<Post> ViewThisPostDetailCommand { get; }
        private void ViewThisPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post}
                });

        /// <summary>
        /// 从Region离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
