using Discovery.Client.SignIn.DataBaseService;
using Discovery.Client.SignIn.EmailService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.Tools;
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
        /// 密码
        /// </summary>
        private string _password;

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

        private readonly IRegionManager _regionManager;
        public SignUpViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NaviagateToSignUpFinallyStepCommand = new DelegateCommand<object>(NavigateToSignUpFinallyStep);
            BackToSignUpFirstStepCommand = new DelegateCommand(BackToSignUpFirstStep);
            SignUpAndNavigateToSignInCommand = new DelegateCommand(SignUpAndNavigateToSignIn);
            SendAuthenticationCodeCommand = new DelegateCommand(SendAuthenticationCode);
            BackToSignInCommand = new DelegateCommand(BackToSignIn);
        }

        public DelegateCommand<object> NaviagateToSignUpFinallyStepCommand { get; set; }
        private void NavigateToSignUpFinallyStep(object parameter)
        {
            if (!CheckAuthenticationCode())
            {
                MessageBox.Show("验证码错误!");
                return;
            }
            using (var databaseService = new DataBaseServiceClient())
            {
                if (databaseService.DiscovererIsExists(SignInName))
                {
                    MessageBox.Show("用户名已存在!");
                    return;
                }
            }
            _password = (parameter as PasswordBox).Password;
            _regionManager.RequestNavigate(
                RegionNames.SignUpRegion,
                ViewNames.SignUpFinallyStep);
        }
        private bool CheckAuthenticationCode()
            => _authenticationCode == _inputedCode;
        public DelegateCommand SignUpAndNavigateToSignInCommand { get; }
        private void SignUpAndNavigateToSignIn()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.SignUp(SignInName, _password, Email, AreaOfInterest);
            }
            _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignIn);
        }
        public DelegateCommand BackToSignInCommand { get; }
        private void BackToSignIn()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignIn);
        public DelegateCommand BackToSignUpFirstStepCommand { get; }
        private void BackToSignUpFirstStep()
            => _regionManager.RequestNavigate(
                RegionNames.SignUpRegion,
                ViewNames.SignUpFirstStep);
        public DelegateCommand SendAuthenticationCodeCommand { get; }
        private void SendAuthenticationCode()
        {
            GetAuthenticationCode();
            using (var emailService = new EmailServiceClient())
            {
                emailService.SendEmail(Email, "注册验证码", _authenticationCode);
            }
            MessageBox.Show("验证码发送成功, 请注意查收~");
        }
        private void GetAuthenticationCode()
            => _authenticationCode = 
                   new AuthenticationCodeBuilder
                   {
                       CharsSource = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890",
                       CodeLength = 6
                   }.Build();
        public bool KeepAlive => false;
    }
}
