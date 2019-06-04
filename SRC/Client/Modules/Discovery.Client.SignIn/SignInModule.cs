using Discovery.Client.SignIn.Views;
using Discovery.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Discovery.Client.SignIn
{
    public class SignInModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                             .RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.SignIn));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.SignIn>();
            containerRegistry.RegisterForNavigation<SignUp>();
        }
    }
}
