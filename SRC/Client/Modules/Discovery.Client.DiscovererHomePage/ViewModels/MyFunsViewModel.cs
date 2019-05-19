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
    public class MyFunsViewModel : BindableBase
    {
        public Discoverer CurrentUser { get; }
        private ObservableCollection<Discoverer> _funs;
        public ObservableCollection<Discoverer> Funs
        {
            get => _funs;
            set => SetProperty(ref _funs, value);
        }
        public MyFunsViewModel()
        {
            CurrentUser = GlobalObjectHolder.CurrentUser;
            Funs = new ObservableCollection<Discoverer>(LoadData());
        }
        private Discoverer[] LoadData()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                return databaseService.GetFunsOfTheIdol(CurrentUser.BasicInfo.ID);
            }
        }
    }
}
