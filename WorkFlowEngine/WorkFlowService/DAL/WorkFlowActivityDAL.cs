using System;
using System.Collections.Generic;
using CommonLibrary.Model;
using DBHelp;
using WorkFlowService.IDAL;


namespace WorkFlowService.DAL
{
    using Help;
    public class WorkFlowActivityDAL : IDataOperationActivity<WorkFlowActivityModel>
    {
        private IDBHelp DBHelpInstance
        {
            get { return new SQLiteHelp(); }
        }

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
                                 entity.WorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                                 entity.OperatorUserId, entity.CreateDateTime, entity.LastUpdateDateTime,
                                 entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName,Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(WorkFlowActivityModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifySqlByEntity(entity));
        }

        private string GetModifySqlByEntity(WorkFlowActivityModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkFlowActivitySqlTags, entity.ID, entity.AppId,
                              entity.WorkFlowState, entity.OperatorActivity, entity.CurrentWorkFlowState,
                              entity.OperatorUserId, entity.CreateDateTime, entity.LastUpdateDateTime,
                              entity.CreateUserId, entity.OperatorUserList, entity.ApplicationState, entity.AppName, Convert.ToInt32(entity.IsDelete));

        }

        public int DeleteByID(string id)
        {
            return  DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
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
            return string.Format(WFConstants.QueryWorkFlowActivityByIDTags, id);
        }

        public List<WorkFlowActivityModel>  QueryInProgressActivityByOperatorUserId(string operatorUserId)
        {
            return new List<WorkFlowActivityModel>() {};
        }

        public WorkFlowActivityModel QueryByAppId(string appId)
        {
            return null;
        }

    }
}
