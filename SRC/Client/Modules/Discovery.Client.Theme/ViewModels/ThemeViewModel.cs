using Discovery.Client.Resource.Themes;
using Discovery.Core.GlobalData;
using Discovery.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace Discovery.Client.Theme.ViewModels
{
    public class ThemeViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 导航离开时, 不保留视图
        /// </summary>
        public bool KeepAlive => false;

        /// <summary>
        /// 当前主题(枚举)
        /// </summary>
        public Core.Enums.Theme CurrentTheme
        {
            get => GlobalObjectHolder.CurrentTheme;
            set
            {
                if (value != CurrentTheme)
                {
                    GlobalObjectHolder.CurrentTheme = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ThemeViewModel()
        {
            ChangeAppThemeCommand = new DelegateCommand(ChangeAppTheme);
            CurrentTheme = GlobalObjectHolder.CurrentTheme;
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        public DelegateCommand ChangeAppThemeCommand { get; }
        private void ChangeAppTheme()
        {
            if (CurrentTheme == Core.Enums.Theme.DarkMagenta && 
                Application.Current.Resources.MergedDictionaries[0].GetType() != typeof(DarkMagentaTheme))
            {
                Application.Current.Resources.MergedDictionaries[0] = DarkMagentaTheme.Instance;
                AppFileHelper.WriteUserThemeSettings(CurrentTheme);
                return;
            }
            if (CurrentTheme == Core.Enums.Theme.Default &&
                Application.Current.Resources.MergedDictionaries[0].GetType() != typeof(DefaultTheme))
            {
                Application.Current.Resources.MergedDictionaries[0] = DefaultTheme.Instance;
                AppFileHelper.WriteUserThemeSettings(CurrentTheme);
                return;
            }
        }
    }
}
