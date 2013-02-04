/********************************************************************************
** Class Name:   UserRoleInfoModel 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserRoleInfoModel class
*********************************************************************************/

using System;

namespace WorkFlowService.Model
{
    public class UserRoleInfoModel
    {
        public string ID { get; set; }

        public string UserID { get; set; }

        public string OperatorState { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }

    }
}
