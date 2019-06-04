using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class UpdatePostInfoViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// 帖子
        /// </summary>
        private Post _post;
        public Post Post
        {
            get => _post;
            set => SetProperty(ref _post, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public UpdatePostInfoViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SaveUpdateCommand = new DelegateCommand(SaveUpdate);
        }

        /// <summary>
        /// 更新帖子
        /// </summary>
        public DelegateCommand SaveUpdateCommand { get; }
        private void SaveUpdate()
        {
            Post.LastEditedTime = DateTime.Now;
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.UpdatePostInfo(Post);
            }
            NavigateToMyHomePage();
        }

        /// <summary>
        /// 导航到我的主页
        /// </summary>
        private void NavigateToMyHomePage()
            => _regionManager.RequestNavigate(
                RegionNames.MainMenuContent,
                ViewNames.DiscovererHomePage);

        /// <summary>
        /// 导航到此视图时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(
            NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Post"] is Post post)
            {
                Post = post;
            }
        }

        /// <summary>
        /// 是否可以导航到此视图
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(
            NavigationContext navigationContext)
            => true;

        /// <summary>
        /// 导航离开时
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(
            NavigationContext navigationContext) { }
    }
}
