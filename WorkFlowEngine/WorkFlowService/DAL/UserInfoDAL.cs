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
    using Model;
    using Help;
    using CommonLibrary.Help;

    public class UserInfoDAL : DataOperationActivityBase<UserInfoModel>
    {
        public static UserInfoDAL Current
        {
            get { return new UserInfoDAL(); }
        }

        protected override string GetInsertByEntitySql(UserInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserInfoSqlTags, entity.ID, entity.UserName,entity.UserDisplayName, entity.Password,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetModifyByEntitySql(UserInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserInfoSqlTags, entity.ID, entity.UserName,entity.UserDisplayName, entity.Password,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserInfoByIDSqlTags, id);
        }

        protected override string GetQueryByIDSql(string id)
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
 
        public UserInfoModel QueryByUserName(string userName)
        {
            var entityList = DBHelpInstance.ReadEntityList<UserInfoModel>(GetQueryByUserNameSql(userName));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByUserNameSql(string userName)
        {
            return string.Format(WFConstants.QueryUserInfoByUserNameSqlTags, userName);
        }

        protected override UserInfoModel NullResult()
        {
            return null;
        }

        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateUserInfoTableSqlTags;
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllUserInfoSqlTags;
        }
    }
}
