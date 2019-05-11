using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace Discovery.Model
{
    public class BasicInfo : BindableBase
    {
        private int _id;
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _signInName;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private int _sex;
        public int Sex
        {
            get => _sex;
            set => SetProperty(ref _sex, value);
        }

        private DateTime _signUpTime;
        public DateTime SignUpTime
        {
            get => _signUpTime;
            set => SetProperty(ref _signUpTime, value);
        }

        private string _avatarPath;
        public string AvatarPath
        {
            get => _avatarPath;
            set => SetProperty(ref _avatarPath, value);
        }

        private string _profileBackgroundImagePath;
        public string ProfileBackgroundImagePath
        {
            get => _profileBackgroundImagePath;
            set => SetProperty(ref _profileBackgroundImagePath, value);
        }
    }
}
