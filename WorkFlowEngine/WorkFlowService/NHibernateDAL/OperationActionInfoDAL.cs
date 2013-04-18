using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Help;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class OperationActionInfoDAL : DataOperationActivityBase<OperationActionInfoModel>
    {
        public static  OperationActionInfoDAL Current
        {
            get { return new  OperationActionInfoDAL(); }
        }

        public OperationActionInfoModel QueryByCondition(string workflowName, string actionName)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
              return  session.CreateCriteria(typeof(OperationActionInfoModel))
                          .Add(Restrictions.Eq("ActionName", actionName))
                          .Add(Restrictions.Eq("WorkflowName", workflowName))
                          .List<OperationActionInfoModel>().FirstOrDefault();
            }
        }
    }
}
