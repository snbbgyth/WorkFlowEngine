/********************************************************************************
** Class Name:   WorkflowExecutionEngine 
** Author：      Spring Yang
** Create date： 2013-1-1
** Modify：      Spring Yang
** Modify Date： 2013-2-17
** Summary：     WorkflowExecutionEngine class
*********************************************************************************/

using System.Linq;
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
            return (from stepRunnerStep in context.WorkflowStepList.OfType<StepRunnerStep>()
                    let subStep =
                        stepRunnerStep.WorkflowSteps.Find(entity => entity.StepId.CompareEqualIgnoreCase(currentState))
                    let invokeStep = subStep as InvokeStep
                    where invokeStep != null
                    select
                        stepRunnerStep.WorkflowSteps.Find(
                            entity => entity.StepId.CompareEqualIgnoreCase(invokeStep.InvokeContext.PortType)))
                .FirstOrDefault();
  
        }
    }
}