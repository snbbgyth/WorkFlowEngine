using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.IDAL;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Dialect.Schema;
using NHibernate.Engine;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Type;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    using NHibernate.DomainModel.BLL;
    public abstract class DataOperationActivityBase<T> : IDataOperationActivity<T> where T : new()
    {
        #region Private Variable

        #endregion

        #region Private Property

        #endregion

        public int Insert(T entity)
        {
            using (var session =NhibernateHelp.Instance.GetSession())
            {
                session.Save(entity);
                session.Flush();
                return 1;
            }
           
        }
 
        public int Modify(T entity)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                session.Update(entity);
                session.Flush();
                return 1;
            }
        }
 
        public int DeleteByID(string id)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var queryString = string.Format("delete {0} where id = :id",
                                        typeof(T));
               return session.CreateQuery(queryString)
                       .SetParameter("id", id)
                       .ExecuteUpdate();
            }
        }
 
        public List<T> QueryAll()
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return session.CreateCriteria(typeof (T)).List<T>().ToList();
            }
        }
 
        public T QueryByID(string id)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
               return  session.Get<T>(id);
              
            }
        }
 
 
 
    }
}
