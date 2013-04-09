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
   public class WorkflowStateInfoModelTest:TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.WorkflowStateInfo.hbm.xml" }; }
        }

        [Test]
        public void TestWorkflowStateInfoModelCreateTable()
        {
            var entity = new WorkflowStateInfoModel { WorkflowName = "WorkflowName", WorkflowDisplayName = "WorkflowDisplayName", StateNodeName = "StateNodeName", StateNodeDisplayName = "StateNodeDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            WorkflowStateInfoModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<WorkflowStateInfoModel>(entity.Id);
            }
            Assert.AreEqual(entity.WorkflowName, result.WorkflowName);
        }

        [Test]
        public void TestWorkflowStateInfoModelModify()
        {
            var entity = new WorkflowStateInfoModel { WorkflowName = "WorkflowName", WorkflowDisplayName = "WorkflowDisplayName", StateNodeName = "StateNodeName", StateNodeDisplayName = "StateNodeDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            WorkflowStateInfoModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<WorkflowStateInfoModel>(entity.Id);
                queryEntity.WorkflowName = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<WorkflowStateInfoModel>(entity.Id);
                Assert.AreEqual(queryEntity.WorkflowName, result.WorkflowName);
            }
        }

        [Test]
        public void TestWorkflowStateInfoModelDelete()
        {
            var insertEntity = new WorkflowStateInfoModel { WorkflowName = "WorkflowName", WorkflowDisplayName = "WorkflowDisplayName", StateNodeName = "StateNodeName", StateNodeDisplayName = "StateNodeDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkflowStateInfoModel));
                var entity = crit.List<WorkflowStateInfoModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<WorkflowStateInfoModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestWorkflowStateInfoModelQueryAll()
        {
            var insertEntity = new WorkflowStateInfoModel { WorkflowName = "WorkflowName", WorkflowDisplayName = "WorkflowDisplayName", StateNodeName = "StateNodeName", StateNodeDisplayName = "StateNodeDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkflowStateInfoModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from WorkflowStateInfoModel ").List<WorkflowStateInfoModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestWorkflowStateInfoModelByRoleName()
        {
            var insertEntity = new WorkflowStateInfoModel { WorkflowName = "WorkflowName", WorkflowDisplayName = "WorkflowDisplayName", StateNodeName = "StateNodeName", StateNodeDisplayName = "StateNodeDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                WorkflowStateInfoModel entity = null;
                var resultList = session.CreateQuery("from WorkflowStateInfoModel ").List<WorkflowStateInfoModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(WorkflowStateInfoModel)).Add(Restrictions.Eq("WorkflowName", entity.WorkflowName));
                var result = crit.List<WorkflowStateInfoModel>().First();
                Assert.AreEqual(result.WorkflowName, entity.WorkflowName);
            }
        }
    }
}
