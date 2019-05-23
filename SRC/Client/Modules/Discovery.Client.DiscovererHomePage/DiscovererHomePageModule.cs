using Discovery.Client.DiscovererHomePage.Views;
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
            containerRegistry.RegisterForNavigation<MyHomePage>();
            containerRegistry.RegisterForNavigation<IPostedPosts>();
            containerRegistry.RegisterForNavigation<MyFavoritedPosts>();
            containerRegistry.RegisterForNavigation<MyConcern>();
            containerRegistry.RegisterForNavigation<MyFuns>();

            containerRegistry.RegisterForNavigation<OtherUsersHomePage>();
            containerRegistry.RegisterForNavigation<OtherUsersPostedPosts>();
            containerRegistry.RegisterForNavigation<OtherUsersFavoritedPosts>();
            containerRegistry.RegisterForNavigation<OtherUsersConcern>();
            containerRegistry.RegisterForNavigation<OtherUsersFuns>();
        }
    }
}
