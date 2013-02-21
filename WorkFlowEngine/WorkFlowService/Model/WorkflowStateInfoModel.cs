/********************************************************************************
** Class Name:   WorkflowStateInfoModel 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     WorkflowStateInfoModel class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowService.Model
{
    public class WorkflowStateInfoModel
    {
        public string ID { get; set; }

        public string WorkflowName { get; set; }

        public string WorkflowDisplayName { get; set; }

        public string StateNodeName { get; set; }

        public string StateNodeDisplayName { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
