using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using WorkFlowHandle.IDAL;

namespace WorkFlowHandle.BLL
{
    public class WorkflowHandleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //Sql Authentication should be registered last as it will fall back to Forms if it is not enabled
            // or if there is a database issue

            //Authenticators
            builder.RegisterType<WorkflowHandle>().As<IWorkflowHandle>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WorkflowExecutionEngine>().As<IWorkflowExecutionEngine>().AsSelf().InstancePerLifetimeScope();

        }
    }
}
