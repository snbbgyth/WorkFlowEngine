/********************************************************************************
** Class Name:   OperationActionInfoModel 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     OperationActionInfoModel class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.Model
{
    [Serializable]
    public class OperationActionInfoModel : ITableModel
    {
        public virtual string Id { get; set; }

        public virtual string ActionName { get; set; }

        public virtual string ActionDisplayName { get; set; }

        public virtual string WorkflowName { get; set; }

        public virtual string WorkflowDisplayName { get; set; }

        public virtual DateTime? CreateDateTime { get; set; }

        public virtual DateTime? LastUpdateDateTime { get; set; }

        public virtual bool IsDelete { get; set; }
    }
}
