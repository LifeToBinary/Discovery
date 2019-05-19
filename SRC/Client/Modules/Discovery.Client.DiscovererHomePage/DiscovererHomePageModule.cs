using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Discovery.Client.DiscovererHomePage
{
    public class DiscovererHomePageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.DiscovererHomePage>();
            containerRegistry.RegisterForNavigation<Views.PostedPosts>();
            containerRegistry.RegisterForNavigation<Views.FavoritedPosts>();
            containerRegistry.RegisterForNavigation<Views.MyConcern>();
            containerRegistry.RegisterForNavigation<Views.MyFuns>();
        }
    }
}
