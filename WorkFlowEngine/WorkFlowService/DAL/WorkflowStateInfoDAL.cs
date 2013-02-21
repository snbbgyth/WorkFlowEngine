/********************************************************************************
** Class Name:   WorkflowStateInfoDAL 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     WorkflowStateInfoDAL class
*********************************************************************************/

namespace WorkFlowService.DAL
{
    using System;
    using System.Collections.Generic;
    using CommonLibrary.Help;
    using DBHelp;
    using Help;
    using IDAL;
    using Model;

    public class WorkflowStateInfoDAL : IDataOperationActivity<WorkflowStateInfoModel>
    {
        public static WorkflowStateInfoDAL Current
        {
            get { return new WorkflowStateInfoDAL(); }
        }

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

        public int Insert(WorkflowStateInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
        }

        private string GetInsertSqlByEntitySql(WorkflowStateInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkflowStateInfoSqlTags, entity.ID, entity.WorkflowName, entity.WorkflowDisplayName, entity.StateNodeName, entity.StateNodeDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(WorkflowStateInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(WorkflowStateInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkflowStateInfoSqlTags, entity.ID, entity.WorkflowName, entity.WorkflowDisplayName, entity.StateNodeName, entity.StateNodeDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkflowStateInfoByIDSqlTags, id);
        }

        public List<WorkflowStateInfoModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<WorkflowStateInfoModel>(WFConstants.QueryAllWorkflowStateInfoSqlTags);
        }

        public WorkflowStateInfoModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkflowStateInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryWorkflowStateInfoByIDSqlTags, id);
        }

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateWorkflowStateInfoTableSqlTags);
        }
    }
}
