/********************************************************************************
** Class Name:   Enums 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
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

    [Flags]
    [Serializable]
    public enum ActivityState
    {
        Save = 1,
        Edit = 2,
        Resubmit = 4,
        Submit = 8,
        Revoke = 16,
        Approve = 32,
        Reject = 64,
        Read = 128,
        Forward = 256
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

    /// <summary>
    /// Log Message Type enum
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// unknown type 
        /// </summary>
        Unknown,

        /// <summary>
        /// information type
        /// </summary>
        Information,

        /// <summary>
        /// User operation type
        /// </summary>
        UserOperation,

        /// <summary>
        /// warning type
        /// </summary>
        Warning,

        /// <summary>
        /// error type
        /// </summary>
        Error,

        /// <summary>
        /// Process runing
        /// </summary>
        Runing,

        /// <summary>
        /// success type
        /// </summary>
        Success
    }
}
