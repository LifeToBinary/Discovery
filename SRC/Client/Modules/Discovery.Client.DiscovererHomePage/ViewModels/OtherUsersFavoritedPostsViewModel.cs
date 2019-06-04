using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
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
    class OtherUsersFavoritedPostsViewModel 
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private ObservableCollection<Post> _favoritedPosts;
        public ObservableCollection<Post> FavoritedPosts
        {
            get => _favoritedPosts;
            set => SetProperty(ref _favoritedPosts, value);
        }
        private readonly IRegionManager _regionManager;
        public OtherUsersFavoritedPostsViewModel(IRegionManager regionManager)
        {
            FavoritedPosts = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewThisPostDetailCommand = 
                new DelegateCommand<Post>(ViewThisPostDetail);
        }
        public DelegateCommand<Post> ViewThisPostDetailCommand { get; }
        private void ViewThisPostDetail(Post post)
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
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in await databaseService.GetFavoritePostsAsync(
                    Discoverer.BasicInfo.ID))
                {
                    FavoritedPosts.Add(post);
                }
            }
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        public bool KeepAlive => false;
    }
}
