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
    public class FavoritedPostsViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private ObservableCollection<Post> _myFavorites;
        public ObservableCollection<Post> MyFavorites
        {
            get => _myFavorites;
            set => SetProperty(ref _myFavorites, value);
        }

        public FavoritedPostsViewModel()
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _myFavorites = new ObservableCollection<Post>(LoadData());
        }

        private Post[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetFavoritePosts(CurrentUser.BasicInfo.ID);
            }
        }
    }
}
