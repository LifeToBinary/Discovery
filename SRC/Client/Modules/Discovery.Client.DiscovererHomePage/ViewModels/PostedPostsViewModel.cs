using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Mvvm;
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
        private ObservableCollection<Post> _postsIPost;
        public ObservableCollection<Post> PostsIPost
        {
            get => _postsIPost;
            set => SetProperty(ref _postsIPost, value);
        }

        public PostedPostsViewModel()
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _postsIPost = new ObservableCollection<Post>(LoadData());
        }

        private Post[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetPostsOfTheDiscoverer(CurrentUser.BasicInfo.ID);
            }
        }
    }
}
