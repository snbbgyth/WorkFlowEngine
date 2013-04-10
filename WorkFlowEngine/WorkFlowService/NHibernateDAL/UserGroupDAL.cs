using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class UserGroupDAL : DataOperationActivityBase<UserGroupModel>
    {
        public static UserGroupDAL Current
        {
            get { return new UserGroupDAL(); }
        }

        public UserGroupModel QueryByGroupName(string groupName)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(UserGroupModel))
                           .Add(Restrictions.Eq("GroupName", groupName))
                           .List<UserGroupModel>().FirstOrDefault();
            }
        }

    }
}
