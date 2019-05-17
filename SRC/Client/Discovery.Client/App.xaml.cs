using Discovery.Client.DiscovererHomePage;
using Discovery.Client.MainMenu;
using Discovery.Client.MainMenuTitle;
using Discovery.Client.Option;
using Discovery.Client.Recommended;
using Discovery.Client.SignIn;
using Discovery.Client.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client
{
    /// <summary>
    /// App.xaml 的交互逻
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
            => moduleCatalog.AddModule<SignInModule>()
                            .AddModule<MainMenuModule>()
                            .AddModule<MainMenuTitleModule>()
                            .AddModule<DiscovererHomePageModule>()
                            .AddModule<RecommendedModule>()
                            .AddModule<OptionModule>();
    }
}
