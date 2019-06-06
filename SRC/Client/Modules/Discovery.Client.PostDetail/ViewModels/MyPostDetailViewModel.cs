using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

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
        /// 帖子的全部评论
        /// </summary>
        private ObservableCollection<PostComment> _postComments;
        public ObservableCollection<PostComment> PostComments
        {
            get => _postComments;
            set => SetProperty(ref _postComments, value);
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
            AddCommentToThePostCommand = 
                new DelegateCommand<string>(AddCommentToThePost);
            ViewThisUsersHomePageCommand =
                new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
        {
            if (discoverer.BasicInfo.ID == GlobalObjectHolder.CurrentUser.BasicInfo.ID)
            {
                _regionManager.RequestNavigate(
                    RegionNames.MainMenuContent,
                    ViewNames.DiscovererHomePage);
                return;
            }
            _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });
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
        
        public DelegateCommand<string> AddCommentToThePostCommand { get; }
        private async void AddCommentToThePost(string comment)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                int newCommentID = await databaseService.AddACommentAsync(
                                       CurrentPost.ID,
                                       CurrentUser.BasicInfo.ID,
                                       comment);
                PostComments.Add(new PostComment
                {
                    ID = newCommentID,
                    PostID = CurrentPost.ID,
                    Author = CurrentUser,
                    Comment = comment,
                    CreationTime = DateTime.Now
                });
            }
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
                LoadPostComments();
            }
        }
        private async void LoadPostComments()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                PostComments = new ObservableCollection<PostComment>(
                    await databaseService.GetCommentsOfThePostAsync(
                        CurrentPost.ID));
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
