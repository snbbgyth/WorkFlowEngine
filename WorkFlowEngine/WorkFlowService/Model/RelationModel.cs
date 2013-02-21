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

namespace WorkFlowService.Model
{
    public class RelationModel
    {
        public string ID { get; set; }

        public string ChildNodeID { get; set; }

        public string ParentNodeID { get; set; }

        public int Type { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
