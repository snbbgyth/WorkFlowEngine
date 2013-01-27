using WorkFlowService.Help;

namespace WorkFlowService.IDAL
{
    public interface IStateBase : IActivity, IActivityState
    {
        WorkFlowState GetCurrentState();
    }
}
