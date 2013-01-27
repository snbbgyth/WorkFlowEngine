using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowService.Model
{
    public class UserRoleInfoModel
    {
        public string ID { get; set; }

        public string UserID { get; set; }

        public string OperatorState { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public bool IsDelete { get; set; }

    }
}
