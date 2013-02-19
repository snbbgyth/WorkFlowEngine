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
    using DBHelp;
    using Help;
    using IDAL;
    using CommonLibrary.Help;
    using CommonLibrary.Model;
    public class WorkFlowActivityLogDAL : IDataOperationActivity<WorkFlowActivityLogModel>
    {
        #region Private Variable

        private IDBHelp _dbHelpInstance;

        #endregion

        #region Private Property

        private IDBHelp DBHelpInstance
        {
            get
            {
                if (_dbHelpInstance == null)
                {
                    _dbHelpInstance = new SQLiteHelp();
                    _dbHelpInstance.ConnectionString = string.Format(WFConstants.SQLiteConnectionString,
                                                                   WFUntilHelp.SqliteFilePath);
                }
                return _dbHelpInstance;
            }
        }

        #endregion

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateWorkFlowActivityLogTableSqlTags);
        }

        public int Insert(WorkFlowActivityLogModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntity(entity));
        }

        private string GetInsertSqlByEntity(WorkFlowActivityLogModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkFlowActivityLogSqlTags, entity.ID, entity.AppId,
                                 entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                                 entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete),entity.OldID);
        }

        public int Modify(WorkFlowActivityLogModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifySqlByEntity(entity));
        }

        private string GetModifySqlByEntity(WorkFlowActivityLogModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivityLogSqlTags, entity.ID, entity.AppId,
                              entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                              entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                              entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete),entity.OldID);

        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkFlowActivityLogByIDSqlTags, id);
        }

        public List<WorkFlowActivityLogModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<WorkFlowActivityLogModel>(GetQueryAllSql());
        }

        private string GetQueryAllSql()
        {
            return WFConstants.QueryAllWorkFlowActivityLogSqlTags;
        }

        public WorkFlowActivityLogModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkFlowActivityLogModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
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

    }
}
