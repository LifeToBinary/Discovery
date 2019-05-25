using Discovery.Core.GlobalData;
using Discovery.Core.Model;
using Discovery.Core.RelationalModel.DataBaseService;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Core.RelationalModel
{
    public class DiscovererRelationshipModel : BindableBase
    {
        /// <summary>
        /// 用户对象
        /// </summary>
        private Discoverer _discoverer;
        public Discoverer Discoverer
        {
            get => _discoverer;
            set => SetProperty(ref _discoverer, value);
        }

        /// <summary>
        /// 是否被当前用户所关注
        /// </summary>
        private bool _isBeConcerned;
        public bool IsBeConcerned
        {
            get => _isBeConcerned;
            set => SetProperty(ref _isBeConcerned, value);
        }

        /// <summary>
        /// 关注/取消关注 此用户
        /// </summary>
        public DelegateCommand ConcernOrCancelConcernCommand { get; }
        private void ConcernOrCancelConcern()
        {
            using (var databaseService = new DataBaseServiceClient())
            {
                if (IsBeConcerned)
                {
                    databaseService.CancelConcern(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsBeConcerned = false;
                }
                else
                {
                    databaseService.ConcernADiscoverer(
                        GlobalObjectHolder.CurrentUser.BasicInfo.ID,
                        Discoverer.BasicInfo.ID);
                    IsBeConcerned = true;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DiscovererRelationshipModel() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="discoverer"></param>
        /// <param name="isBeConcerned"></param>
        /// <param name="command"></param>
        public DiscovererRelationshipModel(
            Discoverer discoverer,
            bool isBeConcerned)
        {
            Discoverer = discoverer;
            IsBeConcerned = isBeConcerned;
            ConcernOrCancelConcernCommand = new DelegateCommand(ConcernOrCancelConcern);
        }
    }
}
