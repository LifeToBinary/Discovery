using Prism.Mvvm;
using Prism.Regions;
using Discovery.Core.RelationalModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discovery.Core.Constants;
using Discovery.Core.Model;
using Prism.Commands;
using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.GlobalData;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersPostedPostsViewModel
        : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        private ObservableCollection<Post> _postedPosts;
        public ObservableCollection<Post> PostedPosts
        {
            get => _postedPosts;
            set => SetProperty(ref _postedPosts, value);
        }
        public OtherUsersPostedPostsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewThisPostDetailCommand = new DelegateCommand<Post>(ViewThisPostDetail);
            PostedPosts = new ObservableCollection<Post>();
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
                foreach (Post post in await databaseService.GetPostsOfTheDiscovererAsync(
                    Discoverer.BasicInfo.ID))
                {
                    PostedPosts.Add(post);
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
