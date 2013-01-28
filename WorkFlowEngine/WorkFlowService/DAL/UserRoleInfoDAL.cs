

namespace WorkFlowService.DAL
{
    using Help;
    using System;
    using System.Collections.Generic;
    using DBHelp;
    using IDAL;
    using Model;
ic class UserRoleInfoDAL : IDataOperationActivity<UserRoleInfoModel>
    {
        public int Insert(Ustatic UserRoleInfoDAL Current
        {
            get { return new UserRoleInfoDAL(); }
        }
blic int Insertrivate IDBHelp DBHelpInstance
        {
            get { return new SQLiteHelp(); }
        }

        public int Insert(UserRoleInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertByEntitySql(entity));
        }

        private string GetInsertByEntitySql(UserRoleInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserRoleInfoSqlTags, entity.ID, entity.UserID, entity.OperatorState,
                                 entity.CreateDateTime, entity.LastUpdateDateTime, Convert.ToInt32(entity.IsDelete));
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
            return string.Format(WFConstants.DeleteUserRoleInfoByUserIDTags, userID)st<UserRoleInfoModel> QueryAll()
        {
            throw new NotImplementedException()return DBHelpInstance.ReadEntityList<UserRoleInfoModel>(WFConstants.QueryAllUserRoleInfoSqlTags);
        }
        ryByID(string id)
        {
            throw new NotImplementedException();
   var entityList = DBHelpInstance.ReadEntityList<UserRoleInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserRoleInfoByIDTags, id);
        }


        public List<UserRoleInfoModel> QueryByUserID(string userID)
        {
            return DBHelpInstance.ReadEntityList<UserRoleInfoModel>(GetQueryByUserIDSql(userID));
            
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
