/********************************************************************************
** Class Name:   RelationModel 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     RelationModel class
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
    public class RelationModel : ITableModel
    {
        public virtual string Id { get; set; }

        public virtual string ChildNodeID { get; set; }

        public virtual string ParentNodeID { get; set; }

        public virtual int Type { get; set; }

        public virtual DateTime? CreateDateTime { get; set; }

        public virtual DateTime? LastUpdateDateTime { get; set; }

        public virtual bool IsDelete { get; set; }
    }
}
