/********************************************************************************
** Class Name:   WorkflowHandle 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     WorkflowHandle class
*********************************************************************************/

using System;
using System.Collections.Generic;

namespace WorkFlowHandle.BLL
{
    using Model;
    using DAL;
    using IDAL;
    using Steps;

    public class WorkflowHandle:IWorkflowHandle
    {
        private static ProcessDefinitionEngine _workflowProcessDefinition;

        public WorkflowHandle()
        {
            _workflowProcessDefinition = new ProcessDefinitionEngine();
        }

        private static WorkflowHandle _workflowHandle;

        private static readonly object SyncObj = new object();

        public static IWorkflowHandle Instance
        {
            get
            {
                lock (SyncObj)
                {
                    if (_workflowHandle == null)
                        _workflowHandle = new WorkflowHandle();
                }
                return _workflowHandle;
            }
        }

        public string Run(string workflowName, string currentState, string actionName)
        {
            var onContext = GetWorkflowContextByWorkflowName(workflowName);
            var workflowStep = WorkflowExecutionEngine.Current.ExecuteWorkflowByCurrentState(onContext, currentState);
            return workflowStep == null ? string.Empty : workflowStep.Run(onContext, actionName);
        }


        public WorkflowContext GetWorkflowContextByWorkflowName(string workflowName)
        {
            var onContext = new WorkflowContext(workflowName, Guid.NewGuid().ToString());
            _workflowProcessDefinition.LoadNewWorkflow(onContext);
            return onContext;
        }

        public List<WorkflowStep> GetInvokeStepByWorkflowName(string workflowName)
        {
            var onContext = GetWorkflowContextByWorkflowName(workflowName);
            return onContext.WorkflowStepList.FindAll(entity => entity is InvokeStep);
        }

    }
}
