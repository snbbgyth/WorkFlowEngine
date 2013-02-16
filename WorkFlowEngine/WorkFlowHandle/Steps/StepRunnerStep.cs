/********************************************************************************
** Class Name:   StepRunnerStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     StepRunnerStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;

namespace Worusing WorkFlowHandle.ModelWorkFlowHandle.Steps
{
    public abstract class StepRunnerStep : WorkflowStep
    {
        private List<WorkflowStep> _workflowSteps;

        protected StepRunnerStep()
        {
            _workflowSteps = new List<WorkflowStep>();
        }

        public override WorkFlowState Run(string cstringg steWorkflowContext context, string stepId)
        {
          return WorkFlowState.Managersu.ToString()mmary>
        /// Gets a set of steps defined within this step
        /// </summary>
        public List<WorkflowStep> WorkflowSteps
        {
            get { return _workflowSteps; }
        }
    }
}
