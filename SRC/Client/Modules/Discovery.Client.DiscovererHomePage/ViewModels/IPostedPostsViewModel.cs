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
    public class IPostedPostsViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _postedIPost;
        public ObservableCollection<Post> PostedIPost
        {
            get => _postedIPost;
            set => SetProperty(ref _postedIPost, value);
        }
        public IPostedPostsViewModel(IRegionManager regionManager)
        {
            PostedIPost = new ObservableCollection<Post>();
            _regionManager = regionManager;
            ViewMyPostDetailCommand = new DelegateCommand<Post>(ViewMyPostDetail);
            LoadData();
        }
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Post post in 
                    await databaseService.GetPostsOfTheDiscovererAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    PostedIPost.Add(post);
                }
            }
        }
        public bool KeepAlive => false;

        public DelegateCommand<Post> ViewMyPostDetailCommand { get; }
        private void ViewMyPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.MyPostDetail,
                new NavigationParameters
                {
                    { "Post", post}
                });
    }
}
