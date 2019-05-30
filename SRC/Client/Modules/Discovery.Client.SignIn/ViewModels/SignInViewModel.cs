using Discovery.Client.SignIn.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Client.SignIn.ViewModels
{
    public class SignInViewModel : BindableBase, IRegionMemberLifetime
    {
        private IRegionManager _regionManager;
        public bool KeepAlive => false;

        private string _signInName = String.Empty;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }


        public SignInViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SignInCommand = new DelegateCommand<object>(SignIn);
            NavigateToSignUpCommand = new DelegateCommand(NavigateToSignUp);
        }

        public DelegateCommand NavigateToSignUpCommand { get; }
        private void NavigateToSignUp()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.SignUp);

        public DelegateCommand<object> SignInCommand { get; }

        private void SignIn(object parameter)
        {
            using (var dataBaseService = new DataBaseServiceClient())
            {
                if (!dataBaseService.DiscovererIsExists(_signInName))
                {
                    MessageBox.Show("用户名不存在");
                    return;
                }
                if (dataBaseService.SignIn(_signInName, (parameter as PasswordBox).Password) is Discoverer discoverer)
                {
                    GlobalObjectHolder.CurrentUser = discoverer;
                    _regionManager.RequestNavigate(
                        RegionNames.MainRegion,
                        ViewNames.MainMenu);
                    return;
                }
                MessageBox.Show("密码不正确");
            }
        }
    }
}
