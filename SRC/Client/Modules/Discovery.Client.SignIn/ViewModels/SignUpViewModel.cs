using Discovery.Client.SignIn.DataBaseService;
using Discovery.Client.SignIn.EmailService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.Tools;
using Discovery.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Client.SignIn.ViewModels
{
    public class SignUpViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 用户名
        /// </summary>
        private string _signInName;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }

        /// <summary>
        /// 注册验证码
        /// </summary>
        private string _authenticationCode;

        /// <summary>
        /// 用户输入的验证码
        /// </summary>
        private string _inputedCode;
        public string InputedCode
        {
            get => _inputedCode;
            set => SetProperty(ref _inputedCode, value);
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        /// <summary>
        /// 感兴趣的领域
        /// </summary>
        private Field _areaOfInterest;
        public Field AreaOfInterest
        {
            get => _areaOfInterest;
            set => SetProperty(ref _areaOfInterest, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public SignUpViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SignUpAndNavigateToSignInCommand = new DelegateCommand<object>(SignUpAndNavigateToSignIn);
            SendAuthenticationCodeCommand = new DelegateCommand(SendAuthenticationCode);
            BackToSignInCommand = new DelegateCommand(BackToSignIn);
        }

        /// <summary>
        /// 检查用户输入的验证码是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckAuthenticationCode()
            => _authenticationCode == _inputedCode;

        /// <summary>
        /// 注册中
        /// </summary>
        private bool _isSigningUp;
        public bool IsSigningUp
        {
            get => _isSigningUp;
            set => SetProperty(ref _isSigningUp, value);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public DelegateCommand<object> SignUpAndNavigateToSignInCommand { get; }
        private void SignUpAndNavigateToSignIn(object parameter)
        {
            IsSigningUp = true;
            if (!CheckAuthenticationCode())
            {
                MessageBox.Show("验证码错误!");
                IsSigningUp = false;
                return;
            }
            using (var databaseService = new DataBaseServiceClient())
            {
                if (databaseService.DiscovererIsExists(SignInName))
                {
                    MessageBox.Show("用户名已存在!");
                    IsSigningUp = false;
                    return;
                }
                databaseService.SignUp(SignInName, (parameter as PasswordBox).Password, Email, AreaOfInterest);
                CreateDirectoryForThisUser();
                _regionManager.RequestNavigate(
                    RegionNames.MainRegion,
                    ViewNames.SignIn);
            }
        }

        /// <summary>
        /// 在服务器上创建文件夹以保存此用户的一些数据
        /// </summary>
        private void CreateDirectoryForThisUser()
        {
            string ftpImageFileBasePath = @"ftp://47.240.12.27/Discovery/DiscoveryWebFiles/Discoverer/Images/";
            var webFileService = new WebFileService(ftpImageFileBasePath);
            webFileService.CreateDirectoryIfNotExistWithRecurtion($"{SignInName}/Post");
        }

        /// <summary>
        /// 导航到登录界面
        /// </summary>
        public DelegateCommand BackToSignInCommand { get; }
        private void BackToSignIn()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignIn);

        /// <summary>
        /// 登录中
        /// </summary>
        private bool _isSending;
        public bool IsSending
        {
            get => _isSending;
            set => SetProperty(ref _isSending, value);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        public DelegateCommand SendAuthenticationCodeCommand { get; }
        private async void SendAuthenticationCode()
        {
            IsSending = true;
            GetAuthenticationCode();
            using (var emailService = new EmailServiceClient())
            {
                await emailService.SendEmailAsync(Email, "注册验证码", _authenticationCode);
            }
            IsSending = false;
            MessageBox.Show("验证码发送成功, 请注意查收~");
        }

        /// <summary>
        /// 生成6位随机验证码
        /// </summary>
        private void GetAuthenticationCode()
            => _authenticationCode =
                   new AuthenticationCodeBuilder
                   {
                       CharsSource = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890",
                       CodeLength = 6
                   }.Build();

        /// <summary>
        /// 导航离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
