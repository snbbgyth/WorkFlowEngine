/********************************************************************************
** Class Name:   FaultHandler 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     FaultHandler class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace WorkFlowHandle.Steps
{
    public class FaultHandler : StepRunnerStep
    {
        /// <summary>
        /// Initializes a new instance of the FaultHandler class
        /// </summary>
        /// <param name="xmlNode">The XML child node of the FaultHandler BPEL node.
        /// This child node contains attribures of the FaultHandler node.</param>
        public FaultHandler(IXPathNavigable xmlNode)
        {
            XmlNode childNode = xmlNode as XmlNode;
            if (xmlNode != null)
            {
                if (childNode.LocalName == "catchAll")
                {
                    StepId = childNode.Name;
                }
                else
                {
                    // parse node and save relavent info
                    foreach (XmlAttribute attrib in childNode.Attributes)
                    {
                        if (attrib.LocalName == "faultName")
                        {
                            StepId = attrib.Value;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Executes the FaultHandler step.
        /// </summary>
        /// <param name="context">A WorkflowContext containing data and state of the
        /// executing workflow.</param>
        /// <param name="exception">The exception that caused this FaultHandler to
        /// be invoked.</param>
        /// <returns>A value indicating whether ???</returns>
        public bool Run(string context, Exception exception)
        {
            if (StepId == "catchAll")
            {
                // special case to handle all exceptions
                if (exception != null)
                {
                    //Debug.WriteLine("FaultHandler for Workflow " + context.Name + ":" + context.Id +
                    //    " received a " + exception.GetType().ToString() + " : " +
                    //    exception.Message);
                    //context.SetWorkflowParameter("faultException", exception);
                    this.Run(context, null);
                }
                else
                {
                    //Debug.WriteLine("FaultHandler for Workflow " + context.Name + ":" + context.Id +
                    //    " received a null exception");
                }

                return true;
            }
            else
            {
                // fault handler for a specific type? of exception
                // ToDo: Implement logic that checks if this is the correct
                // handler for this exception
            }

            return false;
        }
    }
}
