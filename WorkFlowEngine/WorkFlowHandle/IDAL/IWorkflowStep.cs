using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.IDAL
{
   public interface IWorkflowStep
    {
       string StepId { get; set; }
       string Run(WorkflowContext context, string stepId);
       bool HasStep(string stepId);
    }
}
