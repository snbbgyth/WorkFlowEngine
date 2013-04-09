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
        public virtual string Id { get; set; }

        public virtual string GroupName { get; set; }

        public virtual string GroupDisplayName { get; set; }

        public virtual DateTime? CreateDateTime { get; set; }

        public virtual DateTime? LastUpdateDateTime { get; set; }

        public virtual bool IsDelete { get; set; }
    }
}
