using Discovery.Client.DiscovererHomePage.DataBaseService;
using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Client.DiscovererHomePage.ViewModels
{
    public class MyConcernViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private ObservableCollection<Discoverer> _idols;
        public ObservableCollection<Discoverer> Idols
        {
            get => _idols;
            set => SetProperty(ref _idols, value);
        }
        public MyConcernViewModel()
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            _idols = new ObservableCollection<Discoverer>(LoadData());
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetIdols(CurrentUser.BasicInfo.ID);
            }
        }
    }
}
