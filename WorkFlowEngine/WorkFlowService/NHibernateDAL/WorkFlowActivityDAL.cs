using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using CommonLibrary.Model;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Help;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class WorkFlowActivityDAL : DataOperationActivityBase<WorkFlowActivityModel>
    {
        public static WorkFlowActivityDAL Current
        {
            get { return new WorkFlowActivityDAL(); }
        }

        public IList<WorkFlowActivityModel> QueryInProgressActivityByOperatorUserId(string operatorUserId)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
              return 
                    session.CreateCriteria(typeof(WorkFlowActivityModel))
                           .Add(Restrictions.Eq("OperatorUserId", operatorUserId))
                           .List<WorkFlowActivityModel>();
            }
        }

        public WorkFlowActivityModel QueryByAppId(string appId)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                      session.CreateCriteria(typeof(WorkFlowActivityModel))
                             .Add(Restrictions.Eq("AppId", appId))
                             .List<WorkFlowActivityModel>().FirstOrDefault();
            }
        }
    }
}
