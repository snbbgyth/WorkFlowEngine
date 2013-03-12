/********************************************************************************
** Class Name:   WorkFlowActivityModel
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowActivityModel class
*********************************************************************************/

using System;
using System.Runtime.Serialization;
using CommonLibrary.IDAL;

namespace CommonLibrary.Model
{
    [DataContract]
    public class WorkFlowActivityModel : ITableModel
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string AppId { get; set; }
        [DataMember]
        public string WorkflowName { get; set; }
        [DataMember]
        public string ForeWorkflowState { get; set; }
        [DataMember]
        public string OperatorActivity { get; set; }
        [DataMember]
        public string CurrentWorkflowState { get; set; }
        [DataMember]
        public string OperatorUserId { get; set; }
        [DataMember]
        public DateTime? CreateDateTime { get; set; }
        [DataMember]
        public DateTime? LastUpdateDateTime { get; set; }
        [DataMember]
        public string CreateUserId { get; set; }
        [DataMember]
        public string OperatorUserList { get; set; }
        [DataMember]
        public string ApplicationState { get; set; }
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
    }
}

