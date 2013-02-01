/********************************************************************************
** Class Name:   Enums 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     Enums class
*********************************************************************************/

using System;

namespace CommonLibrary.Help
{
    [Serializable]
    public enum ApplicationState
    {
        Draft,
        InProgress,
        Complete,
    }

    [Serializable]
    public enum WorkFlowState
    {
        Common,
        Manager,
        Done,
        Refuse
    }

    [Flags]
    [Serializable]
    public enum ActivityState
    {
        Create = 1,
        Edit = 2,
        Resbmit = 4,
        Submit = 8,
        Revoke = 16,
        Approve = 32,
        Reject = 64,
        Read = 128,
        None = 256
    }

    [Serializable]
    [Flags]
    public enum ActionState
    {
        Create = 1,
        Edit = 2,
        Delete = 4
    }


    public enum EventType
    {
        /// <summary>
        /// Used to specify any event type as criterion
        /// </summary>
        Any = 0,

        /// <summary>
        /// Subworkflow completion event
        /// </summary>
        SubWorkflowComplete = 1,

        /// <summary>
        /// Message event
        /// </summary>
        Message = 2,

        /// <summary>
        /// Timer event
        /// </summary>
        Timer = 3,

        /// <summary>
        /// Cancel event
        /// </summary>
        Cancel = 4
    }
}
