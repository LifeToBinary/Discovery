using Discovery.Client.Resource.Themes;
using Discovery.Core.GlobalData;
using Discovery.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using ThemeEnum = Discovery.Core.Enums.Theme;
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
            var enumValues = new ThemeEnum[] { ThemeEnum.Default, ThemeEnum.DarkMagenta, ThemeEnum.Pink };
            var resources = new ResourceDictionary[] { DefaultTheme.Instance, DarkMagentaTheme.Instance, PinkTheme.Instance };
            SetTheme();

            void SetTheme(int index = 0)
            {
                if (CurrentTheme == enumValues[index] &&
                    Application.Current.Resources.MergedDictionaries[0].GetType() != resources[index].GetType())
                {
                    Application.Current.Resources.MergedDictionaries[0] = resources[index];
                    AppFileHelper.WriteUserThemeSettings(CurrentTheme);
                    return;
                }
                SetTheme(index + 1);
            }
        }
    }
}
