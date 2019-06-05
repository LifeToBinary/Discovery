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
