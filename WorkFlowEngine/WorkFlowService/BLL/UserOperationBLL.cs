/********************************************************************************
** Class Name:   UserOperationBLL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserOperationBLL class
*********************************************************************************/

using System;
using System.Linq;
using CommonLibrary.Help;
using WorkFlowService.IDAL;


namespace WorkFlowService.BLL
{
    using Model;
    using DAL;
    using Help;

    public class UserOperationBLL //: IUserOperationActivity
    {
        #region User operation

        public bool CreateUser(string userName,string userDisplayName, string password)
        {
            return DataOperationBLL.Current.Insert(new UserInfoModel
            {
                CreateDateTime = DateTime.Now,
                IsDelete = false,
                LastUpdateDateTime = DateTime.Now,
                Password = password,
                UserName = userName,
                UserDisplayName = userDisplayName
            }) > 0;
        }

        public bool ModifyPasswordByUserID(string userId, string password)
        {
            var entity = DataOperationBLL.Current.QueryByEID<UserInfoModel>(userId);
            entity.Password = password;
            return DataOperationBLL.Current.Modify(entity) > 0;
        }

        public bool ModifyPasswordByUserName(string userName, string password)
        {
            var entity = UserInfoDAL.Current.QueryByUserName(userName); 
            entity.Password = password;
            return DataOperationBLL.Current.Modify(entity) > 0;
        }

        public bool DeleteUserByUserID(string userId)
        {
            DataOperationBLL.Current.Remove<UserInfoModel>(userId);
           // RoleInfoDAL.Current.DeleteByUserID(userId);
           // Todo: delete userID in realtion table.
            return true;
        }

        public string LoginIn(string userName, string password)
        {
            return UserInfoDAL.Current.Login(userName, password);
        }

        #endregion

        #region User relation UserGroup Type enqual 1

        public bool AddUserInUserGroup(string userId, string userGroupId)
        {
            return
                RelationDAL.Current.Insert(new RelationModel
                                               {
                                                   ChildNodeID = userId,
                                                   CreateDateTime = DateTime.Now,
                                                   LastUpdateDateTime = DateTime.Now,
                                                   ParentNodeID = userGroupId,
                                                   Type = 1
                                               })>0;
        }



        #endregion

        #region UserGroup relation RoleInfo

        public bool AssignUserGroupRole(string userGroupId, string roleID)
        {
            return
                DataOperationBLL.Current.Insert(new RelationModel()
                {
                    ChildNodeID = userGroupId,
                    ParentNodeID = roleID,
                    Type = 2,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                }) > 0;
        }

        public bool DeleteUserGroupRole(string userId)
        {
            return RoleInfoDAL.Current.DeleteByUserID(userId) > 0;
        }

        #endregion
    }
}
