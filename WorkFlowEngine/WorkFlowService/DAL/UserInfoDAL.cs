/********************************************************************************
** Class Name:   UserInfoDAL 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     UserInfoDAL class
*********************************************************************************/


namespace WorkFlowService.DAL
{
    using System;
    using System.Collections.Generic;
    using DBHelp;
    using IDAL;
    using Model;
    using Help;
    using CommonLibrary.Help;

    public class UserInfoDAL : IDataOperationActivity<UserInfoModel>
    {
        public static UserInfoDAL Current
        {
            get { return new UserInfoDAL(); }
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

        public int Insert(UserInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
        }

        private string GetInsertSqlByEntitySql(UserInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserInfoSqlTags, entity.ID, entity.UserName,entity.UserDisplayName, entity.Password,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(UserInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(UserInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserInfoSqlTags, entity.ID, entity.UserName,entity.UserDisplayName, entity.Password,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserInfoByIDSqlTags, id);
        }

        public List<UserInfoModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<UserInfoModel>(WFConstants.QueryAllUserInfoSqlTags);
        }

        public UserInfoModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserInfoByIDTags, id);
        }

        public string Login(string userName, string password)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByUserNameAndPasswordSql(userName, password));
            return entityList != null && entityList.Count > 0 ? entityList[0].ID : null;
        }

        private string GetQueryByUserNameAndPasswordSql(string userName, string password)
        {
            return string.Format(WFConstants.QueryUserInfoByUserNameAndPasswordTags, userName, password);
        }

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateUserInfoTableSqlTags);
        }

        public UserInfoModel QueryByUserName(string userName)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByUserNameSql(userName));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByUserNameSql(string userName)
        {
            return string.Format(WFConstants.QueryUserInfoByUserNameSqlTags, userName);
        }
    }
}
