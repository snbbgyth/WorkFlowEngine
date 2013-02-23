/********************************************************************************
** Class Name:   WorkFlowEngine 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowEngine class
*********************************************************************************/

using System;
using CommonLibrary.Help;
using WorkFlowHandle.Steps;
using WorkFlowService.DAL;
using WorkFlowService.Help;
using WorkFlowService.IDAL;
using WorkFlowHandle.BLL;
using WorkFlowService.Model;


namespace WorkFlowService.BLL
{
    public class WorkFlowEngine
    {

        public static WorkFlowEngine Current
        {
            get { return new WorkFlowEngine(); }
        }

        private IStateBase GetCurrentWorkFlowStateByWorkFlowState(WorkFlowState workFlowState)
        {
            foreach (var iStateBase in StateMapping.Instance.StateBasesList)
            {
                if (iStateBase.GetCurrentState() == workFlowState) return iStateBase;
            }
            return new CommonState();
        }

        public string Execute(string workflowName, string currentState, string activityState)
        {
            return WorkflowHandle.Instance.Run(workflowName, currentState, activityState);
        }

        public void InitWorkflowState(string workflowName)
        {
            var invokeStepList = WorkflowHandle.Instance.GetInvokeStepByWorkflowName(workflowName);
            foreach (var workflowStep in invokeStepList)
            {
                AddWorkflowStateInfoByWorkflowNameAndWorkflowStep(workflowName, workflowStep);
            }
        }

        private void AddWorkflowStateInfoByWorkflowNameAndWorkflowStep(string workflowName, WorkflowStep workflowStep)
        {
            var workflowStateEntity = GetWorkflowStateInfoByWorkflowNameAndStateNodeName(workflowName,
                                                                                           workflowStep.StepId);
            if (workflowStateEntity == null)
            {
                workflowStateEntity = new WorkflowStateInfoModel
                                          {
                                              ID = Guid.NewGuid().ToString(),
                                              CreateDateTime = DateTime.Now,
                                              LastUpdateDateTime = DateTime.Now,
                                              StateNodeName = workflowStep.StepId,
                                              StateNodeDisplayName = workflowStep.StepId,
                                              WorkflowName = workflowName,
                                              WorkflowDisplayName = workflowName
                                          };
            }
            else
            {
                workflowStateEntity.WorkflowDisplayName = workflowName;
                workflowStateEntity.StateNodeDisplayName = workflowStep.StepId;
                //Todo: modify workflowStateInfo 
            }
            WorkflowStateInfoDAL.Current.Modify(workflowStateEntity);
            
        }

        public WorkflowStateInfoModel GetWorkflowStateInfoByWorkflowNameAndStateNodeName(string workflowName,
                                                                                         string stateNodeName)
        {
          return  WorkflowStateInfoDAL.Current.QueryByWorkflowNameAndStateNodeName(workflowName, stateNodeName);
        }

        public ActivityState GetActivityStateByWorkFlowState(WorkFlowState workFlowState)
        {
            return GetCurrentWorkFlowStateByWorkFlowState(workFlowState).GetActivityState();
        }
    }
}
