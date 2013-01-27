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

        public const string QueryWorkFlowActivityByIDTags = "select * from WorkFlowActivity where ID='{0}'";

        #endregion

        #region WorkFlowActivityLog Table

        public const string CreateWorkFlowActivityLogTableSqlTags = @"CREATE TABLE [WorkFlowActivityLog] (ID varchar(50) NOT NULL PRIMARY KEY UNIQUE,OldID varchar(50) ,AppId varchar(50),WorkFlowState varchar(50),OperatorActivity varchar(50),CurrentWorkFlowState varchar(50),OperatorUserId varchar(50),CreateDateTime datetime,LastUpdateDateTime datetime,CreateUserId varchar(50),OperatorUserList varchar(2000),ApplicationState varchar(50),AppName varchar(200),IsDelete boolean)";

        public const string InsertWorkFlowActivityLogSqlTags = @"Insert into WorkFlowActivityLog(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string InsertOrReplaceWorkFlowActivityLogSqlTags = @"Insert or replace into WorkFlowActivityLog(ID,AppId,WorkFlowState,OperatorActivity,CurrentWorkFlowState,OperatorUserId,CreateDateTime,LastUpdateDateTime,CreateUserId,OperatorUserList,ApplicationState,AppName,IsDelete,OldID)values('{0}','{1}','{2}','{3}','{4}','{5}',datetime('{6}'),datetime('{7}'),'{8}','{9}','{10}','{11}',{12},'{13}')";

        public const string DeleteWorkFlowActivityLogByIDSqlTags = "delete from WorkFlowActivityLog where ID='{0}'";

        public const string QueryAllWorkFlowActivityLogSqlTags = "select * from WorkFlowActivityLog";

        public const string QueryWorkFlowActivityLogByIDTags = "select * from WorkFlowActivityLog where ID='{0}'";

        #endregion

    }
}
