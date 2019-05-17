using Discovery.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.MainMenuTitle
{
    public class MainMenuTitleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
            => containerProvider.Resolve<IRegionManager>()
                                .RegisterViewWithRegion(RegionNames.MainMenuTitle, typeof(Views.MainMenuTitle));

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
