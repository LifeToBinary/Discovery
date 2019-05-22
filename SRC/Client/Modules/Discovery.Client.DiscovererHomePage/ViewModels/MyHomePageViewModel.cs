using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyHomePageViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _postsIPost;
        public ObservableCollection<Post> PostsIPost
        {
            get => _postsIPost;
            set => SetProperty(ref _postsIPost, value);
        }
        private ObservableCollection<Discoverer> _idols;
        public ObservableCollection<Discoverer> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        private ObservableCollection<Post> _myFavorites;
        public ObservableCollection<Post> MyFavorites
        {
            get => _myFavorites;
            set => SetProperty(ref _myFavorites, value);
        }
        private ObservableCollection<Discoverer> _funs;
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public MyHomePageViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
            ViewMyPostDetailCommand = new DelegateCommand<Post>(ViewMyPostDetail);
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            DeleteThisPostCommand = new DelegateCommand<Post>(DeleteThisPost);
            CancelFavoriteCommand = new DelegateCommand<Post>(CancelFavorite);
            CancelConcernCommand = new DelegateCommand<Discoverer>(CancelConcern);
            LoadData();
        }

        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                PostsIPost = 
                    new ObservableCollection<Post>(
                        await databaseService.GetPostsOfTheDiscovererAsync(
                            CurrentUser.BasicInfo.ID));
                Idols =
                    new ObservableCollection<Discoverer>(
                        await databaseService.GetIdolsAsync(
                            CurrentUser.BasicInfo.ID));

                MyFavorites =
                    new ObservableCollection<Post>(
                        await databaseService.GetFavoritePostsAsync(
                            CurrentUser.BasicInfo.ID));

                Funs =
                    new ObservableCollection<Discoverer>(
                        await databaseService.GetFunsOfTheIdolAsync(
                            CurrentUser.BasicInfo.ID));
            }
        }

        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });

        public DelegateCommand<Post> ViewMyPostDetailCommand { get; }
        private void ViewMyPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.MyPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        public DelegateCommand<Post> DeleteThisPostCommand { get; }
        private void DeleteThisPost(Post post)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.RemoveAPost(post.ID);
            }
            PostsIPost.Remove(post);
        }

        public DelegateCommand<Post> CancelFavoriteCommand { get; }
        private void CancelFavorite(Post post)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelFavorite(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID, 
                    post.ID);
            }
            MyFavorites.Remove(post);
        }

        public DelegateCommand<Discoverer> CancelConcernCommand { get; }
        private void CancelConcern(Discoverer idol)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelConcern(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                    idol.BasicInfo.ID);
            }
            Idols.Remove(idol);
        }
        public bool KeepAlive => false;
    }
}
