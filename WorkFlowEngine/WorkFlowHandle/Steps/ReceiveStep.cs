/********************************************************************************
** Class Name:   ReceiveStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     ReceiveStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLibrary.Help;

namespace WorkFlowHandle.Steps
{
    public class ReceiveStep : WorkflowStep
    {
        public override WorkFlowState Run(string context, string stepId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The BPEL variable defined in the BPEL file for this Receive step.
        /// This string is used to save the received event as a context parameter.
        /// </summary>
        private string variableName;



        /// <summary>
        /// The message name if the eventType is a MessageEvent.
        /// This name is used when trying to find a received event.
        /// </summary>
        private string _messageName;

        /// <summary>
        /// The message name if the eventType is a MessageEvent.
        /// This name is used when trying to find a received event.
        /// </summary>
        public string MessageName
        {
            get
            {
                return _messageName;
            }
            set
            {
                _messageName = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ReceiveStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file</param>
        public ReceiveStep(XmlAttributeCollection attributes)
        {
            if (attributes != null)
            {
                foreach (XmlAttribute attrib in attributes)
                {
                    if (attrib.LocalName == "name")
                    {
                        StepId = attrib.Value;
                    }
                    else if (attrib.LocalName == "variable")
                    {
                        this.variableName = attrib.Value;
                    }
                }
            }


            if (this.variableName.Equals("subWorkflowComplete", StringComparison.OrdinalIgnoreCase))
            {

            }
            else
            {
                // message name so use variableName in search criteria
                this._messageName = this.variableName;
            }
        }

    }
}
