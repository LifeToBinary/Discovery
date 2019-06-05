using Discovery.Client.Feedback.EmailService;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;

namespace Discovery.Client.Feedback.ViewModels
{
    public class FeedbackViewModel : BindableBase, IRegionMemberLifetime
    {
        public FeedbackViewModel()
            => SendEmailCommand = new DelegateCommand(SendEmail);

        /// <summary>
        /// 反馈的内容
        /// </summary>
        private string _emailContent;
        public string EMailContent
        {
            get => _emailContent;
            set => SetProperty(ref _emailContent, value);
        }

        /// <summary>
        /// 用户的邮箱地址
        /// </summary>
        private string _emailOfCustomer;
        public string EmailOfCustomer
        {
            get => _emailOfCustomer;
            set => SetProperty(ref _emailOfCustomer, value);
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        private string _nameOfCustomer;
        public string NameOfCustomer
        {
            get => _nameOfCustomer;
            set => SetProperty(ref _nameOfCustomer, value);
        }

        /// <summary>
        /// 发送反馈命令
        /// </summary>
        public DelegateCommand SendEmailCommand { get; }

        /// <summary>
        /// 导航离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

        /// <summary>
        /// 发送反馈
        /// </summary>
        public async void SendEmail()
        {
            string feedBackContent = $"{_nameOfCustomer}<{_emailOfCustomer}>{Environment.NewLine}{_emailContent}";
            using (var emailService = new EmailServiceClient())
            {
                await emailService.SendEmailAsync("tobinary@outlook.com", "Discovery 反馈", feedBackContent);
            }
            MessageBox.Show("发送成功!");
        }
    }
}
