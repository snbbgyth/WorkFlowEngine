using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestCommunication.WFService;

namespace TestCommunication.Common
{
    public class BaseActivity
    {

        protected BPELWorkFlowServiceClient WfServiceInstance
        {
            get { return new BPELWorkFlowServiceClient(); }
        }
        [SetUp]
        public void Initialize()
        {
          //WfServiceInstance.ClearUnitTestData();
        }

        [TearDown]
        public void Finalize()
        {
          //WfServiceInstance.ClearUnitTestData();
        }
    }
}
