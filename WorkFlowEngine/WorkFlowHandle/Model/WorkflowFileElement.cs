/********************************************************************************
** Class Name:   WorkflowFileElement 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowFileElement class
*********************************************************************************/

using System;
using Sy
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WorkFlowHandle.Model
{
    public class WorkflowFileElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of the WorkflowFileElement class.  This version
        /// of the constructor takes no arguments.
        /// </summary>
        public WorkflowFileElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the WorkflowFileElement class.  This version/// of the constructor constructs a fully populated WorkflowFileElement object.
        
        /// </summary>
        /// <param name="name">string containing the name of the workflow.  This/// same string must be used as the workflow name when starting the workflow.</param>
        
        /// <param name="version">A float value defining the version of the workflow.</param>/// <param name="fileName">A string defining the filename of the workflow.</param>
        
        public WorkflowFileElement(string name, float version, string fileName, string encryptedFileName)
        {
            this.Name = name;
            this.Version = version;
            this.FileName = fileName;
            this.EncryptedFileName = encryptedFileName;
        }

        /// <summary>/// Gets the name of the workflow, from the configuration file.  This name must correspond
        
        /// to the workflow name in the WorkflowNames class
        /// </summary>[ConfigurationProperty("name", DefaultValue = "", IsKey = false, IsRequired = true)]
        
        public string Name
        {
            get
            {
                return (string)base["name"];
            }

            private set
            {
                this["name"] = value;
            }
        }

        /// <summary>
        /// Gets the version of the BPEL file, from the configuration file.
        /// </summary>[ConfigurationProperty("version", DefaultValue = "1.0", IsKey = false, IsRequired = true)]
        
        public float Version
        {
            get
            {
                return (float)base["version"];
            }

            private set
            {
                this["version"] = value;
            }
        }

        /// <summary>/// Gets the file name of the BPEL file.  The path for the files is specified in the configuration key/value pair list
        
        /// </summary>[ConfigurationProperty("fileName", DefaultValue = "", IsKey = true, IsRequired = true)]
        
        public string FileName
        {
            get
            {
                return (string)base["fileName"];
            }

            private set
            {
                this["fileName"] = value;
            }
        }

        /// <summary>/// Gets the encrypted file name of the BPEL file.  The path for the files is specified in the configuration BPELWorkflows section
        
        /// </summary>[ConfigurationProperty("encryptedFileName", DefaultValue = "", IsKey = true, IsRequired = false)]
        
        public string EncryptedFileName
        {
            get
            {
                return (string)base["encryptedFileName"];
            }

            private set
            {
                this["encryptedFileName"] = value;
            }
        }
    }
}
