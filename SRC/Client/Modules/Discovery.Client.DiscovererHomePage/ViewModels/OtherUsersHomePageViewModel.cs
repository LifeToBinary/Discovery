using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.Models;
using Discovery.Core.RelationalModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class OtherUsersHomePageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }
        public DelegateCommand<string> NavigateViewToMainMenuContentRegionCommand { get; }
        private void NavigateViewToMainMenuContentRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                viewName,
                new NavigationParameters
                {
                    { "Discoverer", Discoverer }
                });
        private bool _isConcernedThisUser;
        public bool IsConcernedThisUser
        {
            get => _isConcernedThisUser;
            set => SetProperty(ref _isConcernedThisUser, value);
        }
        public OtherUsersHomePageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateViewToMainMenuContentRegionCommand =
                new DelegateCommand<string>(NavigateViewToMainMenuContentRegion);
            ConcernOrCancelConcernCommand = new DelegateCommand(ConcernOrCancelConcern);
        }
        public DelegateCommand ConcernOrCancelConcernCommand { get; }
        private void ConcernOrCancelConcern()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (_isConcerned)
                {
                    databaseService.CancelConcern(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcernedThisUser = false;
                }
                else
                {
                    databaseService.ConcernADiscoverer(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsConcernedThisUser = true;
                }
            }
        }
        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Discoverer"]
                is Discoverer discoverer)
            {
                Discoverer = discoverer;
                LoadData();
            }
        }
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                IsConcernedThisUser = 
                    await databaseService.IsFunsAsync(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
            }
        }
        public bool IsNavigationTarget(
            NavigationContext _)
            => true;
        public void OnNavigatedFrom(
            NavigationContext _)
        {
        }
        public bool KeepAlive => false;
    }
}
