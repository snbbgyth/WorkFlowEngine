using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using WorkFlowHandle.Model;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
   public interface IWorkFlowEngine
   {
       string Execute(string workflowName, string currentState, string activityState);
       void InitWorkflowState(string workflowName);
       ApplicationState GetAppStateByCondition(string workflowName, string workflowState);
       void AddStateRoleByCondition(string workflowName, string stateNodeName, PartnerLinkModel partnerLink);
       WorkflowStateInfoModel GetWorkflowStateInfoByCondition(string workflowName, string stateNodeName);
       IEnumerable<string> GetActivityStateByConditon(string workflowName, string workFlowState);
   }
}
