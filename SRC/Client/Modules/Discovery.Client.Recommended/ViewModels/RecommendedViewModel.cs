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
    public class RecommendedViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _recommendedPosts;
        public ObservableCollection<Post> RecommendedPosts
        {
            get => _recommendedPosts;
            set => SetProperty(ref _recommendedPosts, value);
        }
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
        public DelegateCommand AddNewPostCommand { get; }
        private void AddNewPost()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.NewPost);

        public DelegateCommand NavigateToSearchViewCommand { get; }
        private void NavigateToSearchView()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.GetSearchContent);

        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        public DelegateCommand ReloadDataCommand { get; }
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
