using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class OtherUsersPostDetailViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private Post _currentPost;
        public Post CurrentPost
        {
            get => _currentPost;
            set => SetProperty(ref _currentPost, value);
        }
        private int _favoriteCount;
        public int FavoriteCount
        {
            get => _favoriteCount;
            set => SetProperty(ref _favoriteCount, value);
        }
        private bool _currentUserIsFavoritedThisPost;
        public bool CurrentUserIsFavoritedThisPost
        {
            get => _currentUserIsFavoritedThisPost;
            set => SetProperty(ref _currentUserIsFavoritedThisPost, value);
        }
        private bool _currentUserIsAFunOfTheAuthor;
        public bool CurrentUserIsAFunOfTheAuthor
        {
            get => _currentUserIsAFunOfTheAuthor;
            set => SetProperty(ref _currentUserIsAFunOfTheAuthor, value);
        }
        private Discoverer _authorOfThePost;
        public Discoverer AuthorOfThePost
        {
            get => _authorOfThePost;
            set => SetProperty(ref _authorOfThePost, value);
        }

        public bool KeepAlive => false;
        private IRegionManager _regionManager;
        public OtherUsersPostDetailViewModel(IRegionManager regionManager)
        {
            ViewAuthorsHomePageCommand =
                new DelegateCommand(ViewAuthorsHomePage);
            _regionManager = regionManager;
            FavoriteOrCancelFavoriteThePostCommand =
                new DelegateCommand(FavoritedOrCancelFavoritedThePost);
            ConcernOrCancelConcernAuthorCommand =
                new DelegateCommand(ConcernOrCancelConcernAuthor);

        }
        public DelegateCommand ViewAuthorsHomePageCommand { get; }
        private void ViewAuthorsHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", AuthorOfThePost }
                });
        
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                CurrentPost = post;
                LoadData();
            }
        }
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                AuthorOfThePost = 
                    await databaseService.GetDiscovererByIDAsync(
                        CurrentPost.AuthorID);
                FavoriteCount =
                    await databaseService.GetFavoritesCountOfThePostAsync(
                        CurrentPost.ID);
                CurrentUserIsFavoritedThisPost =
                    await databaseService.IsFavoritedAPostAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        CurrentPost.ID);
                CurrentUserIsAFunOfTheAuthor =
                    await databaseService.IsFunsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        AuthorOfThePost.BasicInfo.ID);
            }
        }
        public DelegateCommand FavoriteOrCancelFavoriteThePostCommand { get; }
        private void FavoritedOrCancelFavoritedThePost()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (CurrentUserIsFavoritedThisPost)
                {
                    databaseService.CancelFavorite(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        CurrentPost.ID);
                    CurrentUserIsFavoritedThisPost = false;
                    return;
                }
                databaseService.FavoriteAPost(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                    CurrentPost.ID);
                CurrentUserIsFavoritedThisPost = true;
            }
        }
        public DelegateCommand ConcernOrCancelConcernAuthorCommand { get; }
        private void ConcernOrCancelConcernAuthor()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (CurrentUserIsAFunOfTheAuthor)
                {
                    databaseService.CancelConcern(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        AuthorOfThePost.BasicInfo.ID);
                    CurrentUserIsAFunOfTheAuthor = false;
                    return;
                }
                databaseService.ConcernADiscoverer(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                    AuthorOfThePost.BasicInfo.ID);
                CurrentUserIsAFunOfTheAuthor = true;
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
