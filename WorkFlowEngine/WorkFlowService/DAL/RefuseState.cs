/********************************************************************************
** Class Name:   RefuseState 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     RefuseState class
*********************************************************************************/

using CommonLibrary.Help;
using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    public class RefuseState : IStateBase
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
