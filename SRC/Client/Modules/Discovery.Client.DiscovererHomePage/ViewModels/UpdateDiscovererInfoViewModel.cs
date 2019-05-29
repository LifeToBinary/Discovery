using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Service;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class UpdateDiscovererInfoViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        private string _localAvatarPathWillToUpload;
        public UpdateDiscovererInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            SaveUpdateCommand = new DelegateCommand(SaveUpdate);
            UpdateAvatarCommand = new DelegateCommand(UpdateAvatar);
        }

        public DelegateCommand SaveUpdateCommand { get; }
        private void SaveUpdate()
        {
            CurrentUser.BasicInfo.AvatarPath = $"{UploadAvatar()}?{Guid.NewGuid()}";
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.UpdateDiscovererInfo(CurrentUser);
            }
            NavigateToMyHomePage();
        }
        public DelegateCommand UpdateAvatarCommand { get; }
        private void UpdateAvatar()
        {
            var avatarSelector = new OpenFileDialog();
            if (avatarSelector.ShowDialog() == true)
            {
                _localAvatarPathWillToUpload = avatarSelector.FileName;
                CurrentUser.BasicInfo.AvatarPath = avatarSelector.FileName;
            }
        }
        private string UploadAvatar()
        {
            string ftpImageFileBasePath = @"ftp://47.240.12.27/Discovery/DiscoveryWebFiles/Discoverer/Images/";

            string userBasePath = $"{ftpImageFileBasePath}{CurrentUser.BasicInfo.SignInName}";
            string userAvatarPath = $"{userBasePath}/Avatar.jpg";
            var webFileService = new WebFileService(ftpImageFileBasePath);
            webFileService.CreateDirectoryIfNotExistWithRecurtion($"{CurrentUser.BasicInfo.SignInName}/Post");
            webFileService.Upload(
                _localAvatarPathWillToUpload,
                userAvatarPath);
            return userAvatarPath.Replace("ftp://47.240.12.27", "http://47.240.12.27:10003");
        }
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
    }
}
