using Discovery.Client.SignIn.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Client.SignIn.ViewModels
{
    public class SignInViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 导航离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
        
        /// <summary>
        /// 正在登录
        /// </summary>
        private bool _isSigningIn = false;
        public bool IsSigningIn
        {
            get => _isSigningIn;
            set => SetProperty(ref _isSigningIn, value);
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _signInName = String.Empty;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }

        /// <summary>
        /// 登录是否失败
        /// </summary>
        private bool _signFailed;
        public bool SignFailed
        {
            get => _signFailed;
            set => SetProperty(ref _signFailed, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public SignInViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SignInCommand = new DelegateCommand<object>(SignIn);
            NavigateToSignUpCommand = new DelegateCommand(NavigateToSignUp);
        }

        /// <summary>
        /// 导航到注册界面
        /// </summary>
        public DelegateCommand NavigateToSignUpCommand { get; }
        private void NavigateToSignUp()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignUp);

        /// <summary>
        /// 登录
        /// </summary>
        public DelegateCommand<object> SignInCommand { get; }
        private async void SignIn(object parameter)
        {
            if (_isSigningIn)
            {
                return;
            }
            IsSigningIn = true;
            using (var dataBaseService = new DataBaseServiceClient())
            {
                if (!await dataBaseService.DiscovererIsExistsAsync(_signInName))
                {
                    TemporarilyChangeSignFailedPropertyValue();
                    IsSigningIn = false;
                    return;
                }
                if (await dataBaseService.SignInAsync(_signInName, (parameter as PasswordBox).Password) 
                                          is Discoverer discoverer)
                {
                    GlobalObjectHolder.CurrentUser = discoverer;
                    _regionManager.RequestNavigate(
                        RegionNames.MainRegion,
                        ViewNames.MainMenu);
                    return;
                }
                TemporarilyChangeSignFailedPropertyValue();
                IsSigningIn = false;
            }
        }

        private async void TemporarilyChangeSignFailedPropertyValue()
        {
            SignFailed = true;
            await Task.Delay(TimeSpan.FromSeconds(1));
            SignFailed = false;
        }
    }
}
