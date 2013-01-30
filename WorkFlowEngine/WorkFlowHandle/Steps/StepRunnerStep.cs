using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namusing CommonLibrary.Helpnamespace WorkFlowHandle.Steps
{
   public  public abstract class StepRunnerStep : WorkflowStep
    {
        private List<WorkflowStep> _workflowSteps;

        protected StepRunnerStep( )
        {
            _workflowSteps = new List<WorkflowStep>();
        }

        public overrideFlowState Run(string context, string stepId)
        {
            throw new NotImplementedException();
        }
    }
}

        /// <summary>
        /// Gets a set of steps defined within this step
        /// </summary>
        protected IEnumerable<WorkflowStep> WorkflowSteps
        {
            get { return _workflowSteps; }
        }
    }
}
