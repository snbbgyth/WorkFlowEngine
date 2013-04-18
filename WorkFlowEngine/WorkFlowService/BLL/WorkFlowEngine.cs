/********************************************************************************
** Class Name:   WorkFlowEngine 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowEngine class
*********************************************************************************/

using System.Collections.Generic;

namespace WorkFlowService.BLL
{
    using WorkFlowHandle.Model;
    using System;
    using System.Linq;
    using CommonLibrary.Help;
    using WorkFlowHandle.Steps;
    using Help;
    using WorkFlowHandle.BLL;
    using Model;

    public class WorkFlowEngine
    {
        public static WorkFlowEngine Current
        {
            get { return new WorkFlowEngine(); }
        }

        public string Execute(string workflowName, string currentState, string activityState)
        {
            return WorkflowHandle.Instance.Run(workflowName, currentState, activityState);
        }

        public void InitWorkflowState(string workflowName)
        {
            var workflowContext = WorkflowHandle.Instance.GetWorkflowContextByWorkflowName(workflowName);
            //var invokeStepList = GetInvokeStepFromContext(workflowContext);
            //if(invokeStepList!=null)
            //foreach (var workflowStep in invokeStepList)
            //{
            //    AddWorkflowStateInfoByCondition(workflowName, workflowStep);
            //    if (workflowStep == null) continue;
            //    var partnerLinkEntity =
            //        workflowContext.PartnerLinkList.First(
            //            entity => entity.Name.CompareEqualIgnoreCase(workflowStep.InvokeContext.PartnerLink));
            //    if (partnerLinkEntity != null)
            //        AddStateRoleByCondition(workflowName, workflowStep.InvokeContext.Name, partnerLinkEntity);
            //}
             AddInvokeStepRelation(workflowContext);
            AddRoleActionRelation(workflowContext);
        }

        private void AddInvokeStepRelation(WorkflowContext workflowContext)
        {
            var invokeStepList = GetInvokeStepFromContext(workflowContext);
            if (invokeStepList != null)
                foreach (var workflowStep in invokeStepList)
                {
                    AddWorkflowStateInfoByCondition(workflowContext.WorkflowName, workflowStep);
                    if (workflowStep == null) continue;
                    var partnerLinkEntity =
                        workflowContext.PartnerLinkList.First(
                            entity => entity.Name.CompareEqualIgnoreCase(workflowStep.InvokeContext.PartnerLink));
                    if (partnerLinkEntity != null)
                        AddStateRoleByCondition(workflowContext.WorkflowName, workflowStep.InvokeContext.Name, partnerLinkEntity);
                }
        }

        private IEnumerable<InvokeStep> GetInvokeStepFromContext(WorkflowContext workflowContext)
        {
            return workflowContext.WorkflowStepList.OfType<StepRunnerStep>().Select(stepRunnerStep => (from invokeStep in stepRunnerStep.WorkflowSteps.OfType<InvokeStep>() select invokeStep)).FirstOrDefault();
        }



        //private void AddCaseStepRelation(WorkflowContext workflowContext)
        //{
        //    var caseStepList = GetCaseStepFromContext(workflowContext);
        //    if (caseStepList != null&&caseStepList.Any())
        //    {
        //        foreach (var caseStep in caseStepList)
        //        {
        //            AddActionByCondition(workflowContext.WorkflowName, caseStep.CaseContext.Condition);
        //        }
        //    }
        //}

        private void AddRoleActionRelation(WorkflowContext workflowContext)
        {
            var switchStepList = GetSwitchStepFromContext(workflowContext);
            var invokeStepList = GetInvokeStepFromContext(workflowContext);
            if(invokeStepList!=null)
            foreach (var switchStep in switchStepList)
            {
                var invokeStep =
                    invokeStepList.FirstOrDefault(entity => entity.InvokeContext.PortType.CompareEqualIgnoreCase(switchStep.SwitchContext.Name));
                if (invokeStep != null)
                {
                    var partnerLinkEntity =
                        workflowContext.PartnerLinkList.First(
                            entity => entity.Name.CompareEqualIgnoreCase(invokeStep.InvokeContext.PartnerLink));
                    if (partnerLinkEntity != null)
                    {
                        var roleInfoEntity =
                            UserOperationBLL.Current.QueryRoleInfoByCondition(workflowContext.WorkflowName,
                                                                              partnerLinkEntity.MyRole);
                        foreach (var caseStep in switchStep.WorkflowSteps.OfType<CaseStep>())
                        {
                            var actionEntity = AddActionByCondition(workflowContext.WorkflowName, caseStep.CaseContext.Condition);
                                //UserOperationBLL.Current.QueryOperationActionByCondition(workflowContext.WorkflowName,
                                //                                                         caseStep.CaseContext.Condition);
                            UserOperationBLL.Current.AddOperationActionInRole(actionEntity.Id, roleInfoEntity.Id);
                        }
                       
                    }
                }
            }
        }



