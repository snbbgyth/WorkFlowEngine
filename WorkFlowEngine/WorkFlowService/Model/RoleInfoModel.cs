/********************************************************************************
** Class Name:   RoleInfoModel 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     RoleInfoModel class
*********************************************************************************/

using System;
using CommonLibrary.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Model
{
    [Serializable]
    public class RoleInfoModel : ITableModel
    {
        public virtual string Id { get; set; }

        public virtual string RoleName { get; set; }

        public virtual string RoleDisplayName { get; set; }

        public virtual string WorkflowName { get; set; }

        public virtual string WorkflowDisplayName { get; set; }

        public virtual DateTime? CreateDateTime { get; set; }

        public virtual DateTime? LastUpdateDateTime { get; set; }

        public virtual bool IsDelete { get; set; }

    }
}
