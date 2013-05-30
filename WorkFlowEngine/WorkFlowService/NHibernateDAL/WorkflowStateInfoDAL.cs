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
    public class WorkflowStateInfoDAL : DataOperationActivityBase<WorkflowStateInfoModel>, IWorkflowStateInfoDAL
    {
        public static WorkflowStateInfoDAL Current
        {
            get { return new WorkflowStateInfoDAL(); }
        }

        public WorkflowStateInfoModel QueryByWorkflowNameAndStateNodeName(string workflowName, string stateNodeName)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                      session.CreateCriteria(typeof(WorkflowStateInfoModel))
                             .Add(Restrictions.Eq("WorkflowName", workflowName))
                             .Add(Restrictions.Eq("StateNodeName", stateNodeName))
                             .List<WorkflowStateInfoModel>().FirstOrDefault();
            }
        }

    }
}
