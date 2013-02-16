/********************************************************************************
** Class Name:   PickStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     PickStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLibrary.Help;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.Steps
{
    public class PickStep : StepRunnerStep
    {
        /// <summary>
        /// Initializes a new instance of the PickStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file</param>
        public PickStep(XmlAttributeCollection attributes)
        {
            foreach (XmlAttribute attrib in attributes)
            {
                if (attrib.LocalName == "name")
                {
                    StepId = attrib.Value;
                }
            }
        }

        /// <summary>
        /// Executes the OnPick step
        /// Checks if an event or timeout has occurred and executes specified steps for the event found.
        /// Nested events are not supported! (This means that the steps in an onMessage step cannot 
        /// also wait for events.  
        /// </summary>
        /// <param name="context">Context for the workflow to run</param>
        /// <param name="stepId">Step at which to start execution.  Execution starts at first step
        /// if this is null or an empty string.</param>
        /// <returns>State of the workflow after executing the steps.</returns>
        public override Worstringn(WorkflowContext workflowContext, string stepId)
        {
            WorkFlowState state = WorkFlowState.Done;

            // Check for event first.  Go though the possible OnMessage activities and see if any succeed. 
            // If there is an event to process, we're done.  Only one event per execution (check BPEL spec)
            // If not event, start alarm timer. 



            return state;
 .ToString();
        }
    }
}
 