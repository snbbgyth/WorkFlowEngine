/********************************************************************************
** Class Name:   RelationDAL 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     RelationDAL class
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

    public class RelationDAL : IDataOperationActivity<RelationModel>
    {
        public static RelationDAL Current
        {
            get { return new RelationDAL(); }
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

        public int Insert(RelationModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
        }

        private string GetInsertSqlByEntitySql(RelationModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertRelationSqlTags, entity.ID, entity.ChildNodeID, entity.ParentNodeID,entity.Type,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(RelationModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(RelationModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceRelationSqlTags, entity.ID, entity.ChildNodeID, entity.ParentNodeID,entity.Type,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteRelationByIDSqlTags, id);
        }

        public List<RelationModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<RelationModel>(WFConstants.QueryAllRelationSqlTags);
        }

        public RelationModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<RelationModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryRelationByIDSqlTags, id);
        }

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateRelationTableSqlTags);
        }

        public RelationModel QueryByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type)
        {
            var entityList =DBHelpInstance.ReadEntityList<RelationModel>(GetQueryByChildNodeIDAndParentNodeIDAndTypeSql(
                    childNodeID, parentNodeID, type));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByChildNodeIDAndParentNodeIDAndTypeSql(string childNodeID, string parentNodeID, int type)
        {
            return string.Format(WFConstants.QueryRelationByChildNodeIDAndParentNodeIDAndTypeSqlTags,childNodeID,parentNodeID,type);
        }

        public List<RelationModel> QueryByChildNodeIDAndType(string childNodeID, int type)
        {
           return DBHelpInstance.ReadEntityList<RelationModel>(GetQueryByChildNodeIDAndTypeSql(childNodeID, type));
        }

        private string GetQueryByChildNodeIDAndTypeSql(string childNodeID, int type)
        {
            return string.Format(WFConstants.QueryRelationByChildNodeIDAndTypeSqlTags, childNodeID, type);
        }

        public List<RelationModel> QueryByParentNodeIDAndType(string parentNodeID, int type)
        {
            return DBHelpInstance.ReadEntityList<RelationModel>(GetQueryByParentNodeIDAndTypeSql(parentNodeID, type)); 
        }

        private string GetQueryByParentNodeIDAndTypeSql(string parentNodeID, int type)
        {
            return string.Format(WFConstants.QueryRelationByParentNodeIDAndTypeSqlTags, parentNodeID, type);
        }

        public int DeleteByChildNodeIDAndType(string childNodeID, int type)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByChildNodeIDAndTypeSql(childNodeID,type));
        }

        private string GetDeleteByChildNodeIDAndTypeSql(string childNodeID, int type)
        {
            return string.Format(WFConstants.DeleteRelationByChildNodeIDAndTypeSqlTags, childNodeID, type);
        }

        public int DeleteByParentNodeIDAndType(string parentNodeID, int type)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByParentNodeIDAndTypeSql(parentNodeID, type));
        }

        private string GetDeleteByParentNodeIDAndTypeSql(string parentNodeID, int type)
        {
            return string.Format(WFConstants.DeleteRelationByParentNodeIDAndTypeSqlTags, parentNodeID, type);
        }

        public int DeleteByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type)
        {
            return
                DBHelpInstance.ExecuteNonQuery(GetDeleteByChildNodeIDAndParentNodeIDAndTypeSql(childNodeID, parentNodeID,
                                                                                               type));
        }

        private string GetDeleteByChildNodeIDAndParentNodeIDAndTypeSql(string childNodeID, string parentNodeID, int type)
        {
            return string.Format(WFConstants.DeleteRelationByChildNodeIDAndParentNodeIDAndTypeSqlTags, childNodeID,
                                 parentNodeID, type);
        }
    }
}
