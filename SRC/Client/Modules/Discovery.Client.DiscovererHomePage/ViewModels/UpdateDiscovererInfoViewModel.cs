using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class UpdateDiscovererInfoViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        public UpdateDiscovererInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            SaveUpdateCommand = new DelegateCommand(SaveUpdate);
        }
        public DelegateCommand SaveUpdateCommand { get; }
        private void SaveUpdate()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.UpdateDiscovererInfo(CurrentUser);
            }
            NavigateToMyHomePage();
        }
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
    }
}
