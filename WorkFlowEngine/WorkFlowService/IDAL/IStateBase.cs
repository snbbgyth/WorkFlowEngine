/********************************************************************************
** Class Name:   IStateBase 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     IStateBase class
*********************************************************************************/

using WorkFlowSCommonLibrary.Help;orkFlowService.Help;

namespace WorkFlowService.IDAL
{
    public interface IStateBase : IActivity, IActivityState
    {
        WorkFlowState GetCurrentState();
    }
}
