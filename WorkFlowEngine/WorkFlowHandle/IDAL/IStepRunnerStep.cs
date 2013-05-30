using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.IDAL
{
   public interface IStepRunnerStep:IWorkflowStep
    {
       List<IWorkflowStep> WorkflowSteps { get; set; }
    }
}
