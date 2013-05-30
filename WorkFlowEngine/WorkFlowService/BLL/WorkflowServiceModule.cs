using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;


namespace WorkFlowService.BLL
{
    using IDAL;
    using NHibernateDAL;
    public class WorkflowServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //Sql Authentication should be registered last as it will fall back to Forms if it is not enabled
            // or if there is a database issue

            builder.RegisterType<WorkFlowManage>().As<IWorkFlowActivity>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowEngine>().As<IWorkFlowEngine>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataOperationBLL>().As<IDataOperationDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RelationDAL>().As<IRelationDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoDAL>().As<IUserInfoDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowActivityDAL>().As<IWorkFlowActivityDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowActivityLogDAL>().As<IWorkFlowActivityLogDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserGroupDAL>().As<IUserGroupDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoleInfoDAL>().As<IRoleInfoDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WorkflowStateInfoDAL>().As<IWorkflowStateInfoDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<OperationActionInfoDAL>().As<IOperationActionInfoDAL>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserOperationBLL>().As<IUserOperationDAL>().AsSelf().InstancePerLifetimeScope();
           

        }
    }
}
