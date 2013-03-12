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
    public class OperationActionInfoModel:ITableModel
    {
        public string ID { get; set; }

        public string ActionName { get; set; }

        public string ActionDisplayName { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
