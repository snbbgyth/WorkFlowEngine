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

        public WorkFlowState Execute(WorkFlowState workFlowState,ActivityState activityState)
        {
            return GetCurrentWorkFlowStateByWorkFlowState(workFlowState).Execute(activityState);
        }

        public ActivityState GetActivityStateByWorkFlowState(WorkFlowState workFlowState)
        {
            return GetCurrentWorkFlowStateByWorkFlowState(workFlowState).GetActivityState();
        }
    }
}
