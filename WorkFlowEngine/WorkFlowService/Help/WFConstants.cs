/********************************************************************************
** Class Name:   WFConstants 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WFConstants class
*********************************************************************************/

namespace WorkFlowService.Help
{
    public class WFConstants
    {
        #region Common

        public const string SplitCharacterTag = "#";

        public const string SqliteFileNameTags = "mydatabase.sqlite";

        public const string SQLiteConnectionString = "Data Source={0};Pooling=true;FailIfMissing=false";

        #endregion

        #region WorkFlowActivity Table

        public const string CreateWorkFlowActivityTableSqlTags = @"CREATE TABLE [WorkFlowActivity] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,AppId varchar(50),ForeWorkFlowState varchar(50),OperatorActivity varchar(50),CurrentWorkFlowState varchar(50),OperatorUserId varchar(50),CreateDateTime datetime,LastUpdateDateTime datetime,CreateUserId varchar(50),OperatorUserList varchar(2000),ApplicationState varchar(50),AppName varchar(200),IsDelete boolean)";

        public const string InsertWorkFlowActivitySqlTags = @"Insert into WorkFlowActivity(ID,AppId,ForeWorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12})";

        public const string InsertOrReplaceWorkFlowActivitySqlTags = @"Insert or replace into WorkFlowActivity(ID,AppId,ForeWorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12})";

        public const string DeleteWorkFlowActivityByIDSqlTags = "delete from WorkFlowActivity where ID='{0}'";

        public const string QueryAllWorkFlowActivitySqlTags = "select * from WorkFlowActivity";

        public const string QueryWorkFlowActivityByIDSqlTags = "select * from WorkFlowActivity where ID='{0}'";

        public const string QueryWorkFlowActivityByOperatorUserIDSqlTags =
            "select * from WorkFloe OperatorUserId='{0}'";

        public const string QueryWorkFlowActivityByAppIDSqlTags = "select * from WorkFlowActivity where AppId='{0}'";

        #endregion

        #region WorkFlowActivityLog Table

        public const string CreateWorkFlowActivityLogTableSqlTags = @"CREATE TABLE [WorkFlowActivityLog] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,OldID varchar(50) ,AppId varchar(50),ForeWorkFlowState varchar(50),OperatorActivity varchar(50),CurrentWorkFlowState varchar(50),OperatorUserId varchar(50),CreateDateTime datetime,LastUpdateDateTime datetime,CreateUserId varchar(50),OperatorUserList varchar(2000),ApplicationState varchar(50),AppName varchar(200),IsDelete boolean)";

