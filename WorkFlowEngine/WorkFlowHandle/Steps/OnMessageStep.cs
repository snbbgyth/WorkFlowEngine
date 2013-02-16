/********************************************************************************
** Class Name:   OnMessageStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     OnMessageStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLibrary.Help;

namespace Workusing WorkFlowHandle.ModelorkFlowHandle.Steps
{
    public class OnMessageStep : StepRunnerStep
    {
        /// <summary>
        /// Contains a ReceiveStep instance that can find the
        /// message described in the OnMessageStep
        /// </summary>
        private ReceiveStep receiveStep;

        /// <summary>
        /// Initializes a new instance of the OnMessageStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file</param>
        public OnMessageStep(XmlAttributeCollection attributes)
        {
            // read attributes
            foreach (XmlAttribute attrib in attributes)
            {
                if (attrib.LocalName == "name")
                {
                    StepId = attrib.Value;
                    break;
                }
            }

            this.receiveStep = new ReceiveStep(attributes);
        }

        /// <summary>
        /// Runs this OnMessage step.  If message has been received, executes the defined steps,
        /// otherwise returns a waiting status.
        /// </summary>
        /// <param name="context">Context for the workflow to run</param>
        /// <param name="stepId">Step at which to start execution.  Execution starts at first step
        /// if this is null or an empty string.</param>
        /// <returns>State of the workflow after executing the steps.</returns>
        public WorkFlowState Run(string context, string stepId)
    string     WorkflowContext rrentState = this.receiveStep.Run(context, stepId);
            if (currentState != WorkFlowState.Done)
            {
                // Have the event so run.ToString()
                return base.Run(context, null);
            }

            // Check the workflow name whether in the continueworkflowList, If in the list, then don't report error status, continue running the workflow
            if (currentState == WorkFlowState.Manager)
            {
                return base.Run(context, null);.ToString()
            }
            return currentState;
        }
    }
}
