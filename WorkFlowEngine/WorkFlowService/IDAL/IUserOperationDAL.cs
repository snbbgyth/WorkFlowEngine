using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Model;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
   public interface IUserOperationDAL
   {
       IDataOperationDAL DataOperationInstance { get; set; }
       bool CreateUser(string userName, string userDisplayName, string password);
       bool ModifyPasswordByUserID(string userId, string password);
       bool ModifyPasswordByUserName(string userName, string password);
       bool DeleteUserByUserID(string userId);
       string LoginIn(string userName, string password);
       bool AddUserInUserGroup(string userId, string userGroupId);
       bool DeleteUserInUserGroup(string userId, string userGroupId);
       int DeleteUserAllUserGroupRelation(string userId);
       bool ModifyUserInUserGroup(string userId, string oldUserGroupId, string newUserGroupId);
       bool AddUserGroupRole(string userGroupId, string roleID);
       int DeleteUserGroupAllRoleRelation(string userGroupId);
       int DeleteUserGroupAllUserRelation(string userGroupId);
       bool DeleteUserGroupRole(string userGroupId, string roleId);
       bool ModifyUserGroupRole(string userGoupId, string oldRoleId, string newRoleId);
       bool AddOperationActionInRole(string operationActionId, string roleId);
       RelationModel QueryRelationByActionIdAndRoleId(string operationActionId, string roleId);
       int DeleteRoleAllActionRelation(string roleId);
       bool DeleteOperationActionInRole(string operationActionId, string roleId);
       int DeleteActionAllRoleRelation(string operationActionId);
       bool AddWorkflowStateInRole(string workflowStateId, string roleId);
       int DeleteRoleWorkflowState(string roleId);
       bool ModifyRoleWorkflowState(string roleId, string newWorkflowStateId);
       bool AddUserRole(string userId, string roleId);
       int DeleteUserAllRoleRelation(string userId);
       bool DeleteUserRole(string userId, string roleId);
       bool ModifyUserRole(string userId, string oldRoleId, string newRoleId);
       bool AddUserReportToUser(string userId, string reportUserId);
       bool MoveToActivityLog(WorkFlowActivityModel entity);
        
       WorkFlowActivityLogModel QueryActivityLogByOldId(string oldId);
       WorkFlowActivityLogModel QueryActivityLogByAppId(string appId);
       UserInfoModel QueryUserInfoByUserName(string userName);
       List<UserInfoModel> QueryAllUserInfoByUserGroupId(string userGroupId);
       List<UserGroupModel> QueryAllUserGroupByUserId(string userId);
       List<UserGroupModel> QueryAllUserGroupByRoleId(string roleId);
       List<UserInfoModel> QueryAllUserInfoByRoleId(string roleId);
       List<OperationActionInfoModel> QueryAllActionInfoByRoleId(string roleId);
       List<WorkflowStateInfoModel> QueryAllWorkflowStateByRoleId(string roleId);
       List<RoleInfoModel> QueryAllUserRoleByUserId(string userId);
       List<RoleInfoModel> QueryAllRoleByActionId(string operationActionId);
       List<RoleInfoModel> QueryAllUserRoleByUserGroupId(string groupId);
       UserGroupModel QueryUserGroupByGroupName(string groupName);
       RoleInfoModel QueryRoleInfoByCondition(string workflowName, string roleName);

       WorkflowStateInfoModel QueryWorkflowStateInfoByCondition(string workflowName,string stateNodeName);
       IEnumerable<OperationActionInfoModel> QueryOperationActionByRoleId(string roleId);
       RoleInfoModel QueryRoleInfoByWorkflowStateId(string workflowStateId);

       IEnumerable<OperationActionInfoModel> QueryOperationActionListByCondition(string workflowName, string stateNodeName);
       IEnumerable<OperationActionInfoModel> QueryOperationActionByWorkflowStateId(string workflowStateId);
       OperationActionInfoModel QueryOperationActionByCondition(string workflowName, string actionName);
       UserInfoModel QueryReportUserInfoByUserId(string userId);
       RelationModel QueryReportRelationByCondition(string userId, string reportUserId);

       IList<WorkFlowActivityModel> QueryActivityByCondition(KeyValuePair<string, string> workflowParam,KeyValuePair<string, object> conditionParam);

       IList<WorkFlowActivityLogModel> QueryActivityLogByCondition(KeyValuePair<string, string> workflowParam,
                                                                   KeyValuePair<string, object> conditionParam);

       void ClearUnitTestData();
   }
}
