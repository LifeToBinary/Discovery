using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel.DataBaseService;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Core.RelationalModel
{
    public class PostRelationalModel : BindableBase 
    {
        private Post _post;
        public Post Post
        {
            get => _post;
            set => SetProperty(ref _post, value);
        }

        private bool _isBeFavorited;
        public bool IsBeFavorited
        {
            get => _isBeFavorited;
            set => SetProperty(ref _isBeFavorited, value);
        }

        public DelegateCommand FavoriteOrCancelFavoriteCommand { get; }
        private void FavoriteOrCancelFavorite()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (IsBeFavorited)
                {
                    databaseService.CancelFavorite(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Post.ID);
                    IsBeFavorited = false;
                }
                else
                {
                    databaseService.FavoriteAPost(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Post.ID);
                    IsBeFavorited = true;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PostRelationalModel() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="post"></param>
        /// <param name="isBeFavorited"></param>
        public PostRelationalModel(
            Post post,
            bool isBeFavorited)
        {
            Post = post;
            IsBeFavorited = isBeFavorited;
            FavoriteOrCancelFavoriteCommand = new DelegateCommand(FavoriteOrCancelFavorite);
        }
    }
}
