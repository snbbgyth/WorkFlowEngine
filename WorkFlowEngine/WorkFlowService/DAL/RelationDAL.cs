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
    using Help;
    using Model;

    public class RelationDAL : DataOperationActivityBase<RelationModel>
    {
        public static RelationDAL Current
        {
            get { return new RelationDAL(); }
        }

        protected override string GetInsertByEntitySql(RelationModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertRelationSqlTags, entity.ID, entity.ChildNodeID, entity.ParentNodeID,entity.Type,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetModifyByEntitySql(RelationModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceRelationSqlTags, entity.ID, entity.ChildNodeID, entity.ParentNodeID,entity.Type,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }


       rotected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteRelationByIDSqlTags, id);
        }


        prected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryRelationByIDSqlTags, id);
        }
 

        plic RelationModel QueryByChildNodeIDAndParentNodeIDAndType(string childNodeID, string parentNodeID, int type)
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

        prot  override string GetCreateTableSql()
        {
            return WFConstants.CreateRelationTableSqlTags;
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllRelationSqlTags;
        }
    }
}
