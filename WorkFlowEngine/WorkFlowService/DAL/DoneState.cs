/********************************************************************************
** Class Name:   DoneState 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     DoneState class
*********************************************************************************/

using WorkFlowSCommonLibrary.Help;orkFlowService.Help;
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
