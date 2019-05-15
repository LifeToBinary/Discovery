using Discovery.Client.Views;
using Prism.Unity;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;

namespace Discovery.Client
{
    /// <summary>
    /// 应用程序启动类
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 设置启动窗口
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
            => Container.Resolve<MainWindow>();

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

        }
    }
}
