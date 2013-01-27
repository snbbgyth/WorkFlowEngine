using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    public class RefuseState:IStateBase
    {
        public WorkFlowState Execute(ActivityState activityState)
        {
          return WorkFlowState.Refuse;
        }

        public WorkFlowState GetCurrentState()
        {
            return WorkFlowState.Refuse;
        }

        public ActivityState GetActivityState()
        {
            return ActivityState.Read;
        }
    }
}
