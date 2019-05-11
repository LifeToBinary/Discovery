using System;
using Prism.Mvvm;

namespace Discovery.Model
{
    /// <summary>
    /// 表示一个帖子
    /// </summary>
    public class Post : BindableBase
    {
        /// <summary>
        /// 帖子 ID
        /// </summary>
        private int _id;
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// 帖子图标路径
        /// </summary>
        private string _iconPath;
        public string IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
        }
        
        /// <summary>
        /// 链接
        /// </summary>
        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        private DateTime _lastEditedTime;
        public DateTime LastEditedTime
        {
            get => _lastEditedTime;
            set => SetProperty(ref _lastEditedTime, value);
        }

        // TODO: change to DiscovererName? 
        /// <summary>
        /// 作者ID
        /// </summary>
        private int _authorID;
        public int AuthorID
        {
            get => _authorID;
            set => SetProperty(ref _authorID, value);
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        /// <summary>
        /// 所属标签分类
        /// </summary>
        private PostCategory _postCategory;
        public PostCategory PostCategory
        {
            get => _postCategory;
            set => SetProperty(ref _postCategory, value);
        }
    }
}
