using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using CommonLibrary.Model;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Help;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class WorkFlowActivityDAL : DataOperationActivityBase<WorkFlowActivityModel>, IWorkFlowActivityDAL
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

        public IList<WorkFlowActivityModel> QueryByCondition(KeyValuePair<string, string> workflowParam, KeyValuePair<string, object> conditionParam)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var iCriteria =
                     session.CreateCriteria(typeof(WorkFlowActivityModel));
                if (!string.IsNullOrEmpty(workflowParam.Key) && !string.IsNullOrEmpty(workflowParam.Value))
                {
                    iCriteria.Add(Restrictions.Eq(workflowParam.Key, workflowParam.Value));
                }
                if (!string.IsNullOrEmpty(conditionParam.Key) && !string.IsNullOrEmpty(conditionParam.Value.ToString()))
                    iCriteria.Add(Restrictions.Like(conditionParam.Key,string.Format("%{0}%", conditionParam.Value)));
                return iCriteria.List<WorkFlowActivityModel>();
            }
        }
    }
}
