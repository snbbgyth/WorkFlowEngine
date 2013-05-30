using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.IDAL
{
   public interface IWorkflowHandle
   {
       string Run(string workflowName, string currentState, string actionName);
       List<IWorkflowStep> GetInvokeStepByWorkflowName(string workflowName);
       WorkflowContext GetWorkflowContextByWorkflowName(string workflowName);
   }
}
