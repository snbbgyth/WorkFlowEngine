using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namusing CommonLibrary.Helpnamespace WorkFlowHandle.Steps
{
    public abstract class WorkflowStep
    {

        public string StepId { get; set; }
    }
}

        /// <summary>
        /// Used to run a workflow step
        /// </summary>
        /// <param name="context">Context for the workflow</param>
        /// <param name="stepId">Step at which to start execution.  Ignored</param>
        /// <returns>State of the workflow after executing the steps.</returns>
        public abstract WorkFlowState Run(string context, string stepId);
    }
}
