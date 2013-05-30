using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.IDAL
{
    public interface ICaseStep:IStepRunnerStep
    {
        CaseContextModel CaseContext { get; set; }
        bool IsConditionTrue(WorkflowContext context);
    }
}
