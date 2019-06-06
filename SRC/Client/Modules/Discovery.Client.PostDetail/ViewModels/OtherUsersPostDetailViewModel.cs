using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class OtherUsersPostDetailViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
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
        /// 此帖子被收藏的次数
        /// </summary>
        private int _favoriteCount;
        public int FavoriteCount
        {
            get => _favoriteCount;
            set => SetProperty(ref _favoriteCount, value);
        }

        /// <summary>
        /// 当前用户是否已经收藏了此帖子
        /// </summary>
        private bool _currentUserIsFavoritedThisPost;
        public bool CurrentUserIsFavoritedThisPost
        {
            get => _currentUserIsFavoritedThisPost;
            set => SetProperty(ref _currentUserIsFavoritedThisPost, value);
        }

        /// <summary>
        /// 当前用户是否已经关注了此帖子的作者
        /// </summary>
        private bool _currentUserIsAFunOfTheAuthor;
        public bool CurrentUserIsAFunOfTheAuthor
        {
            get => _currentUserIsAFunOfTheAuthor;
            set => SetProperty(ref _currentUserIsAFunOfTheAuthor, value);
        }

        /// <summary>
        /// 此帖子的作者
        /// </summary>
        private Discoverer _authorOfThePost;
        public Discoverer AuthorOfThePost
        {
            get => _authorOfThePost;
            set => SetProperty(ref _authorOfThePost, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
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

        /// <summary>
        /// 查看作者的主页
        /// </summary>
        public DelegateCommand ViewAuthorsHomePageCommand { get; }
        private void ViewAuthorsHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", AuthorOfThePost }
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
                LoadData();
            }
        }

        /// <summary>
        /// 查询此帖子的作者, 被收藏的次数, 是否被当前用户收藏, 以及此帖子的作者是否被当前用户关注
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                AuthorOfThePost = 
                    await databaseService.GetDiscovererByIDAsync(
                        CurrentPost.Author.BasicInfo.ID);
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

        /// <summary>
        /// 当前用户收藏/取消收藏此帖子
        /// </summary>
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

        /// <summary>
        /// 当前用户关注/取消关注此帖子的作者
        /// </summary>
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
