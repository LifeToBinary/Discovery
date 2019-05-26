using Discovery.Client.SignIn.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Client.SignIn.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string _signInName;
        public string SignInName
        {
            get => _signInName;
            set => SetProperty(ref _signInName, value);
        }

        private IRegionManager _regionManager;

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
                    NavigateToMainMenu();
                    return;
                }
                MessageBox.Show("密码不正确");
            }
        }
        private void NavigateToMainMenu()
            => _regionManager.RequestNavigate(
                RegionNames.MainRegion,
                ViewNames.MainMenu);
    }
}
