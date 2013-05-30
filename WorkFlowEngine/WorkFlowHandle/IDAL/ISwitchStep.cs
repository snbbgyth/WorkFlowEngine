using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.IDAL
{
   public interface ISwitchStep:IStepRunnerStep
    {
       SwitchContextModel SwitchContext { get; set; }
    }
}
