using System.Collections.Generic;
using CommonLibrary.Model;
using WorkFlowService.Help;
 

namespace WorkFlowService.IDAL
{
 
  public  interface IWorkFlowActivity
    {
        WorkFlowState Execute(AppInfoModel entity);
        WorkFlowState NewWorkFlow(AppInfoModel entity);
        List<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId);
        ActivityState GetCurrentActivityStateByAppIdAndUserID(string appId, string userId);
    }
}
