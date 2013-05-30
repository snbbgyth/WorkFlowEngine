using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IOperationActionInfoDAL : IDataOperationActivity<OperationActionInfoModel>
   {
       OperationActionInfoModel QueryByCondition(string workflowName, string actionName);
   }
}
