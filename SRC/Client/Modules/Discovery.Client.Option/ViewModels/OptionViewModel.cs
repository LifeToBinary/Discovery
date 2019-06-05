using Discovery.Core.Constants;
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

namespace Discovery.Client.Option.ViewModels
{
    public class OptionViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public Discoverer CurrentUser { get; }

        /// <summary>
        /// Regin 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public OptionViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            SignOutCommand = new DelegateCommand(SignOut);
            NavigationSignInViewToMainMenuContentRegionCommand =
                new DelegateCommand(NavigationSignInViewToMainMenuContentRegion);
        }

        /// <summary>
        /// 导航到 我的主页
        /// </summary>
        public DelegateCommand NavigationSignInViewToMainMenuContentRegionCommand {get;}
        private void NavigationSignInViewToMainMenuContentRegion()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);

        /// <summary>
        /// 导航离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

        /// <summary>
        /// 注销登录
        /// </summary>
        public DelegateCommand SignOutCommand { get; }
        public void SignOut()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignIn);
    }
}
