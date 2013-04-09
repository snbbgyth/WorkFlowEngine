using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.DomainModel.Entities;
using NUnit.Framework;

namespace TestCommunication.NhibernateTest
{
    using WorkFlowService.Model;

    public class RelationModelTest : TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.Relation.hbm.xml" }; }
        }

        [Test]
        public void TestRelationModelCreateTable()
        {
            var entity = new RelationModel { ParentNodeID = "ParentNodeID", ChildNodeID = "ChildNodeID",Type=1, CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            RelationModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<RelationModel>(entity.Id);
            }
            Assert.AreEqual(entity.ParentNodeID, result.ParentNodeID);
        }

        [Test]
        public void TestRelationModelModify()
        {
            var entity = new RelationModel { ParentNodeID = "ParentNodeID", ChildNodeID = "ChildNodeID", Type = 1, CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            RelationModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<RelationModel>(entity.Id);
                queryEntity.ParentNodeID = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<RelationModel>(entity.Id);
                Assert.AreEqual(queryEntity.ParentNodeID, result.ParentNodeID);
            }
        }

        [Test]
        public void TestRelationModelDelete()
        {
            var insertEntity = new RelationModel { ParentNodeID = "ParentNodeID", ChildNodeID = "ChildNodeID", Type = 1, CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(RelationModel));
                var entity = crit.List<RelationModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<RelationModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestRelationModelQueryAll()
        {
            var insertEntity = new RelationModel { ParentNodeID = "ParentNodeID", ChildNodeID = "ChildNodeID", Type = 1, CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(RelationModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from RelationModel ").List<RelationModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestRelationModelByParentNodeID()
        {
            var insertEntity = new RelationModel { ParentNodeID = "ParentNodeID", ChildNodeID = "ChildNodeID", Type = 1, CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                RelationModel entity = null;
                var resultList = session.CreateQuery("from RelationModel ").List<RelationModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(RelationModel)).Add(Restrictions.Eq("ParentNodeID", entity.ParentNodeID));
                var result = crit.List<RelationModel>().First();

                Assert.AreEqual(result.ParentNodeID, entity.ParentNodeID);
            }
        }
    }
}
