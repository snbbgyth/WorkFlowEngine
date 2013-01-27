namespace WorkFlowService.Help
{
    public enum ApplicationState
    {
        Draft,
        InProgress,
        Complete,
    }

    public enum WorkFlowState
    {
        Common,
        Manager,
        Done,
        Refuse 
    }

    public enum ActivityState
    {
        Create,
        Submit,
        Revoke,
        Approve,
        Reject,
        Read,
        None
    }
}
