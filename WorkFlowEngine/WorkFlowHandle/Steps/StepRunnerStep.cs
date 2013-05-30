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
using WorkFlowHandle.Model;

namespace WorkFlowHandle.Steps
{
    using IDAL;
    public abstract class StepRunnerStep : WorkflowStep,IStepRunnerStep
    {
        private List<IWorkflowStep> _workflowSteps;

        protected StepRunnerStep()
        {
            _workflowSteps = new List<IWorkflowStep>();
        }

        public override string Run(WorkflowContext context, string stepId)
        {
            foreach (var workflowStep in _workflowSteps)
            {
                return workflowStep.Run(context, stepId);
            }
            return string.Empty;
        }



        /// <summary>
        /// Gets a set of steps defined within this step
        /// </summary>
        public List<IWorkflowStep> WorkflowSteps
        {
            get { return _workflowSteps; }
            set { _workflowSteps = value; }
        }
    }
}
