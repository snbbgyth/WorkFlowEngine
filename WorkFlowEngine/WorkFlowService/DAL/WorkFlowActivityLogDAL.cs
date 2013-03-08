/********************************************************************************
** Class Name:   WorkFlowActivityLogDAL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowActivityLogDAL class
*********************************************************************************/


namespace WorkFlowService.DAL
{

    using System;
    using System.Collections.Generic;
    using Help;
    using CommonLibrary.Help;
    using CommonLibrary.Model;
    public class WorkFlowActivityLogDAL : DataOperationActivityBase<WorkFlowActivityLogModel>
    {
        public static WorkFlowActivityLogDAL Current
        {
            get { return new WorkFlowActivityLogDAL(); }
        }

        protected override string GetInsertByEntitySql(WorkFlowActivityLogModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkFlowActivityLogSqlTags, entity.ID, entity.AppId, entity.WorkflowName,
                                 entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkflowState,
                                 entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete), entity.OldID);
        }

        protected override string GetModifyByEntitySql(WorkFlowActivityLogModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivityLogSqlTags, entity.ID, entity.AppId, entity.WorkflowName,
                              entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkflowState,
                              entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                              entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete), entity.OldID);

        }

        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkFlowActivityLogByIDSqlTags, id);
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllWorkFlowActivityLogSqlTags;
        }

        protected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryWorkFlowActivityLogByIDTags, id);
        }

        public List<WorkFlowActivityLogModel> QueryInProgressActivityByOperatorUserId(string operatorUserId)
        {
            return DBHelpInstance.ReadEntityList<WorkFlowActivityLogModel>(GetQueryInProgressActivityByOperatorUserIdSql(operatorUserId));
        }

        private string GetQueryInProgressActivityByOperatorUserIdSql(string operatorUserId)
        {
            return string.Format(WFConstants.QueryInProgressActivityByLogByUserOperatorIDSqlTags, operatorUserId);
        }

        public WorkFlowActivityLogModel QueryByAppId(string appId)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkFlowActivityLogModel>(GetQueryByAppIDSql(appId));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByAppIDSql(string appId)
        {
            return string.Format(WFConstants.QueryWorkFlowActivityLogByAppIdSqlTags, appId);
        }
 
        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateWorkFlowActivityLogTableSqlTags;
        }
    }
}
