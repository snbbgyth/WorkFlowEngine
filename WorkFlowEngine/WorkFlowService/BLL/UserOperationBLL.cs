/********************************************************************************
** Class Name:   UserOperationBLL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserOperationBLL class
*********************************************************************************/

using System;
using System.Linqsing WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using Model;
    using DAL;
    using Help;

    public class UserOperationBLL : IUserOperationActivity
    {
        public bool CreateUser(string userName, string password)
        {
            return DataOperationBLL.Current.Insert(new UserInfoModel
            {
                CreateDateTime = DateTime.Now,
                IsDelete = false,
                LastUpdateDateTime = DateTime.Now,
                Password = password,
                UserName = userName
            }) > 0;
        }

        public bool ModifyPassword(string userId, string password)
        {
            var entity = DataOperationBLL.Current.QueryByEID<UserInfoModel>(userId);
            entity.Password = password;
            return DataOperationBLL.Current.Modify(entity) > 0;
        }

        public bool DeleteUser(string userId)
        {
            DataOperationBLL.Current.Remove<UserInfoModel>(userId);
            UserRoleInfoDAL.Current.DeleteByUserID(userId);
            return true;
        }

        public bool AssignUserRole(string userId, string workflowState)
        {
            return
                DataOperationBLL.Current.Insert(new UserRoleInfoModel
                {
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    OperatorState = workflowState,
                    UserID = userId
                }) > 0;
        }

        public bool ModifyUserRole(string userId, string workflowState)
        {
            var entityList = UserRoleInfoDAL.Current.QueryByUserID(userId);
            var entity = entityList.First(t => t.OperatorState.CompareEqualIgnoreCase(workflowState));
            entity.OperatorState = workflowState;
            return DataOperationBLL.Current.Modify(entity) > 0;
        }

        public bool DeleteUserRole(string userId)
        {
            return UserRoleInfoDAL.Current.DeleteByUserID(userId) > 0;
        }

        public string LoginIn(string userName, string password)
        {
            return UserInfoDAL.Current.Login(userName, password);
        }
    }
}
