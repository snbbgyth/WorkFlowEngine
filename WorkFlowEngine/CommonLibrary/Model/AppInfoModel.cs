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
        public string TableName { get; set; }
        [DataMember]
        public string ActivityState { get; set; }

    }
}
