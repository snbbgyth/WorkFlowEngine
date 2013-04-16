/********************************************************************************
** Class Name:   WorkFlowActivityLogModel
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowActivityLogModel class
*********************************************************************************/

namespace CommonLibrary.Model
{
    using System;
    using System.Runtime.Serialization;
    using IDAL;
    [Serializable]
    public class WorkFlowActivityLogModel : ITableModel
    {
        [DataMember]
        public virtual string Id { get; set; }
        [DataMember]
        public virtual string OldID { get; set; }
        [DataMember]
        public virtual string AppId { get; set; }
        [DataMember]
        public virtual string WorkflowName { get; set; }
        [DataMember]
        public virtual string ForeWorkFlowState { get; set; }
        [DataMember]
        public virtual string OperatorActivity { get; set; }
        [DataMember]
        public virtual string CurrentWorkflowState { get; set; }
        [DataMember]
        public virtual string OperatorUserId { get; set; }
        [DataMember]
        public virtual DateTime? CreateDateTime { get; set; }
        [DataMember]
        public virtual DateTime? LastUpdateDateTime { get; set; }
        [DataMember]
        public virtual string CreateUserId { get; set; }
        [DataMember]
        public virtual string OperatorUserList { get; set; }
        [DataMember]
        public virtual string ApplicationState { get; set; }
        [DataMember]
        public virtual string AppName { get; set; }
        [DataMember]
        public virtual bool IsDelete { get; set; }
    }
}
