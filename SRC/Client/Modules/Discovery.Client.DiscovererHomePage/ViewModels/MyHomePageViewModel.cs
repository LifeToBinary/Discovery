using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyHomePageViewModel : BindableBase, IRegionMemberLifetime
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
        public MyHomePageViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            NavigateViewToMainMenuRegionCommand = new DelegateCommand<string>(NavigateViewToMainMenuRegion);
        }

        /// <summary>
        /// 注入一个视图到 MainMenuContent Region 中
        /// </summary>
        public DelegateCommand<string> NavigateViewToMainMenuRegionCommand { get; }
        private void NavigateViewToMainMenuRegion(string viewName)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                viewName);

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
