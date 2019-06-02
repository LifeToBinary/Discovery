using Discovery.Client.PostDetail.DataBaseService;
using Discovery.Core.Constants;
using Discovery.Core.Enums;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client.PostDetail.ViewModels
{
    public class NewPostViewModel : BindableBase, IRegionMemberLifetime
    {
        private Post _newPostForAdd = new Post();
        public Post NewPostForAdd
        {
            get => _newPostForAdd;
            set => SetProperty(ref _newPostForAdd, value);
        }

        public DelegateCommand AddNewPostCommand { get; }
        public bool KeepAlive => false;

        private void AddNewPost()
        {
            InitNewPost();
            using (var databaseService = new DataBaseServiceClient())
            {
                databaseService.AddANewPost(_newPostForAdd);
            }
            _regionManager.RequestNavigate(RegionNames.MainMenuContent, ViewNames.DiscovererHomePage);
        }
        private void InitNewPost()
        {
            _newPostForAdd.CreationTime = DateTime.Now;
            _newPostForAdd.LastEditedTime = DateTime.Now;
            _newPostForAdd.AuthorID = GlobalObjectHolder.CurrentUser.BasicInfo.ID;
            _newPostForAdd.Url = String.Empty;
            _newPostForAdd.IconPath = String.Empty;
            _newPostForAdd.LastEditedTime = _newPostForAdd.CreationTime;
        }

        private IRegionManager _regionManager;
        public NewPostViewModel(IRegionManager regionManager)
        {
            AddNewPostCommand = new DelegateCommand(AddNewPost);
            _regionManager = regionManager;
        }
    }
}
