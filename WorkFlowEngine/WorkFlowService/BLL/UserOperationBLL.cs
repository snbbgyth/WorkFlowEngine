/********************************************************************************
** Class Name:   UserOperationBLL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserOperationBLL class
*********************************************************************************/

using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using Model;
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserOperationBLL //: IUserOperationActivity
    {
        #region Public Get New Instance

        public static UserOperationBLL Current
        {
            get { return new UserOperationBLL(); }
        }

        public UserOperationBLL()
        {
            DataOperationInstance.InitDataBase();
        }

        private DataOperationBLL _dataOperationInstance;

        private static readonly object SyncOperationObj = new object();

        public IDataOperationDAL DataOperationInstance
        {
            get
            {
                lock (SyncOperationObj)
                {
                    if (_dataOperationInstance == null)
                        _dataOperationInstance = new DataOperationBLL();
                }

                return _dataOperationInstance;
            }
        }

        #endregion

        #region User operation

        public bool CreateUser(string userName, string userDisplayName, string password)
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
            var entity = DataOperationBLL.Current.QueryByID<UserInfoModel>(userId);
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
            DeleteUserAllUserGroup(userId);
            DeleteUserAllRole(userId);

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
                }) > 0;
        }

        public bool DeleteUserInUserGroup(string userId, string userGroupId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndParentNodeIDAndType(userId, userGroupId, 1) > 0;
        }

        public int DeleteUserAllUserGroup(string userId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndType(userId, 1);
        }

        public bool ModifyUserInUserGroup(string userId, string oldUserGroupId, string newUserGroupId)
        {
            DeleteUserInUserGroup(userId, oldUserGroupId);
            return AddUserInUserGroup(userId, newUserGroupId);
        }


        #endregion

        #region UserGroup relation RoleInfo Type enqual 2

        public bool AddUserGroupRole(string userGroupId, string roleID)
        {
            return
                DataOperationBLL.Current.Insert(new RelationModel
                {
                    ChildNodeID = userGroupId,
                    ParentNodeID = roleID,
                    Type = 2,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                }) > 0;
        }

        public int DeleteUserGroupAllRole(string userGroupId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndType(userGroupId, 2);
        }

        public bool DeleteUserGroupRole(string userGroupId, string roleId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndParentNodeIDAndType(userGroupId, roleId, 2) > 0;
        }

        public bool ModifyUserGroupRole(string userGoupId, string oldRoleId, string newRoleId)
        {
            DeleteUserGroupRole(userGoupId, oldRoleId);
            return AddUserGroupRole(userGoupId, newRoleId);
        }

        #endregion

        #region OperationActionInfo Relation RoleInfo Type enqual 3

        public bool AddOperationActionInRole(string operationActionId, string roleId)
        {
            return RelationDAL.Current.Insert(new RelationModel
            {
                ChildNodeID = operationActionId,
                ParentNodeID = roleId,
                Type = 3,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now
            }) > 0;
        }

        public int DeleteRoleAllOperationAction(string roleId)
        {
            return RelationDAL.Current.DeleteByParentNodeIDAndType(roleId, 3);
        }

        public bool DeleteOperationActionInRole(string operationActionId, string roleId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndParentNodeIDAndType(operationActionId, roleId, 3) > 0;
        }

        #endregion

        #region WorkflowStateInfo relation RoleInfo Type enqual 4

        public bool AddWorkflowStateInRole(string workflowStateId, string roleId)
        {
            return
                RelationDAL.Current.Insert(new RelationModel
                {
                    ChildNodeID = workflowStateId,
                    ParentNodeID = roleId,
                    Type = 4,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now
                }) > 0;
        }

        /// <summary>
        /// Role relation workflow state is pair 1
        /// </summary>
        /// <param name="roleId">roleId</param>
        /// <returns>execute count</returns>
        public int DeleteRoleWorkflowState(string roleId)
        {
            return RelationDAL.Current.DeleteByParentNodeIDAndType(roleId, 4);
        }

        public bool ModifyRoleWorkflowState(string roleId, string newWorkflowStateId)
        {
            DeleteRoleWorkflowState(roleId);
            return AddWorkflowStateInRole(newWorkflowStateId, roleId);
        }

        #endregion

        #region UserInfo relation RoleInfo Type enqual 5

        public bool AddUserRole(string userId, string roleId)
        {
            return
                RelationDAL.Current.Insert(new RelationModel
                {
                    ChildNodeID = userId,
                    ParentNodeID = roleId,
                    Type = 5,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now
                }) > 0;
        }

        public int DeleteUserAllRole(string userId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndType(userId, 5);
        }

        public bool DeleteUserRole(string userId, string roleId)
        {
            return RelationDAL.Current.DeleteByChildNodeIDAndParentNodeIDAndType(userId, roleId, 5) > 0;
        }

        public bool ModifyUserRole(string userId, string oldRoleId, string newRoleId)
        {
            DeleteUserRole(userId, oldRoleId);
            return AddUserRole(userId, newRoleId);
        }

        #endregion

        #region UserInfo relation ReportTo UserInfo Type enqual 6

        public bool AddUserReportToUser(string userId, string reportUserId)
        {
            return
                RelationDAL.Current.Insert(new RelationModel
                {
                    ChildNodeID = userId,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    ParentNodeID = reportUserId,
                    Type = 6
                }) > 0;
        }

        #endregion

        #region Query operation

        public UserInfoModel QueryUserInfoByUserName(string userName)
        {
            return UserInfoDAL.Current.QueryByUserName(userName);
        }

        public List<UserInfoModel> QueryAllUserInfoByUserGroupId(string userGroupId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(userGroupId, 1);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => UserInfoDAL.Current.QueryByID(entity.ChildNodeID)).ToList()
                       : null;
        }

        public List<UserGroupModel> QueryAllUserGroupByUserId(string userId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(userId, 1);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => UserGroupDAL.Current.QueryByID(entity.ParentNodeID)).ToList()
                       : null;
        }

        public List<UserGroupModel> QueryAllUserGroupByRoleId(string roleId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(roleId, 2);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => UserGroupDAL.Current.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<UserInfoModel> QueryAllUserInfoByRoleId(string roleId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(roleId, 5);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => UserInfoDAL.Current.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<OperationActionInfoModel> QueryAllActionInfoByRoleId(string roleId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(roleId, 3);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => OperationActionInfoDAL.Current.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<WorkflowStateInfoModel> QueryAllWorkflowStateByRoleId(string roleId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(roleId, 4);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => WorkflowStateInfoDAL.Current.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<RoleInfoModel> QueryAllUserRoleByUserId(string userId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(userId, 5);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDAL.Current.QueryByID(entity.ParentNodeID)).ToList()
                       : null;
        }

        public List<RoleInfoModel> QueryAllRoleByActionId(string operationActionId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(operationActionId, 3);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDAL.Current.QueryByID(entity.ParentNodeID)).ToList()
                       : null;
        }
        public List<RoleInfoModel> QueryAllUserRoleByUserGroupId(string groupId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(groupId, 2);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDAL.Current.QueryByID(entity.ParentNodeID)).ToList()
                       : null;
        }

        public UserGroupModel QueryUserGroupByGroupName(string groupName)
        {
            return UserGroupDAL.Current.QueryByGroupName(groupName);
        }

        public RoleInfoModel QueryRoleInfoByRoleName(string roleName)
        {
            return RoleInfoDAL.Current.QueryByRoleName(roleName);
        }

        public WorkflowStateInfoModel QueryWorkflowStateInfoByCondition(string workflowName,
                                                                                           string stateNodeName)
        {
            return WorkflowStateInfoDAL.Current.QueryByWorkflowNameAndStateNodeName(workflowName, stateNodeName);
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionByRoleId(string roleId)
        {
            var relationList = RelationDAL.Current.QueryByParentNodeIDAndType(roleId, 3);
            return relationList.Select(relationModel => OperationActionInfoDAL.Current.QueryByID(relationModel.ChildNodeID));
        }

        public RoleInfoModel QueryRoleInfoByWorkflowStateId(string workflowStateId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(workflowStateId, 4);
            var relationEntity = relationList != null && relationList.Count > 0 ? relationList.First() : null;
            return relationEntity != null ? RoleInfoDAL.Current.QueryByID(relationEntity.ParentNodeID) : null;
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionByCondition(
            string workflowName, string stateNodeName)
        {
            var entity = QueryWorkflowStateInfoByCondition(workflowName, stateNodeName);
            if (entity != null)
                return QueryOperationActionByWorkflowStateId(entity.ID);
            return null;
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionByWorkflowStateId(string workflowStateId)
        {
            var roleInfoEntity = QueryRoleInfoByWorkflowStateId(workflowStateId);
            return roleInfoEntity != null ? QueryOperationActionByRoleId(roleInfoEntity.ID) : null;
        }

        public UserInfoModel QueryReportUserInfoByUserId(string userId)
        {
            var relationList = RelationDAL.Current.QueryByChildNodeIDAndType(userId, 6);
            return relationList != null && relationList.Any()
                       ? UserInfoDAL.Current.QueryByID(relationList.First().ParentNodeID)
                       : null;
        }

        #endregion
    }
}
