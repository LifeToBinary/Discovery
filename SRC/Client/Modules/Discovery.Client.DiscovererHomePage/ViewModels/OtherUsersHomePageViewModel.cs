using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.Models;
using Discovery.Core.RelationalModel;
using Prism.Commands;
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
    public class OtherUsersHomePageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        #region 字段
        private readonly IRegionManager _regionManager;
        private ObservableCollection<PostRelationalModel> _postedPost;
        private ObservableCollection<DiscovererRelationshipModel> _funs;
        private ObservableCollection<PostRelationalModel> _favoritedPost;
        private ObservableCollection<DiscovererRelationshipModel> _idols;
        private Discoverer _discoverer;
        private bool _isConcerned;
        #endregion

        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        public ObservableCollection<PostRelationalModel> PostedPosts
        {
            get => _postedPost;
            set => SetProperty(ref _postedPost, value);
        }
        public ObservableCollection<DiscovererRelationshipModel> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public ObservableCollection<PostRelationalModel> FavoritedPosts
        {
            get => _favoritedPost;
            set => SetProperty(ref _favoritedPost, value);
        }

        public ObservableCollection<DiscovererRelationshipModel> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }

        public OtherUsersHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            ViewThisUsersHomePageCommand =
                new DelegateCommand<Discoverer>(
                    ViewThisUsersHomePage);

            ViewPostDetailCommand =
                new DelegateCommand<Post>(ViewPostDetail);

            NavigationToProfileContentRegionCommand =
                new DelegateCommand<string>(
                    NavigationToProfileContentRegion);
            ConcernOrCancelConcernCommand = new DelegateCommand(ConcernOrCancelConcern);
        }

        public bool IsConcerned
        {
            get => _isConcerned;
            set => SetProperty(ref _isConcerned, value);
        }

        public DelegateCommand ConcernOrCancelConcernCommand { get; }
        private void ConcernOrCancelConcern()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (_isConcerned)
                {
                    databaseService.CancelConcern(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcerned = false;
                }
                else
                {
                    databaseService.ConcernADiscoverer(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcerned = true;
                }
            }
        }

        public DelegateCommand<string> NavigationToProfileContentRegionCommand { get; }
        private void NavigationToProfileContentRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.OtherUsersProfileContent,
                viewName,
                new NavigationParameters
                {
                    { "Discoverer", Discoverer }
                });

        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                discoverer.BasicInfo.ID == GlobalObjectHolder.CurrentUser.BasicInfo.ID
                    ? ViewNames.DiscovererHomePage
                    : ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });

        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        private async void LoadData()
        {
            KeyValuePair<Post, bool>[] thisUsersRelationshipWithAnotherUsersPostedPosts;
            KeyValuePair<Discoverer, bool>[] thisUsersRelationshipWithAnotherUsersFuns;
            KeyValuePair<Post, bool>[] thisUsersRelationshipWithAnotherUsersFavoritedPosts;
            KeyValuePair<Discoverer, bool>[] thisUsersRelationshipWithAnotherUsersIdols;

            using (var databaseService = new DataBaseServiceClient())
            {
                thisUsersRelationshipWithAnotherUsersPostedPosts =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersPostedPostsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);

                thisUsersRelationshipWithAnotherUsersFuns =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersFunsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);

                thisUsersRelationshipWithAnotherUsersFavoritedPosts =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersFavoritedPostsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);

                thisUsersRelationshipWithAnotherUsersIdols =
                    await databaseService.ThisUsersRelationshipWithAnotherUsersIdolsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);

                IsConcerned = databaseService.IsFuns(
                                  GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                                  Discoverer.BasicInfo.ID);
            }

            PostedPosts = new ObservableCollection<PostRelationalModel>(
                              thisUsersRelationshipWithAnotherUsersPostedPosts.Select(
                                  item => new PostRelationalModel(
                                              item.Key,
                                              item.Value)));

            Funs = new ObservableCollection<DiscovererRelationshipModel>(
                       thisUsersRelationshipWithAnotherUsersFuns.Select(
                           item => new DiscovererRelationshipModel(
                                       item.Key,
                                       item.Value)));

            FavoritedPosts = new ObservableCollection<PostRelationalModel>(
                                 thisUsersRelationshipWithAnotherUsersFavoritedPosts.Select(
                                     item => new PostRelationalModel(
                                                 item.Key,
                                                 item.Value)));

            Idols = new ObservableCollection<DiscovererRelationshipModel>(
                        thisUsersRelationshipWithAnotherUsersIdols.Select(
                            item => new DiscovererRelationshipModel(
                                        item.Key,
                                        item.Value)));
        }

        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"]
                is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }

        public bool IsNavigationTarget(
            NavigationContext _)
            => true;

        public void OnNavigatedFrom(
            NavigationContext _)
        {
        }

        public bool KeepAlive => false;
    }
}
