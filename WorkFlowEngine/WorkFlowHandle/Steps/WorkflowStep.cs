/********************************************************************************
** Class Name:   WorkflowStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowStep class
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
    public abstract class WorkflowStep:IWorkflowStep
    {

        public string StepId { get; set; }

        /// <summary>
        /// Used to run a workflow step
        /// </summary>
        /// <param name="context">Context for the workflow</param>
        /// <param name="stepId">Step at which to start execution.  Ignored</param>
        /// <returns>State of the workflow after executing the steps.</returns>
        public abstract string Run(WorkflowContext context, string stepId);


        /// <summary>
        /// Determines if this step or any of the enclosed steps
        /// in the local WorkflowSteps list matches the 
        /// specified stepId.
        /// </summary>
        /// <param name="stepId">String identifying a specific step in a workflow.</param>
        /// <returns>true if this step or any of the enclosed steps
        /// in the local WorkflowSteps list matches the 
        /// specified stepId.  false, otherwise</returns>
        public virtual bool HasStep(string stepId)
        {
            if (StepId == stepId)
            {
                return true;
            }

            return false;
        }
    }
}
