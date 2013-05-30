using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;

namespace WorkFlowService.IDAL
{
    public interface IWorkFlowActivityLogDAL : IDataOperationActivity<WorkFlowActivityLogModel>
    {
        IList<WorkFlowActivityLogModel> QueryInProgressActivityByOperatorUserId(string operatorUserId);
        WorkFlowActivityLogModel QueryByAppId(string appId);
        WorkFlowActivityLogModel QueryByOldId(string oldId);
        IList<WorkFlowActivityLogModel> QueryByCondition(KeyValuePair<string, string> workflowParam,
                                                         KeyValuePair<string, object> conditionParam);
    }
}
