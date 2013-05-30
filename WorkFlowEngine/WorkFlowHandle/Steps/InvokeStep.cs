/********************************************************************************
** Class Name:   InvokeStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     InvokeStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLibrary.Help;

namespace WorkFlowHandle.Steps
{
    using Model;
    using IDAL;
    public class InvokeStep : WorkflowStep,IInvokeStep
    {
        public InvokeContextModel InvokeContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the InvokeStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file
        /// when invoking execution of workflow activities</param>
        public InvokeStep(XmlAttributeCollection attributes)
        {
            InvokeContext = new InvokeContextModel();
            // this.type = InvokeType.Unknown;
            foreach (XmlAttribute attrib in attributes)
            {
                switch (attrib.LocalName.ToLower())
                {
                    case "operation":
                        InvokeContext.Operation = attrib.Value;
                        break;
                    case "name":
                        InvokeContext.Name = attrib.Value;
                        StepId = attrib.Value;
                        break;
                    case "partnerlink":
                        InvokeContext.PartnerLink = attrib.Value;
                        break;
                    case "porttype":
                        InvokeContext.PortType = attrib.Value;
                        break;
                    case "inputvariable":
                        InvokeContext.InputVariable = attrib.Value;
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// Run the invoke workflow step.  This step will invoke execution
        /// of either a Workflow Code Block or a new workflow, depending
        /// on the value of the type field set during initialization
        /// from the BPEL attributes.
        /// </summary>
        /// <param name="context">Context for the workflow to run</param>
        /// <param name="stepId">Step at which to start execution.  This
        /// parameter is ignored for the Invoke step.</param>
        /// <returns>State of the workflow after executing the invoke step.</returns>
        public override string Run(WorkflowContext context, string stepId)
        {
            return InvokeContext.Name;
        }




       
    }
}
