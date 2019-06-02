using Discovery.Client.Search.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace Discovery.Client.Search.ViewModels
{
    public class GetSearchContentViewModel : BindableBase, IRegionMemberLifetime
    {
        private string _searchContent;
        public string SearchContent
        {
            get => _searchContent;
            set => SetProperty(ref _searchContent, value);
        }
        private ObservableCollection<Post> _searchedPosts;
        public ObservableCollection<Post> SearchedPosts
        {
            get => _searchedPosts;
            set => SetProperty(ref _searchedPosts, value);
        }
        private ObservableCollection<Discoverer> _searchedDiscoverers;
        public ObservableCollection<Discoverer> SearchedDiscoverers
        {
            get => _searchedDiscoverers;
            set => SetProperty(ref _searchedDiscoverers, value);
        }
        private readonly IRegionManager _regionManager;
        public GetSearchContentViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _searchedDiscoverers = new ObservableCollection<Discoverer>();
            _searchedPosts = new ObservableCollection<Post>();
            SearchCommand = new DelegateCommand<SearchType?>(Search);
            ViewPostDetailCommand = new DelegateCommand<Post>(ViewPostDetail);
            ViewDiscovererHomePageCommand = new DelegateCommand<Discoverer>(ViewDiscovererHomePage);
        }
        public bool KeepAlive => false;


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
        public DelegateCommand<Discoverer> ViewDiscovererHomePageCommand { get; }
        private void ViewDiscovererHomePage(Discoverer discoverer)
        {
            if (discoverer.BasicInfo.ID == GlobalObjectHolder.CurrentUser.BasicInfo.ID)
            {
                _regionManager.RequestNavigate(
                    RegionNames.MainMenuContent,
                    ViewNames.DiscovererHomePage);
                return;
            }
            _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer }
                });
        }
        public DelegateCommand<SearchType?> SearchCommand { get; }
        private async void Search(SearchType? searchType)
        {
            ClearData();
            using (var databaseService = new DataBaseServiceClient())
            {
                if (searchType == SearchType.SearchUser)
                {
                    foreach (Discoverer discoverer in await databaseService.SearchDiscoverersAsync(SearchContent))
                    {
                        _searchedDiscoverers.Add(discoverer);
                    }
                    return;
                }
                foreach (Post post in await databaseService.FindPostFromAllDiscoverersAsync(SearchContent))
                {
                    _searchedPosts.Add(post);
                }
            }
        }
        private void ClearData()
        {
            if (_searchedPosts.Any())
            {
                _searchedPosts.Clear();
            }
            if (_searchedDiscoverers.Any())
            {
                _searchedDiscoverers.Clear();
            }
        }
    }
}
