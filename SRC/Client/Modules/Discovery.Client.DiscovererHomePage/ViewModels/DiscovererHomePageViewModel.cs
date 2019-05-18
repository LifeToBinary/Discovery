﻿using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class DiscovererHomePageViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        public DiscovererHomePageViewModel()
            => CurrentUser = GlobalObjectHolder.CurrentUser;
    }
}
