using System;

namespace WorkFlowService.Help
{
    public  class WFUntilHelp
    {
        public static ActivityState GetActivityStateByName(string activityState)
        {
            ActivityState result;
            if (Enum.TryParse(activityState, true, out result))
                return result;
            return ActivityState.Read;
        }

        public static WorkFlowState GetWorkFlowStateByName(string workFlowState)
        {
            WorkFlowState result;
            if (Enum.TryParse(workFlowState, true, out result))
                return result;
            return WorkFlowState.Common;
        }

        public static ApplicationState GetApplicationState(string applicationState)
        {
            ApplicationState result;
            if (Enum.TryParse(applicationState, true, out result))
                return result;
            return ApplicationState.Draft;
        }
    }
}
