/********************************************************************************
** Class Name:   UserInfoDAL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserInfoDAL class
*********************************************************************************/


namespace WorkFlowService.DAL
{

    using System;
    using System.Collections.Generic;
    using DBHelp;
    using IDAL;
    using Model;lic clusing Help;
lic class UserInfoDAL : IDataOperationActivity<UserInfoModel>
    {
        public int Insert(static UserInfoDAL Current
        { 
            get { return new UserInfoDAL(); }
        }


        private IDBHelp DBHelpInstance
        {
            get { return new SQLiteHelp(); }
        }

        public int Insert(UserInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
        }

        private string GetInsertSqlByEntitySql(UserInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserInfoSqlTags, entity.ID, entity.UserName, entity.Password,
                                 entity.CreateDateTime, entity.LastUpdateDateTime, Convert.ToInt32(entity.IsDelete));
        }

        public int Modify(UserInfoModel entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        private string GetModifyByEntitySql(UserInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserInfoSqlTags, entity.ID, entity.UserName, entity.Password,
                                 entity.CreateDateTime, entity.LastUpdateDateTime, Convert.ToInt32(entity.IsDelete));
        }

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        private string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserInfoByIDSqlTags, idlic List<UserInfoModel> QueryAll()
        {
            throw new NotImplementedExceptreturn DBHelpInstance.ReadEntityList<UserInfoModel>(WFConstants.QueryAllUserInfoSqlTags);
        }
        
        public UserInfoModel QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserInfoByIDTags, ideateTable()
        {
        string Login(string userName, string password)
        {
            retuvar entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByUserNameAndPasswordSql(userName,password));
            return  entityList != null && entityList.Count > 0 ? entityList[0].ID : null;
        }

        private string GetQueryByUserNameAndPasswordSql(string userName, string password)
        {
            return string.Format(WFConstants.QueryUserInfoByUserNameAndPasswordTags, userName, password);
        }

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateUserInfoTableSqlTags);
        }
    }
}
