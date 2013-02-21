/********************************************************************************
** Class Name:   RoleInfoModel 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     RoleInfoModel class
*********************************************************************************/

using System;

namespace WorkFlowService.Model
{
    public class RoleInfoModel
    {
        public string ID { get; set; }

        public string RoleName { get; set; }

        public string RoleDisplayName { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }

    }
}
