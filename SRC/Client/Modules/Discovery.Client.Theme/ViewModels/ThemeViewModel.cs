using Discovery.Client.Resource.Themes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client.Theme.ViewModels
{
    public class ThemeViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
        public ThemeViewModel()
        {
            ChangeAppThemeCommand = new DelegateCommand<Core.Enums.Theme?>(ChangeAppTheme);
        }
        public DelegateCommand<Core.Enums.Theme?> ChangeAppThemeCommand { get; }
        private void ChangeAppTheme(Core.Enums.Theme? newTheme)
        {
            if (newTheme == Core.Enums.Theme.DarkMagenta && 
                Application.Current.Resources.MergedDictionaries[0].GetType() != typeof(DarkMagentaTheme))
            {
                Application.Current.Resources.MergedDictionaries[0] = DarkMagentaTheme.Instance;
                return;
            }
            if (newTheme == Core.Enums.Theme.Default &&
                Application.Current.Resources.MergedDictionaries[0].GetType() != typeof(DefaultTheme))
            {
                Application.Current.Resources.MergedDictionaries[0] = DefaultTheme.Instance;
                return;
            }
        }
    }
}
