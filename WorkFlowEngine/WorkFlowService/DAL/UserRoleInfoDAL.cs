/********************************************************************************
** Class Name:   UserRoleInfoDAL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserRoleInfoDAL class
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

    public class UserRoleInfoDAL : IDataOperationActivity<UserRoleInfoModel>
    {
        public static UserRoleInfoDAL Current
        {
            get { return new UserRoleInfoDAL(); }
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

        public int Insert(UserRoleInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertByEntitySql(entity));
        }

        private string GetInsertByEntitySql(UserRoleInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserRoleInfoSqlTags, entity.ID, entity.UserID, entity.OperatorState,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(UserRoleInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(UserRoleInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserRoleInfoSqlTags, entity.ID, entity.UserID, entity.OperatorState,
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

        public int DeleteByUserID(string userID)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByUserIDSql(userID));
        }

        private string GetDeleteByUserIDSql(string userID)
        {
            return string.Format(WFConstants.DeleteUserRoleInfoByUserIDTags, userID);
        }

        public List<UserRoleInfoModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<UserRoleInfoModel>(WFConstants.QueryAllUserRoleInfoSqlTags);
        }

        public UserRoleInfoModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserRoleInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserRoleInfoByIDTags, id);
        }


        public List<UserRoleInfoModel> QueryByUserID(string userID)
        {
            return  DBHelpInstance.ReadEntityList<UserRoleInfoModel>(GetQueryByUserIDSql(userID));

        }

        private string GetQueryByUserIDSql(string userID)
        {
            return string.Format(WFConstants.QueryUserRoleInfoByUserIDTags, userID);
        }


        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateUserRoleInfoTableSqlTags);
        }
    }
}
