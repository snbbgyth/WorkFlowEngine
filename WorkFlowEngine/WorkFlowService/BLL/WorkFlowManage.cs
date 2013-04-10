﻿/********************************************************************************
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
 
using WorkFlowService.Help;
using WorkFlowService.IDAL;
using WorkFlowService.NHibernateDAL;

namespace WorkFlowService.BLL
{
    public class WorkFlowManage : IWorkFlowActivity
    {
        public string Execute(AppInfoModel entity)
        {
            var activityEntity = WorkFlowActivityDAL.Current.QueryByAppId(entity.AppId);
            var currentWorkFlowState =
                WorkFlowEngine.Current.Execute(entity.WorkflowName, entity.CurrentState,
                                               entity.ActivityState);
            activityEntity.ForeWorkflowState = activityEntity.CurrentWorkflowState;
            activityEntity.CurrentWorkflowState = currentWorkFlowState;
            activityEntity.OperatorUserId = entity.UserId;
            activityEntity.OperatorUserList += entity.UserId + WFConstants.SplitCharacterTag;
            activityEntity.LastUpdateDateTime = DateTime.Now;
            activityEntity.ApplicationState = currentWorkFlowState;
            activityEntity.WorkflowName = entity.WorkflowName;
            DataOperationBLL.Current.Modify(activityEntity);
            return currentWorkFlowState;
        }

        public string NewWorkFlow(AppInfoModel entity)
        {
            var activityEntity = new WorkFlowActivityModel
            {
                ForeWorkflowState = entity.CurrentState,
                OperatorActivity = entity.ActivityState,
                LastUpdateDateTime = DateTime.Now,
                AppId = entity.AppId,
                CreateDateTime = DateTime.Now,
                CreateUserId = entity.UserId,
                OperatorUserId = entity.UserId,
                WorkflowName = entity.WorkflowName,
                OperatorUserList = entity.UserId + WFConstants.SplitCharacterTag,
            };
            WorkFlowEngine.Current.InitWorkflowState(entity.WorkflowName);
            var currentWorkFlowState = WorkFlowEngine.Current.Execute(entity.WorkflowName, string.Empty, entity.ActivityState);
            activityEntity.CurrentWorkflowState = currentWorkFlowState;
            activityEntity.ApplicationState = currentWorkFlowState;
            DataOperationBLL.Current.Insert(activityEntity);
            return currentWorkFlowState;
        }

        public IList<WorkFlowActivityModel> QueryInProgressActivityListByOperatorUserId(string operatorUserId)
        {
            return WorkFlowActivityDAL.Current.QueryInProgressActivityByOperatorUserId(operatorUserId);
        }

        public IEnumerable<string> GetCurrentActivityStateByAppIdAndUserID(string appId, string userId)
        {
            var activityEntity = WorkFlowActivityDAL.Current.QueryByAppId(appId);
            if (CompareIsContain(activityEntity.OperatorUserId, userId))
                return WorkFlowEngine.Current.GetActivityStateByConditon(activityEntity.WorkflowName, activityEntity.CurrentWorkflowState);
            if (CompareIsContain(activityEntity.OperatorUserList, userId))
                return new List<string>{ActivityState.Read.ToString()};
            return null;
        }

        public ApplicationState GetApplicationStateByAppId(string appId)
        {
            var activityEntity = WorkFlowActivityDAL.Current.QueryByAppId(appId);
            return GetApplicationStateByWorkFlowActivityEntity(activityEntity);
        }

        private ApplicationState GetApplicationStateByWorkFlowActivityEntity(WorkFlowActivityModel entity)
        {
            if (entity == null)
                return ApplicationState.Draft;
            //if (WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkflowState) == WorkFlowState.Done || WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkflowState) == WorkFlowState.Refuse)
            //    return ApplicationState.Complete;
            //if (WFUntilHelp.GetWorkFlowStateByName(entity.CurrentWorkflowState) == WorkFlowState.Common && WFUntilHelp.GetActivityStateByName(entity.OperatorActivity) == ActivityState.Revoke)
            //    return ApplicationState.Draft;

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
