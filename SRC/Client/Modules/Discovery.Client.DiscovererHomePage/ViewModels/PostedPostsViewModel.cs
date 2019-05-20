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
    public class PostedPostsViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private IRegionManager _regionManager;
        private ObservableCollection<Post> _postsIPost;
        public ObservableCollection<Post> PostsIPost
        {
            get => _postsIPost;
            set => SetProperty(ref _postsIPost, value);
        }
        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        public PostedPostsViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _postsIPost = new ObservableCollection<Post>(LoadData());
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
        }

        private Post[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetPostsOfTheDiscoverer(CurrentUser.BasicInfo.ID);
            }
        }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent, 
                                  ViewNames.MyPostDetail,
                                  new NavigationParameters
                                  {
                                      { "Post", post }
                                  });

    }
}
