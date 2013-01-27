using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    public  class DoneState:IStateBase
    {
        public WorkFlowState Execute(ActivityState activityState)
        {
            return WorkFlowState.Done;
        }


        public WorkFlowState GetCurrentState()
        {
           return WorkFlowState.Done;
        }

        public ActivityState GetActivityState()
        {
           return ActivityState.Read;
        }
    }
}
