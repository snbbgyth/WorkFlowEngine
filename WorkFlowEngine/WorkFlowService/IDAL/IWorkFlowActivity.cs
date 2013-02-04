/********************************************************************************
** Class Name:   IWorkFlowActivity 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     IWorkFlowActivity class
*********************************************************************************/

using System.Collections.Generic;
using CommonLibrary.Help;
using CommonLibrary.Model;

namespace WorkFlowService.IDAL
{
    public interface IWorkFlowActivity
    {
        WorkFlowState Execute(AppInfoModel entity);
        WorkFlowState NewWorkFlow(AppInfoModel entity);
        List<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId);
        ActivityState GetCurrentActivityStateByAppIdAndUserID(string appId, string userId);
    }
}
 