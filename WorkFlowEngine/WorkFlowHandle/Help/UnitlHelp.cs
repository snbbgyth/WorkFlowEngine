using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowHandle.Model;

namespace WorkFlowHandle.Help
{
    using System.Configuration;
    public class UnitlHelp
    {
        public static WorkflowHandlerSettingsConfigSection GetWorkflowHandlerSettingsConfigSection()
        {
            var configSection = ConfigurationManager.GetSection("workflowHandlerSettings") as WorkflowHandlerSettingsConfigSection;
            return configSection;
        }
    }
}
