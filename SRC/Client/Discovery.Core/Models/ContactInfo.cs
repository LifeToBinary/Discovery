using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace Discovery.Core.Model
{
    /// <summary>
    /// 表示用户的联系方式
    /// </summary>
    public class ContactInfo : BindableBase
    {
        /// <summary>
        /// QQ 号
        /// </summary>
        private string _qq = String.Empty;
        public string QQ
        {
            get => _qq;
            set => SetProperty(ref _qq, value);
        }

        /// <summary>
        /// 微信号
        /// </summary>
        private string _weChat = String.Empty;
        public string WeChat
        {
            get => _weChat;
            set => SetProperty(ref _weChat, value);
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private string _email = String.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        /// <summary>
        /// 博客地址
        /// </summary>
        public string _blogAddress = String.Empty;
        public string BlogAddress
        {
            get => _blogAddress;
            set => SetProperty(ref _blogAddress, value);
        }
    }
}
