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

namespace WorkFlowService.NHibernateDAL
{
    public class WorkFlowActivityLogDAL : DataOperationActivityBase<WorkFlowActivityLogModel>, IWorkFlowActivityLogDAL
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

        public WorkFlowActivityLogModel QueryByOldId(string oldId)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                      session.CreateCriteria(typeof(WorkFlowActivityLogModel))
                             .Add(Restrictions.Eq("OldID", oldId))
                             .List<WorkFlowActivityLogModel>().FirstOrDefault();
            }
        }

        public IList<WorkFlowActivityLogModel> QueryByCondition(KeyValuePair<string, string> workflowParam, KeyValuePair<string, object> conditionParam)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var iCriteria =
                     session.CreateCriteria(typeof(WorkFlowActivityLogModel));
                if (!string.IsNullOrEmpty(workflowParam.Key) && !string.IsNullOrEmpty(workflowParam.Value))
                {
                    iCriteria.Add(Restrictions.Eq(workflowParam.Key, workflowParam.Value));
                }
                if (!string.IsNullOrEmpty(conditionParam.Key) && !string.IsNullOrEmpty(conditionParam.Value.ToString()))
                    iCriteria.Add(Restrictions.Like(conditionParam.Key, string.Format("%{0}%", conditionParam.Value)));
                return iCriteria.List<WorkFlowActivityLogModel>();
            }
        }

    }
}
