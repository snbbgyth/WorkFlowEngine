using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;

namespace TestCommunication.NhibernateTest
{
    public class WorkFlowActivityLogLogTest : TestCase
    {

        protected override IList Mappings
        {
            get { return new[] { "Mappings.WorkFlowActivityLog.hbm.xml" }; }
        }

        [Test]
        public void TestWorkFlowActivityLogCreateTable()
        {
            var entity = new WorkFlowActivityLogModel
                             {
                                 AppId = "AppId",
                                 OldID = "OldID",
                                 ApplicationState = "ApplicationState",
                                 AppName = "AppName",
                                 CreateUserId = "CreateUserId",
                                 CurrentWorkflowState = "CurrentWorkflowState",
                                 ForeWorkFlowState = "ForeWorkflowState",
                                 OperatorActivity = "OperatorActivity",
                                 OperatorUserId = "OperatorUserId",
                                 OperatorUserList = "OperatorUserList",
                                 WorkflowName = "WorkflowName",
                                 CreateDateTime = DateTime.Now,
                                 Id = Guid.NewGuid().ToString(),
                                 LastUpdateDateTime = DateTime.Now
                             };
            WorkFlowActivityLogModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<WorkFlowActivityLogModel>(entity.Id);
            }
            Assert.AreEqual(entity.AppId, result.AppId);
        }

        [Test]
        public void TestWorkFlowActivityLogModify()
        {
            var entity = new WorkFlowActivityLogModel
                             {
                                 AppId = "AppId",
                                 OldID = "OldID",
                                 ApplicationState = "ApplicationState",
                                 AppName = "AppName",
                                 CreateUserId = "CreateUserId",
                                 CurrentWorkflowState = "CurrentWorkflowState",
                                 ForeWorkFlowState = "ForeWorkflowState",
                                 OperatorActivity = "OperatorActivity",
                                 OperatorUserId = "OperatorUserId",
                                 OperatorUserList = "OperatorUserList",
                                 WorkflowName = "WorkflowName",
                                 CreateDateTime = DateTime.Now,
                                 Id = Guid.NewGuid().ToString(),
                                 LastUpdateDateTime = DateTime.Now
                             };
            WorkFlowActivityLogModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<WorkFlowActivityLogModel>(entity.Id);
                queryEntity.AppId = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<WorkFlowActivityLogModel>(entity.Id);
                Assert.AreEqual(queryEntity.AppId, result.AppId);
            }
        }

        [Test]
        public void TestWorkFlowActivityLogDelete()
        {
            var insertEntity = new WorkFlowActivityLogModel
            {
                AppId = "AppId",
                OldID = "OldID",
                ApplicationState = "ApplicationState",
                AppName = "AppName",
                CreateUserId = "CreateUserId",
                CurrentWorkflowState = "CurrentWorkflowState",
                ForeWorkFlowState = "ForeWorkflowState",
                OperatorActivity = "OperatorActivity",
                OperatorUserId = "OperatorUserId",
                OperatorUserList = "OperatorUserList",
                WorkflowName = "WorkflowName",
                CreateDateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                LastUpdateDateTime = DateTime.Now
            };

            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkFlowActivityLogModel));
                var entity = crit.List<WorkFlowActivityLogModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<WorkFlowActivityLogModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestWorkFlowActivityLogQueryAll()
        {
            var insertEntity = new WorkFlowActivityLogModel
            {
                AppId = "AppId",
                OldID = "OldID",
                ApplicationState = "ApplicationState",
                AppName = "AppName",
                CreateUserId = "CreateUserId",
                CurrentWorkflowState = "CurrentWorkflowState",
                ForeWorkFlowState = "ForeWorkflowState",
                OperatorActivity = "OperatorActivity",
                OperatorUserId = "OperatorUserId",
                OperatorUserList = "OperatorUserList",
                WorkflowName = "WorkflowName",
                CreateDateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                LastUpdateDateTime = DateTime.Now
            };

            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(WorkFlowActivityLogModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from WorkFlowActivityLogModel ").List<WorkFlowActivityLogModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestWorkFlowActivityLogQueryByActionName()
        {
            var insertEntity = new WorkFlowActivityLogModel
                                   {
                                       AppId = "AppId",
                                       OldID = "OldID",
                                       ApplicationState = "ApplicationState",
                                       AppName = "AppName",
                                       CreateUserId = "CreateUserId",
                                       CurrentWorkflowState = "CurrentWorkflowState",
                                       ForeWorkFlowState =  "ForeWorkflowState",
                                       OperatorActivity = "OperatorActivity",
                                       OperatorUserId = "OperatorUserId",
                                       OperatorUserList = "OperatorUserList",
                                       WorkflowName = "WorkflowName",
                                       CreateDateTime = DateTime.Now,
                                       Id = Guid.NewGuid().ToString(),
                                       LastUpdateDateTime = DateTime.Now
                                   };

            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                WorkFlowActivityLogModel entity = null;
                var resultList = session.CreateQuery("from WorkFlowActivityLogModel ").List<WorkFlowActivityLogModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit =
                    session.CreateCriteria(typeof(WorkFlowActivityLogModel))
                           .Add(Restrictions.Eq("AppId", entity.AppId));
                var result = crit.List<WorkFlowActivityLogModel>().First();

                Assert.AreEqual(result.AppId, entity.AppId);
            }
        }
    }
}
