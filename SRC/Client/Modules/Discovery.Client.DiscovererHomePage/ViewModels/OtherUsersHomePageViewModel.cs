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
        #region 字段
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _postedPost;
        private ObservableCollection<Discoverer> _funs;
        private ObservableCollection<Post> _favoritedPost;
        private ObservableCollection<Discoverer> _idols;
        private Discoverer _discoverer;
        #endregion

        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        public ObservableCollection<Post> PostedPosts
        {
            get => _postedPost;
            set => SetProperty(ref _postedPost, value);
        }
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public ObservableCollection<Post> FavoritedPosts
        {
            get => _favoritedPost;
            set => SetProperty(ref _favoritedPost, value);
        }
        public ObservableCollection<Discoverer> Idols
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
            using (var databaseService = new DataBaseServiceClient())
            {
                PostedPosts = new ObservableCollection<Post>(
                                  await databaseService.GetPostsOfTheDiscovererAsync(
                                      Discoverer.BasicInfo.ID));

                Funs = new ObservableCollection<Discoverer>(
                           await databaseService.GetFunsOfTheIdolAsync(
                               Discoverer.BasicInfo.ID));

                FavoritedPosts = new ObservableCollection<Post>(
                                     await databaseService.GetFavoritePostsAsync(
                                         Discoverer.BasicInfo.ID));
                Idols = new ObservableCollection<Discoverer>(
                            await databaseService.GetIdolsAsync(
                                Discoverer.BasicInfo.ID));
            }
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
