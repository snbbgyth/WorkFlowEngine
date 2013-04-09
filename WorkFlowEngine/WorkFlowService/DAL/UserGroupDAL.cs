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
    using CommonLibrary.Help;
    using Help;
    using Model;

    public class UserGroupDAL : DataOperationActivityBase<UserGroupModel>
    {
        public static UserGroupDAL Current
        {
            get { return new UserGroupDAL(); }
        }

        protected override string GetInsertByEntitySql(UserGroupModel entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertUserGroupSqlTags, entity.Id, entity.GroupName, entity.GroupDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetModifyByEntitySql(UserGroupModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceUserGroupSqlTags, entity.Id, entity.GroupName, entity.GroupDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }

        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteUserGroupByIDSqlTags, id);
        }

        protected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryUserGroupByIDSqlTags, id);
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

        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateUserGroupTableSqlTags;
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllUserGroupSqlTags;
        }
    }
}
