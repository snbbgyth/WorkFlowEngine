using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using CommonLibrary.Model;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Help;

namespace WorkFlowService.NHibernateDAL
{
    public class WorkFlowActivityLogDAL : DataOperationActivityBase<WorkFlowActivityLogModel>
    {
        public static WorkFlowActivityLogDAL Current
        {
            get { return new WorkFlowActivityLogDAL(); }
        }

        public IList<WorkFlowActivityLogModel> QueryInProgressActivityByOperatorUserId(string operatorUserId)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                      session.CreateCriteria(typeof(WorkFlowActivityLogModel))
                             .Add(Restrictions.Eq("OperatorUserId", operatorUserId))
                             .List<WorkFlowActivityLogModel>();
            }
        }

        public WorkFlowActivityLogModel QueryByAppId(string appId)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                      session.CreateCriteria(typeof(WorkFlowActivityLogModel))
                             .Add(Restrictions.Eq("AppId", appId))
                             .List<WorkFlowActivityLogModel>().FirstOrDefault();
            }
        }

    }
}
