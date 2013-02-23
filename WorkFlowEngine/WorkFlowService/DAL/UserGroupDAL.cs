/********************************************************************************
** Class Name:   UserGroupDAL 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     UserGroupDAL class
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

    public class UserGroupDAL : IDataOperationActivity<UserGroupModel>
    {
        public static UserGroupDAL Current
        {
            get { return new UserGroupDAL(); }
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

        public int Insert(UserGroupModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
        }

        private string GetInsertSqlByEntitySql(UserGroupModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserGroupSqlTags, entity.ID, entity.GroupName, entity.GroupDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(UserGroupModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(UserGroupModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserGroupSqlTags, entity.ID, entity.GroupName, entity.GroupDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserGroupByIDSqlTags, id);
        }

        public List<UserGroupModel> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<UserGroupModel>(WFConstants.QueryAllUserGroupSqlTags);
        }

        public UserGroupModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserGroupModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserGroupByIDSqlTags, id);
        }

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateUserGroupTableSqlTags);
        }

        public UserGroupModel QueryByGroupName(string groupName)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserGroupModel>(GetQueryByGroupNameSql(groupName));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByGroupNameSql(string groupName)
        {
            return string.Format(WFConstants.QueryUserGroupByGroupNameSqlTags, groupName);
        }
    }
}
