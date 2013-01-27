using System;
using System.Collections.Generic;
using DBHelp;
using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.DAL
{
    using CommonLibrary.Model;
    public class WorkFlowActivityLogDAL : IDataOperationActivity<WorkFlowActivityLogModel>
    {
        private IDBHelp DBHelpInstance
        {
            get { return new SQLiteHelp(); }
        }

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
                                 entity.WorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                                 entity.OperatorUserId, entity.CreateDateTime, entity.LastUpdateDateTime,
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete),entity.OldID);
        }

        public int Modify(WorkFlowActivityLogModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifySqlByEntity(entity));
        }

        private string GetModifySqlByEntity(WorkFlowActivityLogModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivityLogSqlTags, entity.ID, entity.AppId,
                              entity.WorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                              entity.OperatorUserId, entity.CreateDateTime, entity.LastUpdateDateTime,
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
            return new List<WorkFlowActivityLogModel>() { };
        }

        public WorkFlowActivityLogModel QueryByAppId(string appId)
        {
            return null;
        }


 
    }
}
