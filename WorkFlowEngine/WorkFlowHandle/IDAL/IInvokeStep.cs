using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.IDAL
{
   public interface IInvokeStep:IWorkflowStep
    {
       InvokeContextModel InvokeContext { get; set; }
    }
}
