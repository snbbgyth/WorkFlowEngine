/********************************************************************************
** Class Name:   WhileStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WhileStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLibrary.Help;

namespace WorkFlowHandle.Steps
{
    public class WhileStep : StepRunnerStep
    {
        /// <summary>
        /// A string containing the condition text
        /// </summary>
        private string condition;

        /// <summary>
        /// Initializes a new instance of the WhileStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file</param>
        public WhileStep(XmlAttributeCollection attributes)
            : base()
        {
            // read attributes
            foreach (XmlAttribute attrib in attributes)
            {
                if (attrib.LocalName == "name")
                {
                    StepId = attrib.Value;
                }
                else if (attrib.LocalName == "condition")
                {
                    this.condition = attrib.Value;
                }
            }
        }

        /// <summary>
        /// Executes the while step.  
        /// </summary>
        /// <param name="context">Context for the workflow to run</param>
        /// <param name="stepId">Step at which to start execution.  Execution starts at first step
        /// if this is null or an empty string.</param>
        /// <returns>State of the workflow after executing the steps.</returns>
        public override WorkFlowState Run(string context, string stepId)
        {
            WorkFlowState currentState = WorkFlowState.Done;
            if (!String.IsNullOrEmpty(stepId))
            {
                // we must be restarting after an event
                // execute from where we left off and then perform while logic as normal
                currentState = base.Run(context, stepId);
                if (currentState != WorkFlowState.Done)
                {
                    return currentState;
                }
            }

            while (this.IsConditionTrue(context))
            {
                currentState = base.Run(context, stepId);
                if (currentState != WorkFlowState.Done)
                {
                    return currentState;
                }
            }

            return currentState;
        }

        /// <summary>
        /// evaluate the condition and return true/false
        /// e.g. getVariableData('isLastUamReplica') = 'True'
        /// </summary>
        /// <param name="context">WorkflowContext of executing workflow</param>
        /// <returns>True if condition is met, false otherwise</returns>
        public bool IsConditionTrue(string context)
        {
            return true;
        }
        /// <summary>
        /// Gets progress weight of this step
        /// </summary>
        public int ProgressWeight
        {
            get
            {
                return 3;
            }
        }
    }
}
