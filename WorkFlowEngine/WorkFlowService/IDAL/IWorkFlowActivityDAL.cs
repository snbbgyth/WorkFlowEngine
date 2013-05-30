using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;

namespace WorkFlowService.IDAL
{
    public interface IWorkFlowActivityDAL : IDataOperationActivity<WorkFlowActivityModel>
    {
        IList<WorkFlowActivityModel> QueryInProgressActivityByOperatorUserId(string operatorUserId);
        WorkFlowActivityModel QueryByAppId(string appId);

        IList<WorkFlowActivityModel> QueryByCondition(KeyValuePair<string, string> workflowParam,
                                                      KeyValuePair<string, object> conditionParam);
    }
}
