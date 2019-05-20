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
    public class MyFunsViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private IRegionManager _regionManager;
        private ObservableCollection<Discoverer> _funs;
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public MyFunsViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            Funs = new ObservableCollection<Discoverer>(LoadData());
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(ViewThisUsersHomePage);
            _regionManager = regionManager;
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetFunsOfTheIdol(CurrentUser.BasicInfo.ID);
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
