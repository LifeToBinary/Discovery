using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace Discovery.Model
{
    public class ContactInfo : BindableBase
    {
        private string _qq;
        public string QQ
        {
            get => _qq;
            set => SetProperty(ref _qq, value);
        }
        private string _weChat;
        public string WeChat
        {
            get => _weChat;
            set => SetProperty(ref _weChat, value);
        }
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string _blogAddress;
        public string BlogAddress
        {
            get => _blogAddress;
            set => SetProperty(ref _blogAddress, value);
        }
    }
}
