using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.IDAL
{
   public interface IWorkflowExecutionEngine
   {
       IWorkflowStep ExecuteWorkflowByCurrentState(WorkflowContext context, string currentState);
   }
}
