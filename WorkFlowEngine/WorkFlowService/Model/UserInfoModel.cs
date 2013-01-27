using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowService.Model
{
   public  class UserInfoModel
    {
       public string ID { get; set; }

       public string UserName { get; set; }

       public string Password { get; set; }

       public DateTime? CreateDateTime { get; set; }

       public DateTime? LastUpdateDateTime { get; set; }

       public bool IsDelete { get; set; }
    }
}
