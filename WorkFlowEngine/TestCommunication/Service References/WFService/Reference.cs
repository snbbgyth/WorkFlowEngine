﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCommunication.WFService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WFService.BPELWorkFlowService")]
    public interface BPELWorkFlowService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/Execute", ReplyAction="http://tempuri.org/BPELWorkFlowService/ExecuteResponse")]
        string Execute(CommonLibrary.Model.AppInfoModel entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/NewWorkFlow", ReplyAction="http://tempuri.org/BPELWorkFlowService/NewWorkFlowResponse")]
        string NewWorkFlow(CommonLibrary.Model.AppInfoModel entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/QueryInProgressActivityListByOperatorUserI" +
            "d", ReplyAction="http://tempuri.org/BPELWorkFlowService/QueryInProgressActivityListByOperatorUserI" +
            "dResponse")]
        CommonLibrary.Model.WorkFlowActivityModel[] QueryInProgressActivityListByOperatorUserId(string operatorUserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/GetCurrentActivityStateByAppIdAndUserID", ReplyAction="http://tempuri.org/BPELWorkFlowService/GetCurrentActivityStateByAppIdAndUserIDRes" +
            "ponse")]
        string[] GetCurrentActivityStateByAppIdAndUserID(string appId, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/GetApplicationStateByAppId", ReplyAction="http://tempuri.org/BPELWorkFlowService/GetApplicationStateByAppIdResponse")]
        CommonLibrary.Help.ApplicationState GetApplicationStateByAppId(string appId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BPELWorkFlowService/ClearUnitTestData", ReplyAction="http://tempuri.org/BPELWorkFlowService/ClearUnitTestDataResponse")]
        void ClearUnitTestData();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BPELWorkFlowServiceChannel : TestCommunication.WFService.BPELWorkFlowService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BPELWorkFlowServiceClient : System.ServiceModel.ClientBase<TestCommunication.WFService.BPELWorkFlowService>, TestCommunication.WFService.BPELWorkFlowService {
        
        public BPELWorkFlowServiceClient() {
        }
        
        public BPELWorkFlowServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BPELWorkFlowServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BPELWorkFlowServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BPELWorkFlowServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Execute(CommonLibrary.Model.AppInfoModel entity) {
            return base.Channel.Execute(entity);
        }
        
        public string NewWorkFlow(CommonLibrary.Model.AppInfoModel entity) {
            return base.Channel.NewWorkFlow(entity);
        }
        
        public CommonLibrary.Model.WorkFlowActivityModel[] QueryInProgressActivityListByOperatorUserId(string operatorUserId) {
            return base.Channel.QueryInProgressActivityListByOperatorUserId(operatorUserId);
        }
        
        public string[] GetCurrentActivityStateByAppIdAndUserID(string appId, string userId) {
            return base.Channel.GetCurrentActivityStateByAppIdAndUserID(appId, userId);
        }
        
        public CommonLibrary.Help.ApplicationState GetApplicationStateByAppId(string appId) {
            return base.Channel.GetApplicationStateByAppId(appId);
        }
        
        public void ClearUnitTestData() {
            base.Channel.ClearUnitTestData();
        }
    }
}
