using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
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
    public class MyConcernViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private ObservableCollection<Discoverer> _myConcerns;
        public ObservableCollection<Discoverer> MyConcerns
        {
            get => _myConcerns;
            set => SetProperty(ref _myConcerns, value);
        }
        public MyConcernViewModel(IRegionManager regionManager)
        {
            MyConcerns = new ObservableCollection<Discoverer>();
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(
                ViewThisUsersHomePage);
            CancelConcernCommand = new DelegateCommand<Discoverer>(CancelConcern);
            LoadData();
        }
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Discoverer discoverer in await databaseService.GetIdolsAsync(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    MyConcerns.Add(discoverer);
                };
            }
        }
        public DelegateCommand<Discoverer> CancelConcernCommand { get; }
        private void CancelConcern(Discoverer discoverer)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelConcern(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                    discoverer.BasicInfo.ID);
            }
            MyConcerns.Remove(discoverer);
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        public bool KeepAlive => false;

        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer}
                });
    }
}
