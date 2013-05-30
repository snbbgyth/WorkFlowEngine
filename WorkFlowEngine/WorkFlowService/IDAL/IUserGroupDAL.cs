using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IUserGroupDAL : IDataOperationActivity<UserGroupModel>
   {
       UserGroupModel QueryByGroupName(string groupName);
   }
}
