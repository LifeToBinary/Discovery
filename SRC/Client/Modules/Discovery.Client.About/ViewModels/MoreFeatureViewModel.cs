using Prism.Mvvm;
using Prism.Regions;

namespace Discovery.Client.About.ViewModels
{
    public class MoreFeatureViewModel 
        : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive => false;
    }
}
