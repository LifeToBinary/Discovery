﻿using Discovery.Client.SignIn.Views;
using Discovery.Core.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.SignIn
{
    public class SignInModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                             .RegisterViewWithRegion(RegionNames.MainRegion, typeof(Views.SignIn))
                             .RegisterViewWithRegion(RegionNames.SignUpRegion, typeof(Views.SignUpFirstStep));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.SignIn>();
            containerRegistry.RegisterForNavigation<SignUp>();
            containerRegistry.RegisterForNavigation<SignUpFirstStep>();
            containerRegistry.RegisterForNavigation<SignUpFinallyStep>();
        }
    }
}
