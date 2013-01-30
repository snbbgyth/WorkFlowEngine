﻿using System;
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


        /// <summary>
        /// Determines if this step or any of the enclosed steps
        /// in the local WorkflowSteps list matches the 
        /// specified stepId.
        /// </summary>
        /// <param name="stepId">String identifying a specific step in a workflow.</param>
        /// <returns>true if this step or any of the enclosed steps
        /// in the local WorkflowSteps list matches the 
        /// specified stepId.  false, otherwise</returns>
        internal virtual bool HasStep(string stepId)
        {
            if (StepId == stepId)
            {
                return true;
            }

            return false;
        }
    }
}
