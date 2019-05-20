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
    public class OtherUsersPostedPostsViewModel : BindableBase, INavigationAware
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

        public OtherUsersPostedPostsViewModel(IRegionManager regionManager)
        {
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
            _regionManager = regionManager;
        }
        private Post[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetPostsOfTheDiscoverer(Discoverer.BasicInfo.ID);
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
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"] is Discoverer discoverer)
            {
                Discoverer = discoverer;
                PostedPosts = new ObservableCollection<Post>(LoadData());
            }
        }

    }
}
