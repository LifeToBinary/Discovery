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
    public class IPostedPostsViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 当前用户对象
        /// </summary>
        public Discoverer CurrentUser { get; }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 当前用户发布的帖子
        /// </summary>
        private ObservableCollection<Post> _postedIPost;
        public ObservableCollection<Post> PostedIPost
        {
            get => _postedIPost;
            set => SetProperty(ref _postedIPost, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public IPostedPostsViewModel(IRegionManager regionManager)
        {
            PostedIPost = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewMyPostDetailCommand = new DelegateCommand<Post>(ViewMyPostDetail);
            LoadData();
        }

        /// <summary>
        /// 查询当前用户发布的帖子
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in 
                    await databaseService.GetPostsOfTheDiscovererAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    PostedIPost.Add(post);
                }
            }
        }

        /// <summary>
        /// 从Region离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

        /// <summary>
        /// 查看帖子
        /// </summary>
        public DelegateCommand<Post> ViewMyPostDetailCommand { get; }

        private void ViewMyPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.MyPostDetail,
                new NavigationParameters
                {
                    { "Post", post}
                });
    }
}
