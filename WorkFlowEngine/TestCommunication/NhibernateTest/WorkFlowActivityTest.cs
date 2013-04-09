using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.DomainModel.Entities;
using NUnit.Framework;
 

namespace TestCommunication.NhibernateTest
{
   public class WorkFlowActivityTest:TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.WorkFlowActivity.hbm.xml" }; }
        }

        [Test]
        public void TestWorkFlowActivityCreateTable()
        {
            var entity = new WorkFlowActivityModel { AppId = "AppId", ApplicationState = "ApplicationState", AppName = "AppName", CreateUserId = "CreateUserId", CurrentWorkflowState = "CurrentWorkflowState", ForeWorkflowState = "ForeWorkflowState", OperatorActivity = "OperatorActivity", OperatorUserId = "OperatorUserId", OperatorUserList = "OperatorUserList", WorkflowName = "WorkflowName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            WorkFlowActivityModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<WorkFlowActivityModel>(entity.Id);
            }
            Assert.AreEqual(entity.AppId, result.AppId);
        }

        [Test]
        public void TestWorkFlowActivityModify()
        {
            var entity = new WorkFlowActivityModel { AppId = "AppId", ApplicationState = "ApplicationState", AppName = "AppName", CreateUserId = "CreateUserId", CurrentWorkflowState = "CurrentWorkflowState", ForeWorkflowState = "ForeWorkflowState", OperatorActivity = "OperatorActivity", OperatorUserId = "OperatorUserId", OperatorUserList = "OperatorUserList", WorkflowName = "WorkflowName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            WorkFlowActivityModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<WorkFlowActivityModel>(entity.Id);
                queryEntity.AppId = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<WorkFlowActivityModel>(entity.Id);
                Assert.AreEqual(queryEntity.AppId, result.AppId);
            }
        }

        [Test]
        public void TestWorkFlowActivityDelete()
        {
            var insertEntity = new WorkFlowActivityModel { AppId = "AppId", ApplicationState = "ApplicationState", AppName = "AppName", CreateUserId = "CreateUserId", CurrentWorkflowState = "CurrentWorkflowState", ForeWorkflowState = "ForeWorkflowState", OperatorActivity = "OperatorActivity", OperatorUserId = "OperatorUserId", OperatorUserList = "OperatorUserList", WorkflowName = "WorkflowName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkFlowActivityModel));
                var entity = crit.List<WorkFlowActivityModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<WorkFlowActivityModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestWorkFlowActivityQueryAll()
        {
            var insertEntity = new WorkFlowActivityModel { AppId = "AppId", ApplicationState = "ApplicationState", AppName = "AppName", CreateUserId = "CreateUserId", CurrentWorkflowState = "CurrentWorkflowState", ForeWorkflowState = "ForeWorkflowState", OperatorActivity = "OperatorActivity", OperatorUserId = "OperatorUserId", OperatorUserList = "OperatorUserList", WorkflowName = "WorkflowName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkFlowActivityModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from WorkFlowActivityModel ").List<WorkFlowActivityModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestWorkFlowActivityQueryByActionName()
        {
            var insertEntity = new WorkFlowActivityModel { AppId = "AppId", ApplicationState = "ApplicationState", AppName = "AppName", CreateUserId = "CreateUserId", CurrentWorkflowState = "CurrentWorkflowState", ForeWorkflowState = "ForeWorkflowState", OperatorActivity = "OperatorActivity", OperatorUserId = "OperatorUserId", OperatorUserList = "OperatorUserList", WorkflowName = "WorkflowName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                WorkFlowActivityModel entity = null;
                var resultList = session.CreateQuery("from WorkFlowActivityModel ").List<WorkFlowActivityModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(WorkFlowActivityModel)).Add(Restrictions.Eq("AppId", entity.AppId));
                var result = crit.List<WorkFlowActivityModel>().First();

                Assert.AreEqual(result.AppId, entity.AppId);
            }
        }
    }
}
