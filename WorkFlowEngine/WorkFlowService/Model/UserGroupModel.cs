/********************************************************************************
** Class Name:   UserGroupModel 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     UserGroupModel class
*********************************************************************************/

using System;
using CommonLibrary.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Model
{
    public class UserGroupModel : ITableModel
    {
        public string ID { get; set; }

        public string GroupName { get; set; }

        public string GroupDisplayName { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
