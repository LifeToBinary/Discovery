﻿using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.MainMenu.ViewModels
{
    public class MainMenuViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        public MainMenuViewModel(IRegionManager regionManager)
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _regionManager = regionManager;
            NavigationToMainMenuContentRegionCommand =
                new DelegateCommand<string>(NavigationViewToMainMenuContentRegion);
            NavigationViewToMainRegionCommand =
                new DelegateCommand<string>(NavigationViewToMainRegion);
            AddNewPostCommand = new DelegateCommand(AddNewPost);
        }

        public DelegateCommand AddNewPostCommand { get; }
        private void AddNewPost()
            => _regionManager.RequestNavigate(RegionNames.MainMenuContent, ViewNames.NewPost);
        public DelegateCommand<string> NavigationToMainMenuContentRegionCommand { get; }
        private void NavigationViewToMainMenuContentRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.MainMenuContent, viewName);

        public DelegateCommand<string> NavigationViewToMainRegionCommand { get; }
        public bool KeepAlive => false;

        private void NavigationViewToMainRegion(string viewName)
            => _regionManager.RequestNavigate(RegionNames.MainRegion, viewName);
    }
}
