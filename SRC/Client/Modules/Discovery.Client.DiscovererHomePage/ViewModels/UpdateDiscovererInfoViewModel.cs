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
using System.IO;

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
        private void UpdateProfileBackground()
        {
            var profileBackgroundImageSelector = new OpenFileDialog();
            if (profileBackgroundImageSelector.ShowDialog() == true)
            {
                _localProfileBackgroundWillToUpload = profileBackgroundImageSelector.FileName;
                CurrentUser.BasicInfo.ProfileBackgroundImagePath = profileBackgroundImageSelector.FileName;
            }
        }

        public bool KeepAlive => false;
        private async void SaveUpdate()
        {
            IsUpdating = true;
            string userAvatarPath = $"{CurrentUser.BasicInfo.SignInName}/Avatar.jpg";
            string userProfileBackgroundImagePath = $"{CurrentUser.BasicInfo.SignInName}/ProfileBackground.jpg";

            using (var databaseService = new DataBaseServiceClient())
            {
                if (_localAvatarPathWillToUpload != null)
                {
                    byte[] avatarData = File.ReadAllBytes(_localAvatarPathWillToUpload);
                    await databaseService.UploadFileAsync(avatarData, userAvatarPath);
                    CurrentUser.BasicInfo.AvatarPath = $"http://47.240.12.27:10003/Discovery/DiscoveryWebFiles/Discoverer/Images/{userAvatarPath}?{Guid.NewGuid()}";
                }
                if (_localProfileBackgroundWillToUpload != null)
                {
                    byte[] profileBackgroundData = File.ReadAllBytes(_localProfileBackgroundWillToUpload);
                    await databaseService.UploadFileAsync(profileBackgroundData, userProfileBackgroundImagePath);
                    CurrentUser.BasicInfo.ProfileBackgroundImagePath = $"http://47.240.12.27:10003/Discovery/DiscoveryWebFiles/Discoverer/Images/{userProfileBackgroundImagePath}?{Guid.NewGuid()}";
                }
                await databaseService.UpdateDiscovererInfoAsync(CurrentUser);
            }
            IsUpdating = false;
            NavigateToMyHomePage();
        }
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
    }
}
