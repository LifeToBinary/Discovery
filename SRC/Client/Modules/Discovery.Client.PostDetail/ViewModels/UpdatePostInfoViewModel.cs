using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
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
    public class UpdatePostInfoViewModel : BindableBase, INavigationAware
    {
        private Post _post;
        public Post Post
        {
            get => _post;
            set => SetProperty(ref _post, value);
        }
        private readonly IRegionManager _regionManager;
        public UpdatePostInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SaveUpdateCommand = new DelegateCommand(SaveUpdate);
        }
        public DelegateCommand SaveUpdateCommand { get; }
        private void SaveUpdate()
        {
            Post.LastEditedTime = DateTime.Now;
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.UpdatePostInfo(Post);
            }
            NavigateToMyHomePage();
        }

        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);

        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                Post = post;
            }
        }

        public bool IsNavigationTarget(
            NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(
            NavigationContext navigationContext) { }
    }
}
