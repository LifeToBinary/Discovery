using Discovery.Core.Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Discovery.Core.Model
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Discoverer : BindableBase
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        private BasicInfo _basicInfo;
        public BasicInfo BasicInfo
        {
            get => _basicInfo;
            set => SetProperty(ref _basicInfo, value);
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get => _contactInfo;
            set => SetProperty(ref _contactInfo, value);
        }

        public override bool Equals(object obj)
            => (obj as Discoverer).BasicInfo.ID == BasicInfo.ID;

        public override int GetHashCode()
            => BasicInfo.ID.GetHashCode();
    }
}
