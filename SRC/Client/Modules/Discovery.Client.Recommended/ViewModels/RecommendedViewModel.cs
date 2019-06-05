using Discovery.Client.Recommended.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace Discovery.Client.Recommended.ViewModels
{
    public class RecommendedViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 推荐的帖子
        /// </summary>
        private ObservableCollection<Post> _recommendedPosts;
        public ObservableCollection<Post> RecommendedPosts
        {
            get => _recommendedPosts;
            set => SetProperty(ref _recommendedPosts, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public RecommendedViewModel(IRegionManager regionManager)
        {
            _recommendedPosts = new ObservableCollection<Post>();
            LoadData();
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
            NavigateToSearchViewCommand = new DelegateCommand(NavigateToSearchView);
            ReloadDataCommand = new DelegateCommand(LoadData);
            AddNewPostCommand = new DelegateCommand(AddNewPost);
        }

        /// <summary>
        /// 发布帖子
        /// </summary>
        public DelegateCommand AddNewPostCommand { get; }
        private void AddNewPost()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.NewPost);

        /// <summary>
        /// 导航到搜索界面
        /// </summary>
        public DelegateCommand NavigateToSearchViewCommand { get; }
        private void NavigateToSearchView()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.GetSearchContent);

        /// <summary>
        /// 查看帖子
        /// </summary>
        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        /// <summary>
        /// 刷新推荐内容
        /// </summary>
        public DelegateCommand ReloadDataCommand { get; }
        public bool KeepAlive => false;

        private async void LoadData()
        {
            _recommendedPosts.Clear();

            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in await databaseService.GetRecommendedForTheDiscovererAsync(
                                                GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    _recommendedPosts.Add(post);
                }
            }
        }
    }
}
