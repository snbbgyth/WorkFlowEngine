/********************************************************************************
** Class Name:   UserInfoModel 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     UserInfoModel class
*********************************************************************************/

using System;
using CommonLibrary.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Model
{
    public class UserInfoModel : ITableModel
    {
        public virtual string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string UserDisplayName { get; set; }

        public virtual string Password { get; set; }

        public virtual DateTime? CreateDateTime { get; set; }

        public virtual DateTime? LastUpdateDateTime { get; set; }

        public virtual bool IsDelete { get; set; }
    }
}
