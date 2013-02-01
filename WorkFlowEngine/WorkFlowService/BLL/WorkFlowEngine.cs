/********************************************************************************
** Class Name:   WorkFlowEngine 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowEngine class
*********************************************************************************/

using CommonLibrary.Help;
using WorkFlowService.DAL;
using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    public class WorkFlowEngine
    {
        private IStateBase GetCurrentWorkFlowStateByWorkFlowState(WorkFlowState workFlowState)
        {
            foreach (var iStateBase in StateMapping.Instance.IStateBasesList)
            {
                if (iStateBase.GetCurrentState() == workFlowState) return iStateBase;
            }
            return new CommonState();
        }

        public WorkFlowState Execute(WorkFlowState workFlowState, ActivityState activityState)
        {
            return GetCurrentWorkFlowStateByWorkFlowState(workFlowState).Execute(activityState);
        }

        public ActivityState GetActivityStateByWorkFlowState(WorkFlowState workFlowState)
        {
            return GetCurrentWorkFlowStateByWorkFlowState(workFlowState).GetActivityState();
        }
    }
}
