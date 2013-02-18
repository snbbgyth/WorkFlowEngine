/********************************************************************************
** Class Name:   SequenceStep 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     SequenceStep class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WorkFlowHandle.Steps
{
    using Model;
    public class SequenceStep : StepRunnerStep
    {
        public SequenceContextModel SequenceContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the SequenceStep class
        /// </summary>
        /// <param name="attributes">The XML attributes associated with the
        /// Sequence Step in the BPEL file.</param>
        public SequenceStep(XmlAttributeCollection attributes)
        {
            // read attributes
            if (attributes != null)
            {
                foreach (XmlAttribute attrib in attributes)
                {
                    if (attrib.LocalName == "name")
                    {
                        SequenceContext.Name = attrib.Name;
                        break;
                    }
                }
            }
        }
    }
}
