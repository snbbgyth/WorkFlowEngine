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
   public class InvokeStep:StepRunnerStep
   {
        public InvokeContextModel InvokeContext { get; set; }
       
 

        /// <summary>
        /// Initializes a new instance of the InvokeStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file
        /// when invoking execution of workflow activities</param>
        public InvokeStep(XmlAttributeCollection attributes)
        {
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
                        break;
                    case  "partnerLink":
                        InvokeContext.PartnerLink = attrib.Value;
                        break;
                    case "portType":
                        InvokeContext.PortType = attrib.Value;
                        break;
                    case "inputVariable":
                        InvokeContext.InputVariable = attrib.Value;
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
        public   WorkFlowState Run(string  context, string stepId)
        {
 

                  return WorkFlowState.Manager;
        }

 
     
    }
}
