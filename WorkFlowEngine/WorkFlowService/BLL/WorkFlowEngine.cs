/********************************************************************************
** Class Name:   WorkFlowEngine 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkFlowEngine class
*********************************************************************************/

using System.Collections.Generic;
using WorkFlowHandle.IDAL;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    using WorkFlowHandle.Model;
    using System;
    using System.Linq;
    using CommonLibrary.Help;
    using Help;
    using Model;

    public class WorkFlowEngine : IWorkFlowEngine
    {
        //public static WorkFlowEngine Current
        //{
        //    get { return new WorkFlowEngine(); }
        //}

        public WorkFlowEngine(IWorkflowHandle workflowHandle,IUserOperationDAL userOperationDAL)
        {
            WorkflowInstance = workflowHandle;
            UserOperationDAL = userOperationDAL;
        }

        private IWorkflowHandle WorkflowInstance { get; set; }
        private IUserOperationDAL UserOperationDAL { get; set; }

        public string Execute(string workflowName, string currentState, string activityState)
        {
            return WorkflowInstance.Run(workflowName, currentState, activityState);
        }

        public void InitWorkflowState(string workflowName)
        {
            var workflowContext = WorkflowInstance.GetWorkflowContextByWorkflowName(workflowName);
            AddInvokeStepRelation(workflowContext);
            AddRoleActionRelation(workflowContext);
        }


        public void AddStateRoleByCondition(string workflowName, string stateNodeName, PartnerLinkModel partnerLink)
        {
            var workflowStateEntity = GetWorkflowStateInfoByCondition(workflowName, stateNodeName);
            var roleInfoEntity = UserOperationDAL.QueryRoleInfoByWorkflowStateId(workflowStateEntity.Id);
            if (roleInfoEntity == null)
            {
                roleInfoEntity = UserOperationDAL.QueryRoleInfoByCondition(workflowName, partnerLink.MyRole);
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
                UserOperationDAL.AddWorkflowStateInRole(workflowStateEntity.Id, roleInfoEntity.Id);
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


        public WorkflowStateInfoModel GetWorkflowStateInfoByCondition(string workflowName, string stateNodeName)
        {
            return UserOperationDAL.QueryWorkflowStateInfoByCondition(workflowName, stateNodeName);
        }

        public IEnumerable<string> GetActivityStateByConditon(string workflowName, string workFlowState)
        {
            var entityList = UserOperationDAL.QueryOperationActionListByCondition(workflowName, workFlowState);
            return entityList.Select(entity => entity.ActionName);
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

        private IEnumerable<IInvokeStep> GetInvokeStepFromContext(WorkflowContext workflowContext)
        {
            return workflowContext.WorkflowStepList.OfType<IStepRunnerStep>().Select(stepRunnerStep => (from invokeStep in stepRunnerStep.WorkflowSteps.OfType<IInvokeStep>() select invokeStep)).FirstOrDefault();
        }

        public ApplicationState GetAppStateByCondition(string workflowName, string workflowState)
        {
            var workflowContext = WorkflowInstance.GetWorkflowContextByWorkflowName(workflowName);
            var currentStepList = workflowContext.WorkflowStepList.OfType<IStepRunnerStep>().Select(stepRunnerStep => (from invokeStep in stepRunnerStep.WorkflowSteps.OfType<IInvokeStep>() where invokeStep.InvokeContext.Name.CompareEqualIgnoreCase(workflowState) select invokeStep)).FirstOrDefault();
            var currentStep = currentStepList.Count() > 0 ? currentStepList.First() : null;
            if (currentStep != null && currentStep.InvokeContext.PortType.CompareEqualIgnoreCase(WFConstants.WorkflowEndPortTypeTags))
                return ApplicationState.Complete;
            return ApplicationState.InProgress;
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
            if (invokeStepList != null)
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
                                UserOperationDAL.QueryRoleInfoByCondition(workflowContext.WorkflowName,partnerLinkEntity.MyRole);
                            foreach (var actionEntity in switchStep.WorkflowSteps.OfType<ICaseStep>().Select(caseStep => AddActionByCondition(workflowContext.WorkflowName, caseStep.CaseContext.Condition)))
                            {
                                //UserOperationDAL.QueryOperationActionByCondition(workflowContext.WorkflowName,
                                //                                                         caseStep.CaseContext.Condition);
                                var relationEntity =
                                    UserOperationDAL.QueryRelationByActionIdAndRoleId(actionEntity.Id,
                                                                                              roleInfoEntity.Id);
                                if (relationEntity == null)
                                UserOperationDAL.AddOperationActionInRole(actionEntity.Id, roleInfoEntity.Id);
                            }
                        }
                    }
                }
        }



        private OperationActionInfoModel AddActionByCondition(string workflowName, string actionName)
        {
            var entity = UserOperationDAL.QueryOperationActionByCondition(workflowName, actionName);
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
                UserOperationDAL.DataOperationInstance.Insert(entity);
            }
            else
            {
                entity.LastUpdateDateTime = DateTime.Now;
                entity.IsDelete = false;
                entity.WorkflowDisplayName = workflowName;
                entity.ActionDisplayName = actionName;
                UserOperationDAL.DataOperationInstance.Modify(entity);
            }
            return entity;
        }

        private IEnumerable<ISwitchStep> GetSwitchStepFromContext(WorkflowContext workflowContext)
        {
            foreach (var stepRunnerStep in workflowContext.WorkflowStepList.OfType<IStepRunnerStep>())
            {
                foreach (var switchStep in stepRunnerStep.WorkflowSteps.OfType<ISwitchStep>())
                {
                    yield return switchStep;
                }
            }
        }

        private IEnumerable<ICaseStep> GetCaseStepFromContext(WorkflowContext workflowContext)
        {
            foreach (var stepRunnerStep in workflowContext.WorkflowStepList.OfType<IStepRunnerStep>())
            {
                foreach (var switchStep in stepRunnerStep.WorkflowSteps.OfType<ISwitchStep>())
                {
                    foreach (var caseStep in switchStep.WorkflowSteps.OfType<ICaseStep>())
                    {
                        yield return caseStep;
                    }
                }
            }
        }

        private void AddWorkflowStateInfoByCondition(string workflowName, IWorkflowStep workflowStep)
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

    }
}
