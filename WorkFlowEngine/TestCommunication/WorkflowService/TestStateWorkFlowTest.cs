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
                                    ActivityState = ActivityState.Submit.ToStri"Submit"            AppId = "001",
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
           "Submit",
                AppId = "002;
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Asse         };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var manageEntity = new AppInfoModel
                                   {
                                       ActivityState = ActivityState.Approve.ToString(),
                     "Approve"
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
      "Submit"w",
                UserId = "005",
                CurrentState = "Manage"
            };
            var manageRes4        };
            var result = WfServiceInstance.NewWorkFlow(appEntity);
            Assert.AreEqual(result, "Manage");

            var manageEntity = new AppInfoModel
                                   {
                             {
                ActivityState ="Reject"w",
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
                WorkflowName = "Te"Save"    UserId = "006",
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
                WorkflowName = "TestStateWorkFlo"Submit"ommon"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoMode }


        [Test]
        public void TestRevokeWorkflow()
        {
            var         AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
 "Revoke"ommon"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Resubmit.ToString(),
                AppId = "0    {
                ActivityState = ActivityState.Submit.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId "Submit"ommon"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoMode }


        [Test]
        public void TestRevokeWorkflow()
        {
            var         AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
  "Revoke"ommon"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoModel
            {
                ActivityState = ActivityState.Resubmit.ToString(),
                AppId = "007",
                WorkflowName = "TestStateWorkFlow",
                UserId = "007",
           "Resubmit"ommon"
            };
            var revokeResult = WfServiceInstance.Execute(commonEntity);
            Assert.AreEqual(revokeResult, "Common");

            var resubmitEntity = new AppInfoMo