using System;
using Discovery.Core.Enums;
using Prism.Mvvm;

namespace Discovery.Model
{
    /// <summary>
    /// 表示用户的基本信息
    /// </summary>
    public class BasicInfo : BindableBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        private int _id;
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 登录名
        /// </summary>
        private string _signInName;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        /// <summary>
        /// 性别
        /// </summary>
        private Sex _sex;
        public Sex Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        private DateTime _signUpTime;
        public DateTime SignUpTime
        {
            get => _signUpTime;
            set => SetProperty(ref _signUpTime, value);
        }

        /// <summary>
        /// 头像图片的路径
        /// </summary>
        private string _avatarPath;
        public string AvatarPath
        {
            get => _avatarPath;
            set => SetProperty(ref _avatarPath, value);
        }

        /// <summary>
        /// 主页背景图片的路径
        /// </summary>
        private string _profileBackgroundImagePath;
        public string ProfileBackgroundImagePath
        {
            get => _profileBackgroundImagePath;
            set => SetProperty(ref _profileBackgroundImagePath, value);
        }
    }
}
