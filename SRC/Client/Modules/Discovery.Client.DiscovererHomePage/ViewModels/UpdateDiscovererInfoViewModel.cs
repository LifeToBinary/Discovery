using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.IO;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class UpdateDiscovererInfoViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public Discoverer CurrentUser { get; }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 用于更新头像的本地图像文件路径
        /// </summary>
        private string _localAvatarPathWillToUpload;

        /// <summary>
        /// 用于更新背景(封面)的本地图像文件路径
        /// </summary>
        private string _localProfileBackgroundWillToUpload;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
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

        /// <summary>
        /// 正在更新中
        /// </summary>
        private bool _isUpdating = false;
        public bool IsUpdating
        {
            get => _isUpdating;
            set => SetProperty(ref _isUpdating, value);
        }

        /// <summary>
        /// 对当前用户 null 属性值的处理
        /// </summary>
        private void InitCurrentUser()
        {
            CurrentUser.ContactInfo.BlogAddress = CurrentUser.ContactInfo.BlogAddress ?? String.Empty;
            CurrentUser.ContactInfo.QQ = CurrentUser.ContactInfo.QQ ?? String.Empty;
            CurrentUser.ContactInfo.WeChat = CurrentUser.ContactInfo.WeChat ?? String.Empty;
        }

        /// <summary>
        /// 选择本地文件以更新头像
        /// </summary>
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

        /// <summary>
        /// 选择本地文件以更新背景(封面图片)
        /// </summary>
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

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

        /// <summary>
        /// 更新个人资料
        /// </summary>
        public DelegateCommand SaveUpdateCommand { get; }
        private async void SaveUpdate()
        {
            IsUpdating = true;
            string userAvatarPath = $"{CurrentUser.BasicInfo.SignInName}/Avatar.jpg";
            string userProfileBackgroundImagePath = $"{CurrentUser.BasicInfo.SignInName}/ProfileBackground.jpg";

            using (var databaseService = new DataBaseServiceClient())
            {
                // 如果选择了新头像文件, 则上传此文件
                if (_localAvatarPathWillToUpload != null)
                {
                    byte[] avatarData = File.ReadAllBytes(_localAvatarPathWillToUpload);
                    await databaseService.UploadFileAsync(avatarData, userAvatarPath);
                    CurrentUser.BasicInfo.AvatarPath = $"http://47.240.12.27:10003/Discovery/DiscoveryWebFiles/Discoverer/Images/{userAvatarPath}?{Guid.NewGuid()}";
                }
                /// 如果选择了新背景(封面)图片文件, 则上传此文件
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

        /// <summary>
        /// 导航到我的主页
        /// </summary>
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);
    }
}
