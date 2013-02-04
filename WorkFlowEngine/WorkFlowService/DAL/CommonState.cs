/********************************************************************************
** Class Name:   CommonState 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     CommonState class
*********************************************************************************/

using CommonLibrary.Help;
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
