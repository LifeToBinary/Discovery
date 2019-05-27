using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Discovery.Client.Search.ViewModels
{
    public class SearchedPostsResultViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Post> _posts;
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }
        public SearchedPostsResultViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
        }

        public DelegateCommand<Post> ViewPostDetailCommand { get; }
        private void ViewPostDetail(Post post)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                post.AuthorID == GlobalObjectHolder.CurrentUser.BasicInfo.ID
                    ? ViewNames.MyPostDetail
                    : ViewNames.OtherUsersPostDetail,
                new NavigationParameters
                {
                    { "Post", post }
                });

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Posts"] is IEnumerable<Post> posts)
            {
                Posts = new ObservableCollection<Post>(posts);
            }
        }

        public bool IsNavigationTarget(
            NavigationContext _)
            => true;
        public void OnNavigatedFrom(
            NavigationContext _)
        {
        }
    }
}