        private OperationActionInfoModel AddActionByCondition(string workflowName, string actionName)
        {
            var entity = UserOperationBLL.Current.QueryOperationActionByCondition(workflowName, actionName);
            if (entity == null)
            {
                entity = new OperationActionInfoModel
                             {
                                 ActionName = actionName,
                                 ActionDisplayName = actionName,
                                 CreateDateTime = DateTime.Now,
                                 LastUpdateDateTime = DateTime.Now,
                                 WorkflowName = workflowName,
                                 WorkflowDisplayName = workflowName
                             };
                UserOperationBLL.Current.DataOperationInstance.Insert(entity);
            }
            else
            {
                entity.LastUpdateDateTime = DateTime.Now;
                entity.IsDelete = false;
                entity.WorkflowDisplayName = workflowName;
                entity.ActionDisplayName = actionName;
                UserOperationBLL.Current.DataOperationInstance.Modify(entity);
            }
            return entity;
        }

        private IEnumerable<SwitchStep> GetSwitchStepFromContext(WorkflowContext workflowContext)
        {
            foreach (var stepRunnerStep in workflowContext.WorkflowStepList.OfType<StepRunnerStep>())
            {
                foreach (var switchStep in stepRunnerStep.WorkflowSteps.OfType<SwitchStep>())
                {
                    yield return switchStep;
                }
            }
        }

        private IEnumerable<CaseStep> GetCaseStepFromContext(WorkflowContext workflowContext)
        {
            foreach (var stepRunnerStep in workflowContext.WorkflowStepList.OfType<StepRunnerStep>())
            {
                foreach (var switchStep in stepRunnerStep.WorkflowSteps.OfType<SwitchStep>())
                {
                    foreach (var caseStep in switchStep.WorkflowSteps.OfType<CaseStep>())
                    {
                        yield return caseStep;
                    }
                }
            }
        }

        public void AddStateRoleByCondition(string workflowName, string stateNodeName, PartnerLinkModel partnerLink)
        {
            var workflowStateEntity = GetWorkflowStateInfoByCondition(workflowName, stateNodeName);
            var roleInfoEntity = UserOperationBLL.Current.QueryRoleInfoByWorkflowStateId(workflowStateEntity.Id);
            if (roleInfoEntity == null)
            {
                roleInfoEntity = UserOperationBLL.Current.QueryRoleInfoByCondition(workflowName, partnerLink.MyRole);
                if (roleInfoEntity == null)
                {
                    roleInfoEntity = new RoleInfoModel
                    {
                        CreateDateTime = DateTime.Now,
                        LastUpdateDateTime = DateTime.Now,
                        RoleDisplayName = partnerLink.MyRole,
                        RoleName = partnerLink.MyRole,
                        WorkflowName = workflowName,
                        WorkflowDisplayName = workflowName,
                    };
                    DataOperationBLL.Current.Insert(roleInfoEntity);

                }
                else
                {
                    roleInfoEntity.RoleDisplayName = partnerLink.MyRole;
                    roleInfoEntity.WorkflowDisplayName = workflowName;
                    roleInfoEntity.LastUpdateDateTime = DateTime.Now;
                    DataOperationBLL.Current.Modify(roleInfoEntity);
                }
                UserOperationBLL.Current.AddWorkflowStateInRole(workflowStateEntity.Id, roleInfoEntity.Id);
            }
            else
            {
                roleInfoEntity.RoleName = partnerLink.MyRole;
                roleInfoEntity.WorkflowName = workflowName;
                roleInfoEntity.RoleDisplayName = partnerLink.MyRole;
                roleInfoEntity.WorkflowDisplayName = workflowName;
                roleInfoEntity.LastUpdateDateTime = DateTime.Now;
                DataOperationBLL.Current.Modify(roleInfoEntity);
            }
        }

        private void AddWorkflowStateInfoByCondition(string workflowName, WorkflowStep workflowStep)
        {
            var workflowStateEntity = GetWorkflowStateInfoByCondition(workflowName, workflowStep.StepId);
            if (workflowStateEntity == null)
            {
                workflowStateEntity = new WorkflowStateInfoModel
                {
              
                    CreateDateTime = DateTime.Now,
                    LastUpdateDateTime = DateTime.Now,
                    StateNodeName = workflowStep.StepId,
                    StateNodeDisplayName = workflowStep.StepId,
                    WorkflowName = workflowName,
                    WorkflowDisplayName = workflowName
                };
                DataOperationBLL.Current.Insert(workflowStateEntity);
            }
            else
            {
                workflowStateEntity.WorkflowDisplayName = workflowName;
                workflowStateEntity.StateNodeDisplayName = workflowStep.StepId;
                //Todo: modify workflowStateInfo 
                DataOperationBLL.Current.Modify(workflowStateEntity);
            }
          
        }

        public WorkflowStateInfoModel GetWorkflowStateInfoByCondition(string workflowName, string stateNodeName)
        {
            return UserOperationBLL.Current.QueryWorkflowStateInfoByCondition(workflowName, stateNodeName);
        }

        public IEnumerable<string> GetActivityStateByConditon(string workflowName, string workFlowState)
        {
            var entityList = UserOperationBLL.Current.QueryOperationActionListByCondition(workflowName, workFlowState);
            return entityList.Select(entity => entity.ActionName);
        }
    }
}
