using Discovery.Client.About;
using Discovery.Client.DiscovererHomePage;
using Discovery.Client.Feedback;
using Discovery.Client.MainMenu;
using Discovery.Client.Option;
using Discovery.Client.PostDetail;
using Discovery.Client.Recommended;
using Discovery.Client.Resource.Themes;
using Discovery.Client.Search;
using Discovery.Client.SignIn;
using Discovery.Client.Theme;
using Discovery.Client.Views;
using Discovery.Core.GlobalData;
using Discovery.Service;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            AppFileHelper.ValidateApplicationFiles();
            Core.Enums.Theme userTheme = AppFileHelper.ReadUserThemeSettings();
            GlobalObjectHolder.CurrentTheme = userTheme;
            Resources.MergedDictionaries[0] = GetThemeResource(AppFileHelper.ReadUserThemeSettings());
            base.OnStartup(e);
        }

        private ResourceDictionary GetThemeResource(Core.Enums.Theme theme)
            => new[]
            {
                DefaultTheme.Instance as ResourceDictionary,
                DarkMagentaTheme.Instance as ResourceDictionary,
                PinkTheme.Instance as ResourceDictionary
            }[(int)theme];

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
            => moduleCatalog.AddModule<SignInModule>()
                            .AddModule<MainMenuModule>()
                            .AddModule<DiscovererHomePageModule>()
                            .AddModule<RecommendedModule>()
                            .AddModule<OptionModule>()
                            .AddModule<FeedbackModule>()
                            .AddModule<AboutModule>()
                            .AddModule<PostDetailModule>()
                            .AddModule<SearchModule>()
                            .AddModule<ThemeModule>();
    }
}
