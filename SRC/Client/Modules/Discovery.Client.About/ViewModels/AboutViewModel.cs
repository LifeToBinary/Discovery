using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.About.ViewModels
{
    public class AboutViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
    }
}
