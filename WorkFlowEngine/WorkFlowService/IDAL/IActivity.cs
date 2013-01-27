using WorkFlowService.Help;

namespace WorkFlowService.IDAL
{
    public  interface IActivity
    {
        WorkFlowState Execute(ActivityState activityState);
    }
}
