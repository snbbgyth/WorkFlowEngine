using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namusing WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using Model;
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
            return DataOperationBLL.Current.Remove<UserInfoModel>(userId) > 0;
            //Todo: Remove UserRole
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
            throw new NotImplementedException();
        }

        public bool DeleteUserRole(string userId)
        {
            throw new NotImplementedException();
        }

        public string LoginIn(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
