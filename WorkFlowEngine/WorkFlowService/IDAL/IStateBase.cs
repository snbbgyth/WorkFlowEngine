/********************************************************************************
** Class Name:   IStateBase 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     IStateBase class
*********************************************************************************/


using CommonLibrary.Help;

namespace WorkFlowService.IDAL
{ 
    public interface IStateBase : IActivity, IActivityState
    {
        WorkFlowState GetCurrentState();
    }
}
