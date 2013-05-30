using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IWorkflowStateInfoDAL : IDataOperationActivity<WorkflowStateInfoModel>
    {
        WorkflowStateInfoModel QueryByWorkflowNameAndStateNodeName(string workflowName, string stateNodeName);
    }
}
