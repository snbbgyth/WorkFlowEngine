/********************************************************************************
** Class Name:   TestStateWorkFlowTest
** Author：      spring yang
** Create date： 2013-3-13
** Modify：      spring yang
** Modify Date： 2012-3-13
** Summary：     TestStateWorkFlowTest interface
*********************************************************************************/

namespace TestCommunication.WorkflowService
{
    using Common;
    using NUnit.Framework;
    using WFService;

    public class TestStateWorkFlowTest : BaseActivity
    {
        private WorkFlowServiceClient WfServiceInstance
        {
            get { return new WorkFlowServiceClient(); }
        }

        [Test]
        public void TestNewWorkflow()
        {
            var appEntity = new AppInfoModel
                                {
                                    ActivityState = ActivityState.Submit.ToString(),
                                    AppId = "001",
                                    WorkflowName = "TestStateWorkFlow",
                                    UserId = "001",
                                    CurrentState = "Common"
                                };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

        }


        [Test]
        public void TestManageApproveWorkflow()
        {
            var appEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Submit.ToString(),
                AppId = "002",
                WorkflowName = "TestStateWorkFlow",
                UserId = "002",
                CurrentState = "Common"
            };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var manageEntity = new AppInfoModel
                                   {
                                       ActivityState = ActivityState.Approve.ToString(),
                                       AppId = "002",
                                       WorkflowName = "TestStateWorkFlow",
                                       UserId = "003",
                                       CurrentState = "Manage"
                                   };
            var manageResult = WfServiceInstance.Execute(manageEntity);
            Assert.AreEqual(manageResult, "Done");
        }

        [Test]
        public void TestManageRejectWorkFlow()
        {
            var appEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Submit.ToString(),
                AppId = "004",
                WorkflowName = "TestStateWorkFlow",
                UserId = "004",
                CurrentState = "Common"
            };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var manageEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Reject.ToString(),
                AppId = "004",
                WorkflowName = "TestStateWorkFlow",
                UserId = "005",
                CurrentState = "Manage"
            };
            var manageResult = WfServiceInstance.Execute(manageEntity);
            Assert.AreEqual(manageResult, "Refuse");
        }


        [Test]
        public void TestSaveWorkflow()
        {
            var appEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Save.ToString(),
                AppId = "006",
                WorkflowName = "TestStateWorkFlow",
                UserId = "006",
                CurrentState = "Common"
            };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Common");
        }


        [Test]
        public void TestRevokeWorkflow()
        {
            var appEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Submit.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
                CurrentState = "Common"
            };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var commonEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Revoke.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
                CurrentState = "Common"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");
        }

        [Test]
        public void TestResubmitWorkflow()
        {
            var appEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Submit.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
                CurrentState = "Common"
            };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var commonEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Revoke.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
                CurrentState = "Common"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Resubmit.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
                CurrentState = "Common"
            };
            var lastResult = WfServiceInstance.Execute(resubmitEntity);
            Assert.AreEqual(lastResult, "Manage");
        }
    }
}

