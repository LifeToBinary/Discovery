using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class MyPostDetailViewModel : BindableBase, INavigationAware
    {
        private Post _currentPost;
        public Post CurrentPost
        {
            get => _currentPost;
            set => SetProperty(ref _currentPost, value);
        }
        private Discoverer _currentUser;
        public Discoverer CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }
        public DelegateCommand NavigateMyHomePageViewToMainMenuRegionCommand { get; }
        private void NavigateMyHomePageViewToMainMenuRegion()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
        private readonly IRegionManager _regionManager;
        public MyPostDetailViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            UpdatePostCommand = new DelegateCommand(UpdatePost);
            RemoveThisPostCommand = new DelegateCommand(RemoveThisPost);
            NavigateMyHomePageViewToMainMenuRegionCommand =
                new DelegateCommand(NavigateMyHomePageViewToMainMenuRegion);
        }

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
        
        public DelegateCommand UpdatePostCommand { get; }
        private void UpdatePost()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.UpdatePostInfo,
                new NavigationParameters
                {
                    { "Post", _currentPost}
                });

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                CurrentPost = post;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
