/********************************************************************************
** Class Name:   WorkflowExecutionEngine 
** Author：      Spring Yang
** Create date： 2013-1-1
** Modify：      Spring Yang
** Modify Date： 2013-2-17
** Summary：     WorkflowExecutionEngine class
*********************************************************************************/

using CommonLibrary.Help;
using WorkFlowHandle.Model;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.BLL
{
    public class WorkflowExecutionEngine
    {

        public static WorkflowExecutionEngine Current
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