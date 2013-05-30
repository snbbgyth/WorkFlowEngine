/********************************************************************************
** Class Name:   UserOperationBLL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     UserOperationBLL class
*********************************************************************************/

using System.Reflection;
using CommonLibrary.Help;
using CommonLibrary.Model;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using Model;
    using NHibernateDAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserOperationBLL : IUserOperationDAL
    {
        #region Public Get New Instance

 

        public UserOperationBLL(IDataOperationDAL dataOperation,IRelationDAL relationDAL,IUserInfoDAL userInfoDAL,IWorkFlowActivityDAL workFlowActivityDAL,
           IWorkFlowActivityLogDAL workFlowActivityLogDAL,IUserGroupDAL userGroupDAL,IRoleInfoDAL roleInfoDAL,IWorkflowStateInfoDAL workflowStateInfoDAL, IOperationActionInfoDAL operationActionInfoDAL)
        {
            DataOperationInstance = dataOperation;
            RelationDALInstance = relationDAL;
            UserInfoDALInstance = userInfoDAL;
            WorkFlowActivityDALInstance = workFlowActivityDAL;
            WorkFlowActivityLogDALInstance = workFlowActivityLogDAL;
            UserGroupDALInstance = userGroupDAL;
            RoleInfoDALInstance = roleInfoDAL;
            WorkflowStateInfoDALInstance = workflowStateInfoDAL;
            OperationActionInfoDALInstance = operationActionInfoDAL;
        }

        public IDataOperationDAL DataOperationInstance { get; set; }

        private IRelationDAL RelationDALInstance { get; set; }

        private IUserInfoDAL UserInfoDALInstance { get; set; }

        private IWorkFlowActivityDAL WorkFlowActivityDALInstance { get; set; }

        private IWorkFlowActivityLogDAL WorkFlowActivityLogDALInstance { get; set; }

        private IUserGroupDAL UserGroupDALInstance { get; set; }

        private IRoleInfoDAL RoleInfoDALInstance { get; set; }

        private IWorkflowStateInfoDAL WorkflowStateInfoDALInstance { get; set; }

        private IOperationActionInfoDAL OperationActionInfoDALInstance { get; set; }

  

        #endregion

        #region User operation

        public bool CreateUser(string userName, string userDisplayName, string password)
        {
            return DataOperationInstance.Insert(new UserInfoModel
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
            var entity = DataOperationInstance.QueryByID<UserInfoModel>(userId);
            entity.Password = password;
            return DataOperationInstance.Modify(entity) > 0;
        }

        public bool ModifyPasswordByUserName(string userName, string password)
        {
            var entity = UserInfoDALInstance.QueryByUserName(userName);
            entity.Password = password;
            return DataOperationInstance.Modify(entity) > 0;
        }

        public bool DeleteUserByUserID(string userId)
        {
            DataOperationInstance.Remove<UserInfoModel>(userId);
            DeleteUserAllUserGroupRelation(userId);
            DeleteUserAllRoleRelation(userId);
            return true;
        }

        public string LoginIn(string userName, string password)
        {
            return UserInfoDALInstance.Login(userName, password);
        }

        #endregion

        #region User relation UserGroup Type enqual 1

        public bool AddUserInUserGroup(string userId, string userGroupId)
        {
            return
                DataOperationInstance.Insert(new RelationModel
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
            return RelationDALInstance.DeleteByChildNodeIDAndParentNodeIDAndType(userId, userGroupId, 1) > 0;
        }

        public int DeleteUserAllUserGroupRelation(string userId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndType(userId, 1);
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
               DataOperationInstance.Insert(new RelationModel
               {
                   ChildNodeID = userGroupId,
                   ParentNodeID = roleID,
                   Type = 2,
                   CreateDateTime = DateTime.Now,
                   LastUpdateDateTime = DateTime.Now,
               }) > 0;
        }

        public int DeleteUserGroupAllRoleRelation(string userGroupId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndType(userGroupId, 2);
        }

        public int DeleteUserGroupAllUserRelation(string userGroupId)
        {
            return RelationDALInstance.DeleteByParentNodeIDAndType(userGroupId, 1);
        }

        public bool DeleteUserGroupRole(string userGroupId, string roleId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndParentNodeIDAndType(userGroupId, roleId, 2) > 0;
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
            return DataOperationInstance.Insert(new RelationModel
            {
                ChildNodeID = operationActionId,
                ParentNodeID = roleId,
                Type = 3,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now
            }) > 0;
        }

        public RelationModel QueryRelationByActionIdAndRoleId(string operationActionId, string roleId)
        {
            return RelationDALInstance.QueryByChildNodeIDAndParentNodeIDAndType(operationActionId, roleId, 3);
        }

        public int DeleteRoleAllActionRelation(string roleId)
        {
            return RelationDALInstance.DeleteByParentNodeIDAndType(roleId, 3);
        }

        public bool DeleteOperationActionInRole(string operationActionId, string roleId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndParentNodeIDAndType(operationActionId, roleId, 3) > 0;
        }

        public int DeleteActionAllRoleRelation(string operationActionId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndType(operationActionId, 3);
        }

        #endregion

        #region WorkflowStateInfo relation RoleInfo Type enqual 4

        public bool AddWorkflowStateInRole(string workflowStateId, string roleId)
        {
            return
               DataOperationInstance.Insert(new RelationModel
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
            return RelationDALInstance.DeleteByParentNodeIDAndType(roleId, 4);
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
               DataOperationInstance.Insert(new RelationModel
               {
                   ChildNodeID = userId,
                   ParentNodeID = roleId,
                   Type = 5,
                   CreateDateTime = DateTime.Now,
                   LastUpdateDateTime = DateTime.Now
               }) > 0;
        }

        public int DeleteUserAllRoleRelation(string userId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndType(userId, 5);
        }

        public bool DeleteUserRole(string userId, string roleId)
        {
            return RelationDALInstance.DeleteByChildNodeIDAndParentNodeIDAndType(userId, roleId, 5) > 0;
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
                DataOperationInstance.Insert(new RelationModel
                {
                    ChildNodeID = userId,
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    ParentNodeID = reportUserId,
                    Type = 6
                }) > 0;
        }

        #endregion

        #region Workflow activity operation

        public bool MoveToActivityLog(WorkFlowActivityModel entity)
        {
            try
            {
                DataOperationInstance.Insert(ConvertToActivityLog(entity));
                WorkFlowActivityDALInstance.DeleteByID(entity.Id);
                return true;
            }
            catch (Exception ex)
            {
                LogHelp.Instance.Write(ex, MessageType.Error, GetType(), MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }

        public WorkFlowActivityLogModel ConvertToActivityLog(WorkFlowActivityModel entity)
        {
            return new WorkFlowActivityLogModel
            {
                ApplicationState = entity.ApplicationState,
                AppName = entity.AppName,
                CreateDateTime = DateTime.Now,
                CreateUserId = entity.CreateUserId,
                CurrentWorkflowState = entity.CurrentWorkflowState,
                ForeWorkFlowState = entity.ForeWorkflowState,
                AppId = entity.AppId,
                LastUpdateDateTime = DateTime.Now,
                OldID = entity.Id,
                OperatorActivity = entity.OperatorActivity,
                OperatorUserId = entity.OperatorUserId,
                OperatorUserList = entity.OperatorUserList,
                WorkflowName = entity.WorkflowName
            };
        }

        public WorkFlowActivityLogModel QueryActivityLogByOldId(string oldId)
        {
            try
            {
                return WorkFlowActivityLogDALInstance.QueryByOldId(oldId);
            }
            catch (Exception ex)
            {
                LogHelp.Instance.Write(ex, MessageType.Error, GetType(), MethodBase.GetCurrentMethod().Name);
                return null;
            }

        }

        public WorkFlowActivityLogModel QueryActivityLogByAppId(string appId)
        {
            try
            {
                return WorkFlowActivityLogDALInstance.QueryByAppId(appId);
            }
            catch (Exception ex)
            {
                LogHelp.Instance.Write(ex, MessageType.Error, GetType(), MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        #endregion

        #region Query operation

        public UserInfoModel QueryUserInfoByUserName(string userName)
        {
            return UserInfoDALInstance.QueryByUserName(userName);
        }

        public List<UserInfoModel> QueryAllUserInfoByUserGroupId(string userGroupId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(userGroupId, 1);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => UserInfoDALInstance.QueryByID(entity.ChildNodeID)).ToList()
                       : null;
        }

        public List<UserGroupModel> QueryAllUserGroupByUserId(string userId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(userId, 1);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => UserGroupDALInstance.QueryByID(entity.ParentNodeID)).ToList()
                       : null;
        }

        public List<UserGroupModel> QueryAllUserGroupByRoleId(string roleId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(roleId, 2);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => UserGroupDALInstance.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<UserInfoModel> QueryAllUserInfoByRoleId(string roleId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(roleId, 5);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => UserInfoDALInstance.QueryByID(entity.ChildNodeID)).ToList()
                        : null;
        }

        public List<OperationActionInfoModel> QueryAllActionInfoByRoleId(string roleId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(roleId, 3);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => OperationActionInfoDALInstance.QueryByID(entity.ChildNodeID)).ToList(): null;
        }

        public List<WorkflowStateInfoModel> QueryAllWorkflowStateByRoleId(string roleId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(roleId, 4);
            return relationList != null && relationList.Count > 0
                        ? relationList.Select(entity => WorkflowStateInfoDALInstance.QueryByID(entity.ChildNodeID)).ToList(): null;
        }

        public List<RoleInfoModel> QueryAllUserRoleByUserId(string userId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(userId, 5);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDALInstance.QueryByID(entity.ParentNodeID)).ToList(): null;
        }

        public List<RoleInfoModel> QueryAllRoleByActionId(string operationActionId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(operationActionId, 3);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDALInstance.QueryByID(entity.ParentNodeID)).ToList(): null;
        }
        public List<RoleInfoModel> QueryAllUserRoleByUserGroupId(string groupId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(groupId, 2);
            return relationList != null && relationList.Count > 0
                       ? relationList.Select(entity => RoleInfoDALInstance.QueryByID(entity.ParentNodeID)).ToList(): null;
        }

        public UserGroupModel QueryUserGroupByGroupName(string groupName)
        {
            return UserGroupDALInstance.QueryByGroupName(groupName);
        }

        public RoleInfoModel QueryRoleInfoByCondition(string workflowName, string roleName)
        {
            return RoleInfoDALInstance.QueryByCondition(workflowName, roleName);
        }

        public WorkflowStateInfoModel QueryWorkflowStateInfoByCondition(string workflowName,
                                                                                           string stateNodeName)
        {
            return WorkflowStateInfoDALInstance.QueryByWorkflowNameAndStateNodeName(workflowName, stateNodeName);
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionByRoleId(string roleId)
        {
            var relationList = RelationDALInstance.QueryByParentNodeIDAndType(roleId, 3);
            return relationList.Select(relationModel => OperationActionInfoDALInstance.QueryByID(relationModel.ChildNodeID));
        }

        public RoleInfoModel QueryRoleInfoByWorkflowStateId(string workflowStateId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(workflowStateId, 4);
            var relationEntity = relationList != null && relationList.Count > 0 ? relationList.First() : null;
            return relationEntity != null ? RoleInfoDALInstance.QueryByID(relationEntity.ParentNodeID) : null;
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionListByCondition(
            string workflowName, string stateNodeName)
        {
            var entity = QueryWorkflowStateInfoByCondition(workflowName, stateNodeName);
            if (entity != null)
                return QueryOperationActionByWorkflowStateId(entity.Id);
            return null;
        }

        public IEnumerable<OperationActionInfoModel> QueryOperationActionByWorkflowStateId(string workflowStateId)
        {
            var roleInfoEntity = QueryRoleInfoByWorkflowStateId(workflowStateId);
            return roleInfoEntity != null ? QueryOperationActionByRoleId(roleInfoEntity.Id) : null;
        }

        public OperationActionInfoModel QueryOperationActionByCondition(string workflowName, string actionName)
        {
            return OperationActionInfoDALInstance.QueryByCondition(workflowName, actionName);
        }

        public UserInfoModel QueryReportUserInfoByUserId(string userId)
        {
            var relationList = RelationDALInstance.QueryByChildNodeIDAndType(userId, 6);
            return relationList != null && relationList.Any() && !string.IsNullOrEmpty(relationList.First().ParentNodeID)
                       ? UserInfoDALInstance.QueryByID(relationList.First().ParentNodeID): null;
        }

        public RelationModel QueryReportRelationByCondition(string userId, string reportUserId)
        {
            return RelationDALInstance.QueryByChildNodeIDAndParentNodeIDAndType(userId, reportUserId, 6);
        }

        public IList<WorkFlowActivityModel> QueryActivityByCondition(KeyValuePair<string, string> workflowParam,
                                                             KeyValuePair<string, object> conditionParam)
        {
            return WorkFlowActivityDALInstance.QueryByCondition(workflowParam, conditionParam);
        }

        public IList<WorkFlowActivityLogModel> QueryActivityLogByCondition(KeyValuePair<string, string> workflowParam,
                                                     KeyValuePair<string, object> conditionParam)
        {
            return WorkFlowActivityLogDALInstance.QueryByCondition(workflowParam, conditionParam);
        }

        #endregion

        #region Delete unit test data

        public void ClearUnitTestData()
        {
            DataOperationInstance.RemoveAll<WorkFlowActivityModel>();
            DataOperationInstance.RemoveAll<WorkFlowActivityLogModel>();
        }

        #endregion
    }
}
