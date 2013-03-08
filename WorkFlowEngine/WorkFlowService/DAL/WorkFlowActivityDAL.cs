/********************************************************************************
** Class Name:   WorkFlowActivityDAL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowActivityDAL class
*********************************************************************************/

namespace WorkFlowService.DAL
{
    using System;
    using System.Collections.Generic;
    using CommonLibrary.Model;
    using CommonLibrary.Help;
    using Help;
    public class WorkFlowActivityDAL : DataOperationActivityBase<WorkFlowActivityModel>
    {
        public static WorkFlowActivityDAL Current
        {
            get { return new WorkFlowActivityDAL(); }
        }

        protected override string GetInsertByEntitySql(WorkFlowActivityModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkFlowActivitySqlTags, entity.ID, entity.AppId, entity.WorkflowName,
                                 entity.ForeWorkflowState, entity.OperatorActivity, entity.CurrentWorkflowState,
                                 entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete));
        }


        protected override string GetModifyByEntitySql(WorkFlowActivityModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivitySqlTags, entity.ID, entity.AppId, entity.WorkflowName,
                              entity.ForeWorkflowState, entity.OperatorActivity, entity.CurrentWorkflowState,
                              entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                              entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkFlowActivityByIDSqlTags, id);
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllWorkFlowActivitySqlTags;
        }

        protected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryWorkFlowActivityByIDSqlTags, id);
        }

        public List<WorkFlowActivityModel> QueryInProgressActivityByOperatorUserId(string operatorUserId)
        {
            return DBHelpInstance.ReadEntityList<WorkFlowActivityModel>(GetQueryByOperatorUserIdSql(operatorUserId));
        }

        private string GetQueryByOperatorUserIdSql(string operatorUserId)
        {
            return string.Format(WFConstants.QueryWorkFlowActivityByOperatorUserIDSqlTags, operatorUserId);
        }

        public WorkFlowActivityModel QueryByAppId(string appId)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkFlowActivityModel>(GetQueryByAppId(appId));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByAppId(string appId)
        {
            return string.Format(WFConstants.QueryWorkFlowActivityByAppIDSqlTags, appId);
        }
 
        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateWorkFlowActivityTableSqlTags;
        }
    }
}
