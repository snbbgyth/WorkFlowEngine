/********************************************************************************
** Class Name:   WorkflowStateInfoDAL 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     WorkflowStateInfoDAL class
*********************************************************************************/

namespace WorkFlowService.DAL
{
    using System;
    using CommonLibrary.Help;
    using Help;
    using Model;

    public class WorkflowStateInfoDAL : DataOperationActivityBase<WorkflowStateInfoModel>
    {
        public static WorkflowStateInfoDAL Current
        {
            get { return new WorkflowStateInfoDAL(); }
        }
 
        protected override string GetInsertByEntitySql(WorkflowStateInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            return string.Format(WFConstants.InsertWorkflowStateInfoSqlTags, entity.ID, entity.WorkflowName, entity.WorkflowDisplayName, entity.StateNodeName, entity.StateNodeDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }
 
        protected override string GetModifyByEntitySql(WorkflowStateInfoModel entity)
        {
            return string.Format(WFConstants.InsertOrReplaceWorkflowStateInfoSqlTags, entity.ID, entity.WorkflowName, entity.WorkflowDisplayName, entity.StateNodeName, entity.StateNodeDisplayName,
                                 entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
        }
 
        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteWorkflowStateInfoByIDSqlTags, id);
        }
 
        protected override string GetQueryByIDSql(string id)
        {
            return string.Format(WFConstants.QueryWorkflowStateInfoByIDSqlTags, id);
        }

        public WorkflowStateInfoModel QueryByWorkflowNameAndStateNodeName(string workflowName, string stateNodeName)
        {
            var entityList = DBHelpInstance.ReadEntityList<WorkflowStateInfoModel>(GetQueryByWorkflowNameAndStateNodeNameSql(workflowName,stateNodeName));
            return entityList != null && entityList.Count > 0 ? entityList[0] : null;
        }

        private string GetQueryByWorkflowNameAndStateNodeNameSql(string workflowName, string stateNodeName)
        {
            return string.Format(WFConstants.QueryWorkflowStateInfoByWorkflowNameAndStateNodeNameSqlTags, workflowName,
                                 stateNodeName);
        }
 
        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllWorkflowStateInfoSqlTags;
        }

        protec verride string GetCreateTableSql()
        {
            return WFConstants.CreateWorkflowStateInfoTableSqlTags;
        }
    }
}
