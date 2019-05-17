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

        private Discoverer _currentUser;
        private IRegionManager _regionManager;
        public DelegateCommand<object> SignInCommand { get; }

        public SignInViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SignInCommand = new DelegateCommand<object>(SignIn);
        }


        private void SignIn(object parameter)
        {
            using (var dataBaseService = new DataBaseServiceClient())
            {
                if (!dataBaseService.DiscovererIsExists(_signInName))
                {
                    MessageBox.Show("用户名不存在");
                    return;
                }
                _currentUser = dataBaseService.SignIn(_signInName, (parameter as PasswordBox).Password);

                if (_currentUser is null)
                {
                    MessageBox.Show("密码不正确");
                    return;
                }
                GlobalObjectHolder.CurrentUser = _currentUser;
                _regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.MainMenu);
            }
        }
    }
}
