using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        public ObservableCollection<DiscovererRelationshipModel> Funs
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
            CancelFavoriteCommand = new DelegateCommand<Post>(CancelFavorite);
            CancelConcernThisUserCommand = new DelegateCommand<Discoverer>(CancelConcern);
            ConcernThisUserCommand = new DelegateCommand<Discoverer>(ConcernThisUser);
            NavigateViewToMainMenuRegionCommand = new DelegateCommand<string>(NavigateViewToMainMenuRegion);
            LoadData();
        }

        private async void LoadData()
        {
            KeyValuePair<Discoverer, bool>[] currentUsersRelationshipWithAnotherUsersFuns;
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

                currentUsersRelationshipWithAnotherUsersFuns =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersFunsAsync(
                        CurrentUser.BasicInfo.ID,
                        CurrentUser.BasicInfo.ID);
            }

            Funs = new ObservableCollection<DiscovererRelationshipModel>(
                       currentUsersRelationshipWithAnotherUsersFuns.Select(item =>
                           new DiscovererRelationshipModel(
                               item.Key,
                               item.Value)));
            Funs.ToList().ForEach(user => user.PropertyChanged += MyFunsCollectionChanged);
        }
        private void MyFunsCollectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DiscovererRelationshipModel.IsBeConcerned) &&
                sender is DiscovererRelationshipModel discovererRelationshipModel)
            {
                if (discovererRelationshipModel.IsBeConcerned)
                {
                    Idols.Add(discovererRelationshipModel.Discoverer);
                }
                else
                {
                    Idols.Remove(discovererRelationshipModel.Discoverer);
                }
            }
        }

        public DelegateCommand<string> NavigateViewToMainMenuRegionCommand { get; }
        private void NavigateViewToMainMenuRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                viewName);
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

        public DelegateCommand<Post> CancelFavoriteCommand { get; }
        private void CancelFavorite(Post post)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelFavorite(
                    CurrentUser.BasicInfo.ID,
                    post.ID);
            }
            MyFavorites.Remove(post);
        }

        public DelegateCommand<Discoverer> CancelConcernThisUserCommand { get; }
        private void CancelConcern(Discoverer idol)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelConcern(
                    CurrentUser.BasicInfo.ID,
                    idol.BasicInfo.ID);
            }
            Idols.Remove(idol);
        }

        public DelegateCommand<Discoverer> ConcernThisUserCommand { get; }
        private void ConcernThisUser(Discoverer discoverer)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.ConcernADiscoverer(
                    CurrentUser.BasicInfo.ID,
                    discoverer.BasicInfo.ID);
            }
            Idols.Add(discoverer);
        }

        public bool KeepAlive => false;
    }
}
