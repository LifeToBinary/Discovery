using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Diagnostics;

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
            OpenLinkInBroswerCommand = new DelegateCommand<string>(OpenLinkInBroswer);
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
        /// 使用默认浏览器程序打开链接
        /// </summary>
        public DelegateCommand<string> OpenLinkInBroswerCommand { get; }
        private void OpenLinkInBroswer(string link) => Process.Start(link);

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
