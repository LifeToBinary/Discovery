using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class NewPostViewModel : BindableBase, IRegionMemberLifetime
    {
        /// <summary>
        /// 新帖子
        /// </summary>
        private Post _newPostForAdd = new Post();
        public Post NewPostForAdd
        {
            get => _newPostForAdd;
            set => SetProperty(ref _newPostForAdd, value);
        }

        /// <summary>
        /// Region 导航对象
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager"></param>
        public NewPostViewModel(IRegionManager regionManager)
        {
            NewPostForAdd = new Post { Author = GlobalObjectHolder.CurrentUser };
            AddNewPostCommand = new DelegateCommand(AddNewPost);
            _regionManager = regionManager;
        }

        /// <summary>
        /// 发布新帖子
        /// </summary>
        public DelegateCommand AddNewPostCommand { get; }
        private void AddNewPost()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.AddANewPost(_newPostForAdd);
            }
            _regionManager.RequestNavigate(RegionNames.MainMenuContent, ViewNames.DiscovererHomePage);
        }

        /// <summary>
        /// 从 Region 离开时, 不保留此视图
        /// </summary>
        public bool KeepAlive => false;

    }
}
