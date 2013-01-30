/********************************************************************************
** Class Name:   WorkFlowService 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowService class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CommonLibrary.Model;
using WorHelpibrary.Model;
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Help;

namespace WorkFlowWCFService
{

    // 注意: 使用�       public class WorkFlowService : IWorkFlowActivity
    {
        private IWorkFlowActivity WorkFlowEngineInstance
        {
            get { return new WorkFlowManage(); }
        }

        [OperationContract]
        public WorkFlowState Execute(AppInfoModel entity)
        {
            return WorkFlowEngineInstance.Execute(entity);
        }

        [OperationContract]
        public WorkFlowState NewWorkFlow(AppInfoModel entity)
        {
            return WorkFlowEngineInstance.NewWorkFlow(entity);
        }

        [OperationContract]
        public List<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId)
        {
            return WorkFlowEngineInstance.QueryInProgressActivityListByOperatorUserId(operatorUserId);
        }

        [OperationContract]
        public ActivityState GetCurrentActivityStateByAppIdAndUserID(string appId, string userId)
        {
            return WorkFlowEngineInstance.GetCurrentActivityStateByAppIdAndUserID(appId, userId);
        }
    }
}
