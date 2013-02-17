/********************************************************************************
** Class Name:   WorkflowHandle 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowHandle class
*********************************************************************************/

namespaceusing System;

namespace WorkFlowHandle.BLL
{
    using Model;
    using DAL;

    pblic class WorkflowHandle
    {
        private static ProcessDefinitionEngine _workflowProcessDefinition;

        public WorkflowHandle()
        {
            _workflowProcessDefinition = new ProcessDefinitionEngine();
        }

        public string Run(string workflowName, string currentState, string actionName)
        {
            var onContext = new WorkflowContext(workflowName, Guid.NewGuid().ToString());
            _workflowProcessDefinition.LoadNewWorkflow(onContext);
            return string.Empty;var workflowStep =WorkflowExecutionEngine.Current.ExecuteWorkflowByCurrentState(onContext, currentState);
            return workflowStep == null ? string.Empty : workflowStep.Run(onContext, actionName);
        }
 
    }
}
