using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namusing WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using Model;
    publiusing DAL;
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
            return DataOperationBLL.CurrDataOperationBLL.Current.Remove<UserInfoModel>(userId);
            UserRoleInfoDAL.Current.DeleteByUserID(userId);
            return true;ol AssignUserRole(string userId, string workflowState)
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
        }var entity = UserRoleInfoDAL.Current.QueryByUserID(userId);
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
