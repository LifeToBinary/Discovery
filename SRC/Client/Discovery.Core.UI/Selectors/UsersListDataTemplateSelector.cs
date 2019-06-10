using Discovery.Core.GlobalData;
using Discovery.Core.RelationalModel;
using System.Windows;
using System.Windows.Controls;

namespace Discovery.Core.UI.Selectors
{
    public class UsersListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate CurrentUserDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(
            object item,
            DependencyObject _)
            => (item as DiscovererRelationshipModel).Discoverer.BasicInfo.ID 
                != GlobalObjectHolder.CurrentUser.BasicInfo.ID
            ? DefaultDataTemplate
            : CurrentUserDataTemplate;
    }
}
