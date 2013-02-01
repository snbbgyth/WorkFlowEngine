/********************************************************************************
** Class Name:   ManageState 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     ManageState class
*********************************************************************************/

using CommonLibrary.Help;
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
            return ActivityState.Approve | ActivityState.Reject | ActivityState.Read;
        }
    }
}
