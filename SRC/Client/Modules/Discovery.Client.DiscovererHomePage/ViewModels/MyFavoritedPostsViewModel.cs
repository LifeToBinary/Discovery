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
    public class MyFavoritedPostsViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _myFavoritedPosts;
        public ObservableCollection<Post> MyFavoritedPosts
        {
            get => _myFavoritedPosts;
            set => SetProperty(ref _myFavoritedPosts, value);
        }

        public MyFavoritedPostsViewModel(IRegionManager regionManager)
        {
            MyFavoritedPosts = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewThisPostDetailCommand = new DelegateCommand<Post>(ViewThisPostDetail);
            LoadData();
        }
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in await databaseService.GetFavoritePostsAsync(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    MyFavoritedPosts.Add(post);
                }
            }
        }
        public DelegateCommand<Post> ViewThisPostDetailCommand { get; }
        private void ViewThisPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post}
                });
        public bool KeepAlive => false;
    }
}
