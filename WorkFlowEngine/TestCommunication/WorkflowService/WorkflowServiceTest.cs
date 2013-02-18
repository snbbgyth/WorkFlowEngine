using TestCommunication.WFService;

namespace TestCommunication.WorkflowService
{
    using Common;
    using NUnit.Framework;

    public class WorkflowServiceTest : BaseActivity
    {
        private WorkFlowServiceClient WfServiceInstance
        {
            get { return new WorkFlowServiceClient(); }
        }

        [Test]
        public void TestNewWorkFlow()
        {
            var appEntity = new AppInfoModel
                                {
                                    ActivityState = ActivityState.Submit.ToString(),
                                    AppId = "001",
                                    WorkflowName = "TestStateWorkFlow",
                                    UserId = "002"
                                };
           var result= WfServiceInstance.NewWorkFlow(appEntity);
            Assert.Equals(result,"");
        }
    }
}

　