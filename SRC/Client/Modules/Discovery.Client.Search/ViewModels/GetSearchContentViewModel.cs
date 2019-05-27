using Discovery.Client.Search.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.Search.ViewModels
{
    public class GetSearchContentViewModel : BindableBase
    {
        private string _searchContent;
        public string SearchContent
        {
            get => _searchContent;
            set => SetProperty(ref _searchContent, value);
        }

        private SearchType _searchType;
        public SearchType SearchType
        {
            get => _searchType;
            set => SetProperty(ref _searchType, value);
        }
        private readonly IRegionManager _regionManager;
        public GetSearchContentViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SearchCommand = new DelegateCommand(Search);
        }
        public DelegateCommand SearchCommand { get; }
        private void Search()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (SearchType == SearchType.SearchUser)
                {
                    NavigateToSearchedDiscoverersResult(
                        databaseService.SearchDiscoverers(
                            SearchContent));
                }
                else
                {
                    NavigateToSearchedPostsResult(
                        databaseService.FindPostFromAllDiscoverers(
                            SearchContent));
                }
            }
        }
        private void NavigateToSearchedDiscoverersResult(IEnumerable<Discoverer> searchedDiscoverers)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.SearchedDiscoverersResult,
                new NavigationParameters
                {
                    { "Discoverers", searchedDiscoverers} 
                });

        private void NavigateToSearchedPostsResult(IEnumerable<Post> searchedPostsResult)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.SearchedPostsResult,
                new NavigationParameters
                {
                    { "Posts", searchedPostsResult} 
                });
    }
}
