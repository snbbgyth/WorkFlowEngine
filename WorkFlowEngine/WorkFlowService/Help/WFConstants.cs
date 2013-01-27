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

        public const string SplitCharacterTag = ";#";

        pblic const string SqliteFileNameTags = "mydatabase.sqlite";

        #endregion

        #region WorkFlowActivity Table
        
        public const string CreateWorkFlowActivityTableSqlTags = @"CREATE TABLE [WorkFlowActivity] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,AppId varchar(50),WorkFlowState varchar(50),OperatorActivity varchar(50),CurrentWorkFlowState varchar(50),OperatorUserId varchar(50),CreateDateTime datetime,LastUpdateDateTime datetime,CreateUserId varchar(50),OperatorUserList varchar(2000),ApplicationState varchar(50),AppName varchar(200),IsDelete boolean)";

        public const string InsertWorkFlowActivitySqlTags = @"Insert into WorkFlowActivity(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12})";

        public const string InsertOrReplaceWorkFlowActivitySqlTags = @"Insert or replace into WorkFlowActivity(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12})";

        public const string DeleteWorkFlowActivityByIDSqlTags = "delete from WorkFlowActivity where ID='{0}'";

        public const string QueryAllWorkFlowActivitySqlTags = "select * from WorkFlowActivity";

        public const string QueryWorkFlowActivityByIDTags = "select * from WorkFlowActSqlTags = "select * from WorkFlowActivity where ID='{0}'";

        public const string QueryWorkFlowActivityByOperatorUserIDSqlTags =
            "select * from WorkFlowActivity where OperatorUserId='{0}'";

        public const string QueryWorkFlowActivityByAppIDSqlTags = "select * from WorkFlowActivity where AppId     #region WorkFlowActivityLog Table

        public const string CreateWorkFlowActivityLogTableSqlTags = @"CREATE TABLE [WorkFlowActivityLog] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,OldID varchar(50) ,AppId varchar(50),WorkFlowState varchar(50),OperatorActivity varchar(50),CurrentWorkFlowState varchar(50),OperatorUserId varchar(50),CreateDateTime datetime,LastUpdateDateTime datetime,CreateUserId varchar(50),OperatorUserList varchar(2000),ApplicationState varchar(50),AppName varchar(200),IsDelete boolean)";

        public const string InsertWorkFlowActivityLogSqlTags = @"Insert into WorkFlowActivityLog(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string InsertOrReplaceWorkFlowActivityLogSqlTags = @"Insert or replace into WorkFlowActivityLog(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string DeleteWorkFlowActivityLogByIDSqlTags = "delete from WorkFlowActivityLog where ID='{0}'";

        public const string QueryAllWorkFlowActivityLogSqlTags = "select * from WorkFlowActivityLog";

        public const string QueryWorkFlowActivityLogByIDTags = "select * from WorkFlowActivityLog where ID='{0}'";

        #endregion

    }
}

        #region UserInfo Table

         public const string CreateUserInfoTableSqlTags = @"CREATE TABLE [UserInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,UserName varchar(100),Password varchar(100),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

         public const string InsertUserInfoSqlTags = @"Insert into UserInfo(ID,UserName,Password, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

         public const string InsertOrReplaceUserInfoSqlTags = @"Insert or replace into UserInfo(ID,UserName,Password, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

         public const string DeleteUserInfoByIDSqlTags = "delete from UserInfo where ID='{0}'";

         public const string QueryAllUserInfoSqlTags = "select * from UserInfo";

         public const string QueryUserInfoByIDTags = "select * from UserInfo where ID='{0}'";

         public const string QueryUserInfoByUserNameAndPasswordTags = "select * from UserInfo where UserName='{0}' and Password='{1}'";

        #endregion

        #region UserRoleInfo Table

         public const string CreateUserRoleInfoTableSqlTags = @"CREATE TABLE [UserRoleInfo] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,UserID varchar(50),OperatorState varchar(100),CreateDateTime datetime,LastUpdateDateTime datetime,IsDelete boolean)";

         public const string InsertUserRoleInfoSqlTags = @"Insert into UserRoleInfo(ID,UserID,OperatorState, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

         public const string InsertOrReplaceUserRoleInfoSqlTags = @"Insert or replace into UserRoleInfo(ID,UserID,OperatorState, CreateDateTime,LastUpdateDateTime ,IsDelete)values('{0}','{1}','{2}',datetime('{3}'),datetime('{4}') ,{5})";

         public const string DeleteUserRoleInfoByIDSqlTags = "delete from UserRoleInfo where ID='{0}'";

         public const string QueryAllUserRoleInfoSqlTags = "select * from UserRoleInfo";

         public const string QueryUserRoleInfoByIDTags = "select * from UserRoleInfo where ID='{0}'";



        #endregion


    }
}
