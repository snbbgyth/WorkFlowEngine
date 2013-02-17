/********************************************************************************
** Class Name:   AppInfoModel
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     AppInfoModel class
*********************************************************************************/

using System.Runtime.Serialization;

namespace CommonLibrary.Model
{
    [DataContract]
    public class AppInfoModel
    {
        [DataMember]
        public string AppId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string WorkflowName { get; set; }
        [DataMember]
        public string ActivityState { get; set; }




    }
}