        public const string InsertWorkFlowActivityLogSqlTags = @"Insert into WorkFlowActivityLog(ID,AppId,ForeWorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string InsertOrReplaceWorkFlowActivityLogSqlTags = @"Insert or replace into WorkFlowActivityLog(ID,AppId,ForeWorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string DeleteWorkFlowActivityLogByIDSqlTags = "delete from WorkFlowActivityLog where ID='{0}'";

        public const string QueryAllWorkFlowActivityLogSqlTags = "select * from WorkFlowActivityLog";

        public const string QueryWorkFlowActivityLogByIDTags = "select * from WorkFlowActivityLog where ID='{0}'";

        public const string QueryInProgressActivityByLogByUserOperatorIDSqlTags = "select * from WorkFlowActivityLog where OperatorUserId='{0}'";

        public const string QueryWorkFlowActivityLogByAppIdSqlTags =
            "select * from WorkFlowActivityLog where AppId='{0}'";


        #endregion

        #region UserInfo Table

        public const string CreateUserInfoTableSqlTags = @"CREATE TABLE [UserInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,UserName varchar(100),UserDisplayName varchar(500),Password varchar(100),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertUserInfoSqlTags = @"Insert into UserInfo(ID,UserName,UserDisplayName,Password, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}','{3}',datetime('{4}'),datetime('{5}') ,{6})";

        public const string InsertOrReplaceUserInfoSqlTags = @"Insert or replace into UserInfo(ID,UserName,UserDisplayName,Password, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}','{3}',datetime('{4}'),datetime('{5}') ,{6})";

        public const string DeleteUserInfoByIDSqlTags = "delete from UserInfo where ID='{0}'";

        public const string QueryAllUserInfoSqlTags = "select * from UserInfo";

        public const string QueryUserInfoByIDTags = "select * from UserInfo where ID='{0}'";

        public const string QueryUserInfoByUserNameAndPasswordTags = "select * from UserInfo where UserName='{0}' and Password='{1}'";

        public const string QueryUserInfoByUserNameSqlTags = "select * from UserInfo where UserName='{0}'";

        #endregion

        #region RoleInfo Table

        public const string CreateRoleInfoTableSqlTags = @"CREATE TABLE [RoleInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,RoleName varchar(200),RoleDisplayName varchar(500),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertRoleInfoSqlTags = @"Insert into RoleInfo(ID,RoleName,RoleDisplayName, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string InsertOrReplaceRoleInfoSqlTags = @"Insert or replace into RoleInfo(ID,RoleName,RoleDisplayName, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string DeleteRoleInfoByIDSqlTags = "delete from RoleInfo where ID='{0}'";

        public const string QueryAllRoleInfoSqlTags = "select * from RoleInfo";

        public const string QueryRoleInfoByIDSqlTags = "select * from RoleInfo where ID='{0}'";

        public const string QueryRoleInfoByUserIDSqlTags = "select * from RoleInfo where UserID='{0}'";

        public const string DeleteRoleInfoByUserIDSqlTags = "delete from RoleInfo where UserID='{0}'";

        public const string QueryRoleInfoByRoleNameSqlTags = "select * from RoleInfo where RoleName='{0}'";

        #endregion

        #region OperationActionInfo Table

        public const string CreateOperationActionInfoTableSqlTags = @"CREATE TABLE [OperationActionInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,ActionName varchar(200),ActionDisplayName varchar(500),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertOperationActionInfoSqlTags = @"Insert into OperationActionInfo(ID,ActionName,ActionDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string InsertOrReplaceOperationActionInfoSqlTags = @"Insert or replace into OperationActionInfo(ID,ActionName,ActionDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string DeleteOperationActionInfoByIDSqlTags = "delete from OperationActionInfo where ID='{0}'";

        public const string QueryAllOperationActionInfoSqlTags = "select * from OperationActionInfo";

        public const string QueryOperationActionInfoByIDSqlTags = "select * from OperationActionInfo where ID='{0}'";

        #endregion

        #region Relation Table

        public const string CreateRelationTableSqlTags = @"CREATE TABLE [Relation] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,ChildNodeID varchar(50),ParentNodeID varchar(50),Type integer,CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertRelationSqlTags = @"Insert into Relation(ID,ChildNodeID,ParentNodeID,Type,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',{3},datetime('{4}'),datetime('{5}') ,{6})";

        public const string InsertOrReplaceRelationSqlTags = @"Insert or replace into Relation(ID,ChildNodeID,ParentNodeID,Type,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',{3},datetime('{4}'),datetime('{5}') ,{6})";

        public const string DeleteRelationByIDSqlTags = "delete from Relation where ID='{0}'";

        public const string QueryAllRelationSqlTags = "select * from Relation";

        public const string QueryRelationByIDSqlTags = "select * from Relation where ID='{0}'";

        public const string QueryRelationByChildNodeIDAndParentNodeIDAndTypeSqlTags = "select * from Relation where ChildNodeID='{0}' and ParentNodeID='{1}' and Type={2} ";

        public const string QueryRelationByChildNodeIDAndTypeSqlTags = "select * from Relation where ChildNodeID='{0}'and Type={1} ";

        public const string QueryRelationByParentNodeIDAndTypeSqlTags = "select * from Relation where ParentNodeID='{0}'and Type={1} ";

        public const string DeleteRelationByChildNodeIDAndTypeSqlTags = "delete from Relation where ChildNodeID='{0}'and Type={1}";

        public const string DeleteRelationByParentNodeIDAndTypeSqlTags = "delete from Relation where ParentNodeID='{0}'and Type={1} ";

        public const string DeleteRelationByChildNodeIDAndParentNodeIDAndTypeSqlTags = "delete from Relation where ChildNodeID='{0}' and ParentNodeID='{1}' and Type={2} ";

        #endregion

        #region UserGroup Table

        public const string CreateUserGroupTableSqlTags = @"CREATE TABLE [UserGroup] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,GroupName varchar(200),GroupDisplayName varchar(500),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertUserGroupSqlTags = @"Insert into UserGroup(ID,GroupName,GroupDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string InsertOrReplaceUserGroupSqlTags = @"Insert or replace into UserGroup(ID,GroupName,GroupDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

        public const string DeleteUserGroupByIDSqlTags = "delete from UserGroup where ID='{0}'";

        public const string QueryAllUserGroupSqlTags = "select * from UserGroup";

        public const string QueryUserGroupByIDSqlTags = "select * from UserGroup where ID='{0}'";

        public const string QueryUserGroupByGroupNameSqlTags = "select * from UserGroup where GroupName='{0}'";

        #endregion

        #region UserGroup Table

        public const string CreateWorkflowStateInfoTableSqlTags = @"CREATE TABLE [WorkflowStateInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,WorkflowName varchar(200),WorkflowDisplayName varchar(500),StateNodeName varchar(200),StateNodeDisplayName varchar(500),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

        public const string InsertWorkflowStateInfoSqlTags = @"Insert into WorkflowStateInfo(ID,WorkflowName,WorkflowDisplayName,StateNodeName,StateNodeDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}','{3}','{4}',datetime('{5}'),datetime('{6}') ,{7})";

        public const string InsertOrReplaceWorkflowStateInfoSqlTags = @"Insert or replace into WorkflowStateInfo(ID,WorkflowName,WorkflowDisplayName,StateNodeName,StateNodeDisplayName,CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}','{3}','{4}',datetime('{5}'),datetime('{6}') ,{7})";

        public const string DeleteWorkflowStateInfoByIDSqlTags = "delete from WorkflowStateInfo where ID='{0}'";

        public const string QueryAllWorkflowStateInfoSqlTags = "select * from WorkflowStateInfo";

        public const string QueryWorkflowStateInfoByIDSqlTags = "select * from WorkflowStateInfo where ID='{0}'";

        public const string QueryWorkflowStateInfoByWorkflowNameAndStateNodeNameSqlTags = "select * from WorkflowStateInfo where WorkflowName='{0}' and StateNodeName='{1}'";

        #endregion
    }
}
