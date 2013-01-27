using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    public class ManageState : IStateBase
    {
        public WorkFlowState Execute(ActivityState activityState)
        {
            if (activityState == ActivityState.Approve)
                return WorkFlowState.Done;
            return WorkFlowState.Refuse;
        }


        public WorkFlowState GetCurrentState()
        {
           return WorkFlowState.Manager;
        }

        public ActivityState GetActivityState()
        {
            return ActivityState.Approve | ActivityState.Reject|ActivityState.Read;
        }
    }
}
