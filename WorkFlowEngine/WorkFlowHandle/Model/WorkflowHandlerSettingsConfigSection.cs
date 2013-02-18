using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WorkFlowHandle.Model
{
    /// <summary>
    /// The class that will have the XML config file data loaded into it via the Configuration Manager.
    /// </summary>
    public class WorkflowHandlerSettingsConfigSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the Collection of implementation-specific workflow settings
        /// </summary>
        [ConfigurationProperty("implementationSettings", IsKey = false, IsRequired = false)]
        public KeyValueConfigurationCollection ImplementationSettings
        {
            get { return (KeyValueConfigurationCollection)base["implementationSettings"]; }
        }

        /// <summary>
        /// Gets the Collection of BPEL workflow files specified in configuration file
        /// </summary>
        [ConfigurationProperty("BPELWorkflows")]
        public WorkflowFilesCollection WorkflowFiles
        {
            get { return (WorkflowFilesCollection)base["BPELWorkflows"]; }
        }

 

    }
}
