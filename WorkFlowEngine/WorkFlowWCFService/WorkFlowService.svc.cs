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
using CommonLibrary.Help;
using CommonLibrary.Model;
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Help;

namespace WorkFlowWCFService
{

    [ServiceContract]
    public class WorkFlowService : IWorkFlowActivity
    {
        private IWorkFlowActivity WorkFlowEngineInstance
        {
            get { return new WorkFlowManage(); }
        }

        [OperationContract]
        public string Execute(AppInfoModel entity)
        {
            return WorkFlowEngineInstance.Execute(entity);
        }

        [OperationContract]
        public string NewWorkFlow(AppInfoModel entity)
        {
            return WorkFlowEngineInstance.NewWorkFlow(entity);
        }

        [OperationContract]
        public IList<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId)
        {
            return WorkFlowEngineInstance.QueryInProgressActivityListByOperatorUserId(operatorUserId);
        }

        [OperationContract]
        public IEnumerable<string> GetCurrentActivityStateByAppIdAndUserID(string appId, string userId)
        {
            return WorkFlowEngineInstance.GetCurrentActivityStateByAppIdAndUserID(appId, userId);
        }
    }
}
