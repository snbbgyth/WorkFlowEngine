using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.DomainModel.BLL;
using WorkFlowService.Help;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class RelationDAL : DataOperationActivityBase<RelationModel>
    {
        public static RelationDAL Current
        {
            get { return new RelationDAL(); }
        }

        public RelationModel QueryByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(RelationModel))
                           .Add(Restrictions.Eq("ChildNodeID", childNodeID))
                           .Add(Restrictions.Eq("ParentNodeID", parentNodeID))
                           .Add(Restrictions.Eq("Type", type))
                           .List<RelationModel>().FirstOrDefault();
            }
        }
    
        public IList<RelationModel> QueryByChildNodeIDAndType(string childNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(RelationModel))
                           .Add(Restrictions.Eq("ChildNodeID", childNodeID))
                           .Add(Restrictions.Eq("Type", type))
                           .List<RelationModel>();
            }
        }
 
        public IList<RelationModel> QueryByParentNodeIDAndType(string parentNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                return
                    session.CreateCriteria(typeof(RelationModel))
                           .Add(Restrictions.Eq("ParentNodeID", parentNodeID))
                           .Add(Restrictions.Eq("Type", type))
                           .List<RelationModel>();
            }
        }

        public int DeleteByChildNodeIDAndType(string childNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var queryString = string.Format("delete {0} where ChildNodeID = :childNodeID and Type=:type",
                                      typeof(RelationModel));
               return  session.CreateQuery(queryString)
                       .SetParameter("childNodeID", childNodeID)
                       .SetParameter("type",type)
                       .ExecuteUpdate();
            }
        }

        public int DeleteByParentNodeIDAndType(string parentNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var queryString = string.Format("delete {0} where ParentNodeID = :parentNodeID and Type=:type",
                                      typeof(RelationModel));
                return session.CreateQuery(queryString)
                        .SetParameter("parentNodeID", parentNodeID)
                        .SetParameter("type", type)
                        .ExecuteUpdate();
            }
        }

        public int DeleteByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type)
        {
            using (var session = NhibernateHelp.Instance.GetSession())
            {
                var queryString = string.Format("delete {0} where ParentNodeID = :parentNodeID and Type=:type and ChildNodeID = :childNodeID ",
                                      typeof(RelationModel));
                return session.CreateQuery(queryString)
                        .SetParameter("parentNodeID", parentNodeID)
                        .SetParameter("childNodeID", childNodeID)
                        .SetParameter("type", type)
                        .ExecuteUpdate();
            }
        }
    }
}
