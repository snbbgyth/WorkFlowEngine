/********************************************************************************
** Class Name:   RoleInfoDAL 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     RoleInfoDAL class
*********************************************************************************/



namespace WorkFlowService.DAL
{
    using Help;
    using System;
    using System.Collections.Generic;
    using Model;
    using CommonLibrary.Help;

    public class RoleInfoDAL : DataOperationActivityBase<RoleInfoModel>
    {
        public static RoleInfoDAL Current
        {
            get { return new RoleInfoDAL(); }
        }

        protected override string GetInsertByEntitySql(RoleInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertRoleInfoSqlTags, entity.ID, entity.RoleName, entity.RoleDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetModifyByEntitySql(RoleInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceRoleInfoSqlTags, entity.ID, entity.RoleName, entity.RoleDisplayName,
                           entity.CreateDateTime, entitertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetModifyBDSql(string id)
        {
            return string.Format(WFConstants.DeleteRoleInfoByIDSqlTags, id);
        }

        //Todo: now is wrong
        public int DeleteByUserID(string userID)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByUserIDSql(userID));
        }

        //Todo: now is wrong
        private string GetDeleteByUserIDSql(string userID)
        {
            return string.Format(WFConstants.DeleteRoleInfoByUserIDSqlTags, userID);
        }

        protected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryRoleInfoByIDSqlTags, id);
        }

        //Todo: now is wrong
        public List<RoleInfoModel> QueryByUserID(string userID)
        {
            return  DBHelpInstance.ReadEntityList<RoleInfoModel>(GetQueryByUserIDSql(userID));

        }

        //Todo: now is wrong
        private string GetQueryByUserIDSql(string userID)
        {
            return string.Format(WFConstants.QueryRoleInfoByUserIDSqlTags, userID);
        }
 

        public RoleInfoModel QueryByRoleName(string roleName)
        {
            var entityList = DBHelpInstance.ReadEntityList<RoleInfoModel>(GetQueryByRoleNameSql(roleName));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByRoleNameSql(string roleName)
        {
            return string.Format(WFConstants.QueryRoleInfoByRoleNameSqlTags, roleName);

        }

        protected override RoleInfoModel NullResult()
        {
            return null;
        }

        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateRoleInfoTableSqlTags;
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllRoleInfoSqlTags;
        }
    }
}
