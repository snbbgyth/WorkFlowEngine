using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;

namespace WorkFlowHandle.Steps
{
    public abstract class StepRunnerStep : WorkflowStep
    {
        private List<WorkflowStep> _workflowSteps;

        protected StepRunnerStep()
        {
            _workflowSteps = new List<WorkflowStep>();
        }

        public override WorkFlowState Run(string context, string stepId)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Gets a set of steps defined within this step
        /// </summary>
        public List<WorkflowStep> WorkflowSteps
        {
            get { return _workflowSteps; }
        }
    }
}
