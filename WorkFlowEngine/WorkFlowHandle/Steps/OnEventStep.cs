/********************************************************************************
** Class Name:   OnEventStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     OnEventStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WorkFlowHandle.Steps
{
   public  class OnEventStep:StepRunnerStep
    {
        public string EventKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the OnMessageStep class
        /// </summary>
        /// <param name="attributes">Xml attributes from the BPEL file</param>
        public OnEventStep(XmlAttributeCollection attributes)
        { 
            // read attributes
            foreach (XmlAttribute attrib in attributes)
            {
                switch (attrib.LocalName)
                {
                    case "name":
                        StepId = attrib.Value;
                        break;
                    case "operation":
                        switch (attrib.Value)
                        {
                            case "messageTimeout":
                              //  EventType = WorkflowEventType.Timeout;
                                break;
                            case "cancel":
                               // EventType = WorkflowEventType.Cancel;
                                break;
                            default:
                               // var ex = ErrorAndExceptionService.CreateLocutusException(this, "E67.01", null);
                               // throw ex;
                                return;
                        }
                        break;
                    case "variable":
                        if (string.IsNullOrEmpty(attrib.Value))
                        {
                            
                        }
                        EventKey = attrib.Value;
                        break;
                }
            }
        }

    }
}
