using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace Discovery.Model
{
    public class Discoverer : BindableBase
    {
        private BasicInfo _basicInfo;
        public BasicInfo BasicInfo
        {
            get => _basicInfo;
            set => SetProperty(ref _basicInfo, value);
        }

        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get => _contactInfo;
            set => SetProperty(ref _contactInfo, value);
        }
    }
}
