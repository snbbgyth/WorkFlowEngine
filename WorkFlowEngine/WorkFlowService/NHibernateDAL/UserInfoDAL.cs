using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    using IDAL;
    public class UserInfoDAL : DataOperationActivityBase<UserInfoModel>,IUserInfoDAL
    {
        public static UserInfoDAL Current
        {
            get { return new UserInfoDAL(); }
        }

        public string Login(string userName, string password)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var entity=
                    session.CreateCriteria(typeof(UserInfoModel))
                           .Add(Restrictions.Eq("UserName", userName))
                           .Add(Restrictions.Eq("Password", password))
                           .List<UserInfoModel>().FirstOrDefault();
                return entity==null?null: entity.Id;
            }
        }

        public UserInfoModel QueryByUserName(string userName)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(UserInfoModel))
                           .Add(Restrictions.Eq("UserName", userName))
                           .List<UserInfoModel>().FirstOrDefault();
            }
        }

    }
}
