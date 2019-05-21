using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
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
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _postedPost;
        public ObservableCollection<Post> PostedPosts
        {
            get => _postedPost;
            set => SetProperty(ref _postedPost, value);
        }
        private ObservableCollection<Discoverer> _funs;
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        private ObservableCollection<Post> _favoritedPost;
        public ObservableCollection<Post> FavoritedPosts
        {
            get => _favoritedPost;
            set => SetProperty(ref _favoritedPost, value);
        }
        private ObservableCollection<Discoverer> _idols;
        public ObservableCollection<Discoverer> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        public OtherUsersHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
            NavigationToProfileContentRegionCommand =
                new DelegateCommand<string>(NavigationToProfileContentRegion);
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
                                  ViewNames.OtherUsersHomePage,
                                  new NavigationParameters
                                  {
                                        { "Discoverer", discoverer }
                                  });
        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        public bool KeepAlive => false;

        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent,
                                  ViewNames.OtherUsersPostDetail,
                                  new NavigationParameters
                                  {
                                      { "Post", post }
                                  });
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }
        private void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                PostedPosts = new ObservableCollection<Post>(
                                  databaseService.GetPostsOfTheDiscoverer(
                                      Discoverer.BasicInfo.ID));

                Funs = new ObservableCollection<Discoverer>(
                           databaseService.GetFunsOfTheIdol(
                               Discoverer.BasicInfo.ID));

                FavoritedPosts = new ObservableCollection<Post>(
                                     databaseService.GetFavoritePosts(
                                         Discoverer.BasicInfo.ID));
                Idols = new ObservableCollection<Discoverer>(
                            databaseService.GetIdols(
                                Discoverer.BasicInfo.ID));
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
