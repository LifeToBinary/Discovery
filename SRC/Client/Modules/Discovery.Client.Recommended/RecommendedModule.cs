using Discovery.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.Recommended
{
    public class RecommendedModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
            => containerProvider.Resolve<IRegionManager>()
                                .RegisterViewWithRegion(RegionNames.MainMenuContent, typeof(Views.Recommended));
        public void RegisterTypes(IContainerRegistry containerRegistry)
            => containerRegistry.RegisterForNavigation<Views.Recommended>();
    }
}
