/********************************************************************************
** Class Name:   WorkFlowManage 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowManage class
*********************************************************************************/

using System;
using System.Collections.Generic;
using CommonLibrary.Help;
using CommonLibrary.Model;
using WorkFlowService.DAL;
using WorkFlowService.Help;
using WorkFlowService.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    public class WorkFlowManage : IWorkFlowActivity
    {
        private WorkFlowActivityDAL WorkFlowActivityDalInstance
        {
            get { return new WorkFlowActivityDAL(); }
        }

        private WorkFlowEngine WorkFlowEngineInstance
        {
            get { return new WorkFlowEngine(); }
        }

        public string Execute(AppInfoModel entity)
        {
            var activityEntity = WorkFlowActivityDalInstance.QueryByAppId(entity.AppId);
            var currentWorkFlowState =
                WorkFlowEngineInstance.Execute(entity.WorkflowName, activityEntity.CurrentWorkFlowState,
                                               WFUntilHelp.GetActivityStateByName(entity.ActivityState));
            activityEntity.WorkFlowState = activityEntity.CurrentWorkFlowState;
            activityEntity.CurrentWorkFlowState = currentWorkFlowState.ToString();
            activityEntity.OperatorUserId = entity.UserId;
            activityEntity.OperatorUserList += entity.UserId + WFConstants.SplitCharacterTag;
            activityEntity.LastUpdateDateTime = DateTime.Now;
            return currentWorkFlowState;
        }


        public string NewWorkFlow(AppInfoModel entity)
        {
            var activityEntity = new WorkFlowActivityModel
            {
                WorkFlowState = WorkFlowState.Common.ToString(),
                // new ActivityId
                OperatorActivity = ActivityState.Submit.ToString(),
                LastUpdateDateTime = DateTime.Now,
                AppId = entity.AppId,
                CreateDateTime = DateTime.Now,
                CreateUserId = entity.UserId,
                OperatorUserId = entity.UserId,
                OperatorUserList = entity.UserId + WFConstants.SplitCharacterTag
            };
            var currentWorkFlowState = WorkFlowEngineInstance.Execute(entity.WorkflowName, WorkFlowState.Common.ToString(), ActivityState.Submit);
            activityEntity.CurrentWorkFlowState = currentWorkFlowState.ToString();
            DataOperationBLL.Current.Insert(activityEntity);
            return currentWorkFlowState;
        }

        public List<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId)
        {
            return WorkFlowActivityDalInstance.QueryInProgressActivityByOperatorUserId(operatorUserId);
        }

        public ActivityState GetCurrentActivityStateByAppIdAndUserID(string appId, string userId)
        {
            var activityEntity = WorkFlowActivityDalInstance.QueryByAppId(appId);
            if (CompareIsContain(activityEntity.OperatorUserId, userId))
                return WorkFlowEngineInstance.GetActivityStateByWorkFlowState(WFUntilHelp.GetWorkFlowStateByName(activityEntity.CurrentWorkFlowState));
            if (CompareIsContain(activityEntity.OperatorUserList, userId))
                return ActivityState.Read;
            return ActivityState.None;
        }

        public ApplicationState GetApplicationStateByAppId(string appId)
        {
            var activityEntity = WorkFlowActivityDalInstance.QueryByAppId(appId);
            return GetApplicationStateByWorkFlowActivityEntity(activityEntity);
        }

        private ApplicationState GetApplicationStateByWorkFlowActivityEntity(WorkFlowActivityModel entity)
        {
            if (entity == null)
                return ApplicationState.Draft;
            if (WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkFlowState) == WorkFlowState.Done || WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkFlowState) == WorkFlowState.Refuse)
                return ApplicationState.Complete;
            if (WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkFlowState) == WorkFlowState.Common && WFUntilHelp.GetActivityStateByName(entity.OperatorActivity) == ActivityState.Revoke)
                return ApplicationState.Draft;
            return ApplicationState.InProgress;
        }

        private bool CompareIsContain(string source, string value)
        {
            if (!string.IsNullOrEmpty(source) && source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
            return false;
        }
    }
}
