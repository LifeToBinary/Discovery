using Discovery.Core.Enums;
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

        }

    }
}
