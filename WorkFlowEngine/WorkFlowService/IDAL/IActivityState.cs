/********************************************************************************
** Class Name:   IActivityState 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     IActivityState class
*********************************************************************************/

using WorkFlowService.Help;

namespace WorkFlowService.IDAL
{
    public interface IActivityState
    {
        ActivityState GetActivityState();
    }
}
