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
    using DBHelp;
    using IDAL;
    using Model;
    using CommonLibrary.Help;

    public class RoleInfoDAL : IDataOperationActivity<RoleInfoModel>
    {
        public static RoleInfoDAL Current
        {
            get { return new RoleInfoDAL(); }
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

        public int Insert(RoleInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertByEntitySql(entity));
        }

        private string GetInsertByEntitySql(RoleInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserRoleInfoSqlTags, entity.ID, entity.RoleName, entity.RoleDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(RoleInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(RoleInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserRoleInfoSqlTags, entity.ID, entity.RoleName, entity.RoleDisplayName,
                           entity.CreateDateTime, entity.LastUpdateDateTime, Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserRoleInfoByIDSqlTags, id);
        }


        //Todo: now is wrong
        public int DeleteByUserID(string userID)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByUserIDSql(userID));
        }

        //Todo: now is wrong
        private string GetDeleteByUserIDSql(string userID)
        {
            return string.Format(WFConstants.DeleteUserRoleInfoByUserIDSqlTags, userID);
        }

        public List<RoleInfoModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<RoleInfoModel>(WFConstants.QueryAllUserRoleInfoSqlTags);
        }

        public RoleInfoModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<RoleInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserRoleInfoByIDSqlTags, id);
        }

        //Todo: now is wrong
        public List<RoleInfoModel> QueryByUserID(string userID)
        {
            return  DBHelpInstance.ReadEntityList<RoleInfoModel>(GetQueryByUserIDSql(userID));

        }

        //Todo: now is wrong
        private string GetQueryByUserIDSql(string userID)
        {
            return string.Format(WFConstants.QueryUserRoleInfoByUserIDSqlTags, userID);
        }


        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateUserRoleInfoTableSqlTags);
        }
    }
}
