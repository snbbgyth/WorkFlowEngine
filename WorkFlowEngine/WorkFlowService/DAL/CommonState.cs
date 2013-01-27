using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    public class CommonState:IStateBase
    {
       public WorkFlowState Execute(ActivityState activityState)
        {
           if(activityState==ActivityState.Submit)
            return WorkFlowState.Manager;
           return WorkFlowState.Common;
        }


       public WorkFlowState GetCurrentState()
       {
          return WorkFlowState.Common;
       }


       public ActivityState GetActivityState()
       {
          return ActivityState.Submit|ActivityState.Read|ActivityState.Revoke;
       }
    }
}
