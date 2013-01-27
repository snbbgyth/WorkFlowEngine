using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CommonLibrary.Model;
using WorkFlowService.BLL;
using WorkFlowService.IDAL;
using WorkFlowService.Help;

namespace WorkFlowWCFService
{

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WorkFlowService”。
    [ServiceContract]
    public class WorkFlowService : IWorkFlowActivity
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
