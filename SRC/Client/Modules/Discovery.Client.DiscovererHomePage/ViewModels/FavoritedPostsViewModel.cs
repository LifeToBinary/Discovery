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
    public class FavoritedPostsViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private IRegionManager _regionManager;
        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private ObservableCollection<Post> _myFavorites;
        public ObservableCollection<Post> MyFavorites
        {
            get => _myFavorites;
            set => SetProperty(ref _myFavorites, value);
        }

        public FavoritedPostsViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _myFavorites = new ObservableCollection<Post>(LoadData());
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
        }

        private Post[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetFavoritePosts(CurrentUser.BasicInfo.ID);
            }
        }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent,
                                  ViewNames.OtherUsersPostDetail,
                                  new NavigationParameters
                                  {
                                      { "Post", post }
                                  });
    }
}
