using Discovery.Client.Recommended.DataBaseService;
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

namespace Discovery.Client.Recommended.ViewModels
{
    public class RecommendedViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private ObservableCollection<Post> _recommendedPosts;
        public ObservableCollection<Post> RecommendedPosts
        {
            get => _recommendedPosts;
            set => SetProperty(ref _recommendedPosts, value);
        }
        public RecommendedViewModel(IRegionManager regionManager)
        {
            _recommendedPosts = new ObservableCollection<Post>(LoadData());
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
        }

        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent,
                                  ViewNames.OtherUsersPostDetail,
                                  new NavigationParameters
                                  {
                                      { "Post", post }
                                  });

        private Post[] LoadData()
        {
            int currentUserID = GlobalObjectHolder.CurrentUser.BasicInfo.ID;
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetRecommendedForTheDiscoverer(currentUserID);
            }
        }
    }
}
