using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.MainMenu.ViewModels
{
    public class MainMenuViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public Discoverer CurrentUser { get; }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public MainMenuViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            NavigationToMainMenuContentRegionCommand =
                new DelegateCommand<string>(NavigationViewToMainMenuContentRegion);
            NavigationViewToMainRegionCommand =
                new DelegateCommand<string>(NavigationViewToMainRegion);
        }

        /// <summary>
        /// 导航视图到 MainMenuContent Region 中
        /// </summary>
        public DelegateCommand<string> NavigationToMainMenuContentRegionCommand { get; }
        private void NavigationViewToMainMenuContentRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.MainMenuContent, viewName);

        /// <summary>
        /// 导航视图到 MainRegion 中
        /// </summary>
        public DelegateCommand<string> NavigationViewToMainRegionCommand { get; }
        private void NavigationViewToMainRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.MainRegion, viewName);

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
