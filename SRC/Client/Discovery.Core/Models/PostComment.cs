using Discovery.Core.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Core.Models
{
    public class PostComment : BindableBase
    {
        /// <summary>
        /// 评论 ID
        /// </summary>
        private int _id;
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// 帖子ID
        /// </summary>
        private int _postID;
        public int PostID
        {
            get => _postID;
            set => SetProperty(ref _postID, value);
        }

        /// <summary>
        /// 评论内容
        /// </summary>
        private string _comment;
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        /// <summary>
        /// 评论者
        /// </summary>
        private Discoverer _author;
        public Discoverer Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        /// <summary>
        /// 评论时间
        /// </summary>
        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
        }
    }
}
