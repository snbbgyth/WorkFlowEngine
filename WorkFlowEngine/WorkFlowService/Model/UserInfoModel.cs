/********************************************************************************
** Class Name:   UserInfoModel 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserInfoModel class
*********************************************************************************/

using System;

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
