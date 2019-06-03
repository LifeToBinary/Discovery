using System;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using Microsoft.Win32;
using Discovery.Service;
using Discovery.Core.Model;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Client.DiscovererHomePage.DataBaseService;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class UpdateDiscovererInfoViewModel : BindableBase, IRegionMemberLifetime
    {
        public Discoverer CurrentUser { get; }
        private readonly IRegionManager _regionManager;
        private string _localAvatarPathWillToUpload;
        private string _localProfileBackgroundWillToUpload;
        public UpdateDiscovererInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CurrentUser = GlobalObjectHolder.CurrentUser;
            SaveUpdateCommand = new DelegateCommand(SaveUpdate);
            UpdateAvatarCommand = new DelegateCommand(UpdateAvatar);
            UpdateProfileBackgroundImageCommand =
                new DelegateCommand(UpdateProfileBackground);
            InitCurrentUser();
        }
        private bool _isUpdating = false;
        public bool IsUpdating
        {
            get => _isUpdating;
            set => SetProperty(ref _isUpdating, value);
        }
        private void InitCurrentUser()
        {
            CurrentUser.ContactInfo.BlogAddress = CurrentUser.ContactInfo.BlogAddress ?? String.Empty;
            CurrentUser.ContactInfo.QQ = CurrentUser.ContactInfo.QQ ?? String.Empty;
            CurrentUser.ContactInfo.WeChat = CurrentUser.ContactInfo.WeChat ?? String.Empty;
        }

        public DelegateCommand SaveUpdateCommand { get; }
        private async void SaveUpdate()
        {
            IsUpdating = true;
            UploadAvatarAndProfileBackgroundImage();
            using (var databaseService = new DataBaseServiceClient())
            {
                await databaseService.UpdateDiscovererInfoAsync(CurrentUser);
            }
            IsUpdating = false;
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
        public DelegateCommand UpdateProfileBackgroundImageCommand { get; }
        public bool KeepAlive => false;

        private void UpdateProfileBackground()
        {
            var profileBackgroundImageSelector = new OpenFileDialog();
            if (profileBackgroundImageSelector.ShowDialog() == true)
            {
                _localProfileBackgroundWillToUpload = profileBackgroundImageSelector.FileName;
                CurrentUser.BasicInfo.ProfileBackgroundImagePath = profileBackgroundImageSelector.FileName;
            }
        }
        private void UploadAvatarAndProfileBackgroundImage()
        {
            if (_localAvatarPathWillToUpload is null &&
                _localProfileBackgroundWillToUpload is null)
            {
                return;
            }
            string ftpImageFileBasePath = @"ftp://47.240.12.27/Discovery/DiscoveryWebFiles/Discoverer/Images/";
            string userBasePath = $"{ftpImageFileBasePath}{CurrentUser.BasicInfo.SignInName}";
            string userAvatarPath = $"{userBasePath}/Avatar.jpg";
            string userProfileBackgroundImagePath = $"{userBasePath}/ProfileBackground.jpg";
            var webFileService = new WebFileService(ftpImageFileBasePath);
            //webFileService.CreateDirectoryIfNotExistWithRecurtion($"{CurrentUser.BasicInfo.SignInName}/Post");
            if (_localAvatarPathWillToUpload != null)
            {
                webFileService.Upload(
                    _localAvatarPathWillToUpload,
                    userAvatarPath);
            }
            if (_localProfileBackgroundWillToUpload != null)
            {
                webFileService.Upload(
                    _localProfileBackgroundWillToUpload,
                    userProfileBackgroundImagePath);
            }
            CurrentUser.BasicInfo.AvatarPath = $"{userAvatarPath.Replace("ftp://47.240.12.27", "http://47.240.12.27:10003")}?{Guid.NewGuid()}";
            CurrentUser.BasicInfo.ProfileBackgroundImagePath = $"{userProfileBackgroundImagePath.Replace("ftp://47.240.12.27", "http://47.240.12.27:10003")}?{Guid.NewGuid()}";
        }
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
    }
}
