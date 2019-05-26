using Discovery.Client.PostDetail.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.PostDetail
{
    public class PostDetailModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MyPostDetail>();
            containerRegistry.RegisterForNavigation<OtherUsersPostDetail>();
            containerRegistry.RegisterForNavigation<NewPost>();
            containerRegistry.RegisterForNavigation<UpdatePostInfo>();
        }
    }
}
