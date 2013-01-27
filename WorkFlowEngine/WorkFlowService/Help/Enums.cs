/********************************************************************************
** Class Name:   Enums 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     Enums class
*********************************************************************************/

namespace WorkFlowService.Help
{
    using System;

    [Serializable]    public enum ApplicationState
    {
        Draft,
        InProgress,
        Complete,
    }

    public en[Serializable]lic enum WorkFlowState
    {
        Common,
        Manager,
        Done,
        Refuse 
    }

    public enum Activ[Serializable] ActivityState
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
