using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class MyPostDetailViewModel : BindableBase, INavigationAware
    {
        private Post _currentPost;
        public Post CurrentPost
        {
            get => _currentPost;
            set => SetProperty(ref _currentPost, value);
        }

        private readonly IRegionManager _regionManager;
        public MyPostDetailViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            UpdatePostCommand = new DelegateCommand(UpdatePost);
            RemoveThisPostCommand = new DelegateCommand(RemoveThisPost);
        }

        public DelegateCommand RemoveThisPostCommand { get; }
        private void RemoveThisPost()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.RemoveAPost(_currentPost.ID);
            }
            _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.Recommended);
        }
        
        public DelegateCommand UpdatePostCommand { get; }
        private void UpdatePost()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.UpdatePostInfo,
                new NavigationParameters
                {
                    { "Post", _currentPost}
                });

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                CurrentPost = post;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
