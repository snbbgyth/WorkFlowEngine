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
    using DBHelp;
    using IDAL;
    using CommonLibrary.Help;
    using Help;
    public class WorkFlowActivityDAL : IDataOperationActivity<WorkFlowActivityModel>
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
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateWorkFlowActivityTableSqlTags);
        }

        public int Insert(WorkFlowActivityModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntity(entity));
        }

        private string GetInsertSqlByEntity(WorkFlowActivityModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkFlowActivitySqlTags, entity.ID, entity.AppId,
                                 entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                                 entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(WorkFlowActivityModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifySqlByEntity(entity));
        }

        private string GetModifySqlByEntity(WorkFlowActivityModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivitySqlTags, entity.ID, entity.AppId,
                              entity.ForeWorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                              entity.OperatorUserId, entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(),
                              entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkFlowActivityByIDSqlTags, id);
        }

        public List<WorkFlowActivityModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<WorkFlowActivityModel>(GetQueryAllSql());
        }

        private string GetQueryAllSql()
        {
            return WFConstants.QueryAllWorkFlowActivitySqlTags;
        }

        public WorkFlowActivityModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkFlowActivityModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
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
    }
}
