using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyConcernViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 当前用户关注的人
        /// </summary>
        private ObservableCollection<Discoverer> _myConcerns;
        public ObservableCollection<Discoverer> MyConcerns
        {
            get => _myConcerns;
            set => SetProperty(ref _myConcerns, value);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public MyConcernViewModel(IRegionManager regionManager)
        {
            MyConcerns = new ObservableCollection<Discoverer>();
            _regionManager = regionManager;
            ViewThisUsersHomePageCommand = new DelegateCommand<Discoverer>(
                ViewThisUsersHomePage);
            CancelConcernCommand = new DelegateCommand<Discoverer>(CancelConcern);
            LoadData();
        }

        /// <summary>
        /// 查询当前用户关注的人
        /// </summary>
        private async void LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                foreach (Discoverer discoverer in await databaseService.GetIdolsAsync(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID))
                {
                    MyConcerns.Add(discoverer);
                };
            }
        }

        /// <summary>
        /// 当前用户取消关注一个用户
        /// </summary>
        public DelegateCommand<Discoverer> CancelConcernCommand { get; }
        private void CancelConcern(Discoverer discoverer)
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.CancelConcern(
                    GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                    discoverer.BasicInfo.ID);
            }
            MyConcerns.Remove(discoverer);
        }

        /// <summary>
        /// 查看一个用户的主页
        /// </summary>
        public DelegateCommand<Discoverer> ViewThisUsersHomePageCommand { get; }
        private void ViewThisUsersHomePage(Discoverer discoverer)
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.OtherUsersHomePage,
                new NavigationParameters
                {
                    { "Discoverer", discoverer}
                });

        /// <summary>
        /// 从Region离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;
    }
}
