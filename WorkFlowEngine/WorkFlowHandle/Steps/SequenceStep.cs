using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namusing System.Xml;

namespace WorkFlowHandle.Steps
{
   public class SequenceStep:StepRunnerStep
    {
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
                        StepId = attrib.Value;
                        break;
                    }
                }
            }
        }
    }
}
