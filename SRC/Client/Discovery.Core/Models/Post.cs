using System;
using Discovery.Core.Enums;
using Prism.Mvvm;

namespace Discovery.Core.Model
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
        /// 创建时间
        /// </summary>
        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }

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
    }
}
