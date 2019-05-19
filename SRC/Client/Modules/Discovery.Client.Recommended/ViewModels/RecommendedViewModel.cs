using Discovery.Client.Recommended.DataBaseService;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Mvvm;
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
        private ObservableCollection<Post> _recommendedPosts;
        public ObservableCollection<Post> RecommendedPosts
        {
            get => _recommendedPosts;
            set => SetProperty(ref _recommendedPosts, value);
        }
        public RecommendedViewModel()
        {
            _recommendedPosts = new ObservableCollection<Post>(LoadData());
        }
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
