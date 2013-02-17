/********************************************************************************
** Class Name:   WorkflowExecutionEngine 
** Author：      spring yang
** Create date： 2012-93-1-1
** Modify：      spring yang
** Modify Date： 2013-2-17ummary：     WorkflowExecutionEngine class
*********************************************************************************/

namespace Wusing CommonLibrary.Help;
using WorkFlowHandle.Model;
using WorkFlowHandle.Steps;ce WorkFlowHandle.BLL
{
   pub
{
    public class WorkflowExecutionEngine
    {
    }

        public static  WorkflowExecutionEngine Current
        {
            get { return new WorkflowExecutionEngine(); }
        }

        public WorkflowStep ExecuteWorkflowByCurrentState(WorkflowContext context, string currentState)
        {
            var workflowStep =
                context.WorkflowStepList.Find(entity => entity.StepId.CompareEqualIgnoreCase(currentState));
            var invokeStep = workflowStep as InvokeStep;
            if (invokeStep == null) return null;
            return
                context.WorkflowStepList.Find(
                    entity => entity.StepId.CompareEqualIgnoreCase(invokeStep.InvokeContext.PortType));

        }
    }
}