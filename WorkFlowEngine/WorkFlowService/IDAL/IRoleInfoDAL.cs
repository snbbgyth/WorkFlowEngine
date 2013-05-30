using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IRoleInfoDAL : IDataOperationActivity<RoleInfoModel>
    {
        RoleInfoModel QueryByCondition(string workflowName, string roleName);
    }
}
