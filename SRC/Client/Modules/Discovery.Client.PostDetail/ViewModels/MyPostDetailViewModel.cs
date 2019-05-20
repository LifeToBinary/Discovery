using Discovery.Core.Model;
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

        public MyPostDetailViewModel()
        {
        }

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
