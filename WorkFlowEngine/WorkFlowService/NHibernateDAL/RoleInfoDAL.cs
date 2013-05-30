using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{

    public class RoleInfoDAL : DataOperationActivityBase<RoleInfoModel>, IRoleInfoDAL
    {
        public static RoleInfoDAL Current
        {
            get { return new RoleInfoDAL(); }
        }
 
        //Todo: now is wrong
        public int DeleteByUserID(string userID)
        {
            return 0;
        }

        //Todo: now is wrong
        public List<RoleInfoModel> QueryByUserID(string userID)
        {
            return null;

        }

        public RoleInfoModel QueryByCondition(string workflowName, string roleName)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(RoleInfoModel))
                           .Add(Restrictions.Eq("RoleName", roleName))
                           .Add(Restrictions.Eq("WorkflowName", workflowName))
                           .List<RoleInfoModel>().FirstOrDefault();
            }
        }

    }
}
