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
    public class MyConcernViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private IRegionManager _regionManager;
        private ObservableCollection<Discoverer> _idols;
        public ObservableCollection<Discoverer> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        public MyConcernViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _idols = new ObservableCollection<Discoverer>(LoadData());
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetIdols(CurrentUser.BasicInfo.ID);
            }
        }
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                                  RegionNames.MainMenuContent, 
                                  ViewNames.OtherUsersHomePage,
                                  new NavigationParameters
                                  {
                                      { "Discoverer", discoverer }
                                  });
    }
}
