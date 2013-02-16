/********************************************************************************
** Class Name:   ProcessDefinitionEngine 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     ProcessDefinitionEngine class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using WorkFlowHandle.Model;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.DAL
{
    public class ProcessDefinitionEngine
    {
        /// <summary>
        /// Cache the XmlElement to improve performace.
        /// </summary>
        private IDictionary<WorkflowFileElement, XmlElement> cachedElements = new Dictionary<WorkflowFileElement, XmlElement>();

        /// <summary>
        /// Cache the WorkflowStep to improve performace.
        /// </summary>
        private IDictionary<string, ICollection<WorkflowStep>> cachedWorkflowSteps = new Dictionary<string, ICollection<WorkflowStep>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, ICollection<FaultHandler>> cachedFaultHandler = new Dictionary<string, ICollection<FaultHandler>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, Dictionary<string, string>> cachedMessageTimeoutHandler = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, string> cachedCancelHandler = new Dictionary<string, string>();

        /// <summary>
        /// contains string of folder containing BPEL files
        /// </summary>
        private string workflowFolder;

        /// <summary>
        /// Hash table containing the latest version of each workflow.
        /// The key for the table is the Workflow name string.
        /// </summary>
        private System.Collections.Hashtable defaultWorkflows;

        /// <summary>
        /// Table containing location of earlier versions of workflows.
        /// </summary>
        private WorkflowFilesCollection olderWorkflows;

        /// <summary>
        /// Used to lock loading of BPEL files so this only gets done once.
        /// </summary>
        private object lockObject = new object();

        /// <summary>
        /// Used to load the BPEL file.
        /// </summary>
        private XmlReader reader = null;

        /// <summary>
        /// Gets timeout parameters 
        /// </summary>
        private Dictionary<string, string> timeoutParameters = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets a HashTable of parameters used to communicate data between the
        /// various code blocks and execution steps in the workflow
        /// </summary>
        public virtual Dictionary<string, string> WorkflowTimeoutParameters
        {
            get
            {
                return this.timeoutParameters;
            }
            set
            {
                this.timeoutParameters = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ProcessDefinitionEngine class
        /// </summary>
        public ProcessDefinitionEngine()
        {
            // Temp for now - hard code the folder.
            this.workflowFolder = AppDomain.CurrentDomain.BaseDirectory;
            this.workflowFolder += string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}BPEL{0}", Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Loads latest version of a workflow from given workflow name.  
        /// Initialize context with all workflow information
        /// </summary>
        /// <param name="context">contains workflow name</param>
        /// <returns>true if workflow was found and successfully loaded. False otherwise</returns>
        public bool LoadNewWorkflow(WorkflowContext context)
        {
            lock (this.lockObject)
            {
                XmlElement rootElement = this.LoadWorkflowFile(context.Name, context.Version);
                if (rootElement == null)
                {
                    return false;
                }

                XmlNodeList childList = rootElement.ChildNodes;
                this.CloseWorkflowFile();

                // Set this before doing actual load so it doesn't trigger
                // a load on its own.
                context.IsWorkflowStepsLoaded = true;

                if (cachedWorkflowSteps.ContainsKey(context.Name) && cachedFaultHandler.ContainsKey(context.Name) && cachedMessageTimeoutHandler.ContainsKey(context.Name) && cachedCancelHandler.ContainsKey(context.Name))
                {
                    this.FillVariableList(context.WorkflowVariables, childList);
                    foreach (FaultHandler handler in cachedFaultHandler[context.Name])
                    {
                        context.FaultHandlers.Add(handler);
                    }

                    foreach (WorkflowStep step in cachedWorkflowSteps[context.Name])
                    {
                        context.WorkflowSteps.Add(step);
                    }

                    foreach (var dict in cachedMessageTimeoutHandler[context.Name])
                    {
                        context.MessageTimeoutEventHanlderDict.Add(dict.Key, dict.Value);
                    }

                    context.CancelEventHandlerName = cachedCancelHandler[context.Name];
                }
                else
                {
                    // Do the actual load
                    string cancelEventHandlerName = string.Empty;
                    this.FillStepList(context.WorkflowVariables, context.FaultHandlers, 0, context.WorkflowSteps, childList, context.MessageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                    context.CancelEventHandlerName = cancelEventHandlerName;
                    cachedWorkflowSteps.Add(context.Name, context.WorkflowSteps);
                    cachedFaultHandler.Add(context.Name, context.FaultHandlers);
                    cachedMessageTimeoutHandler.Add(context.Name, context.MessageTimeoutEventHanlderDict);
                    cachedCancelHandler.Add(context.Name, cancelEventHandlerName);
                }

                return true;
            }
        }

        /// <summary>
        /// Load stepData for specific workflow versi
        /// </summary>
        /// <param name="name">String containing name of workflow to load</param>
        /// <param name="version">String containing version of workflow to load</param>
        /// <param name="stepData">stepData object that is filled in with steps from 
        /// the specified BPEL workflow</param>
        /// <returns>true if workflow load was successful, false otherwise</returns>
        public bool LoadExistingWorkflow(string name, string version, StepData stepData)
        {
            XmlElement rootElement = this.LoadWorkflowFile(name, version);
            if (rootElement == null)
            {
                return false;
            }

            XmlNodeList childList = rootElement.ChildNodes;
            string cancelEventHandlerName = string.Empty;
            this.FillStepList(stepData.WorkflowVariables, stepData.FaultHandlers, 0, stepData.WorkflowSteps, childList, stepData.MessageTimeoutEventHanlderDict, ref cancelEventHandlerName);
            stepData.CancelEventHandlerName = cancelEventHandlerName;
            return true;
        }

        /// <summary>
        /// Adds a new workflow bpel file to the list of known workflows.  This could be
        /// a new workflow or an updated version to an existing workflow.
        /// </summary>
        /// <param name="name">The name of the workflow.  This must correspond to the workflow name
        /// used when starting the workflow.</param>
        /// <param name="version">The version of the workflow.  This must be greater than 0.</param>
        /// <param name="fileName">The filename of the BPEL file.  This is assumed to reside in the
        /// local BPEL folder.</param>
        /// /// <param name="encryptedFileName">encrypted file name of the BPEL workflow description file</param>
        public void AddNewWorkflow(string name, float version, string fileName, string encryptedFileName)
        {
            // ensure we have a collection to add to
            this.LoadWorkflowFileCollection();
            WorkflowFileElement fileElement = new WorkflowFileElement(name, version, fileName, encryptedFileName);
            lock (this.lockObject)
            {
                this.AddNewWorkflow(fileElement);
            }
        }

        /// <summary>
        /// Adds a new workflow bpel file to the list of known workflows.  This could be
        /// a new workflow or an updated version to an existing workflow.
        /// </summary>
        /// <param name="name">The name of the workflow.  This must correspond to the workflow name
        /// used when starting the workflow.</param>
        /// <param name="version">The version of the workflow.  This must be greater than 0.</param>
        /// <param name="workflowDescriptionData">BpelData for the new workflow.  This will be saved as a .bpel file.</param>
        /// <returns>Indication as to whether the workflow was added</returns>
        public bool AddNewWorkflow(string name, float version, object workflowDescriptionData)
        {
            string fileName = name + ".bpel";
            string encryptedFileName = name + ".bin";

            // save bpel data as a file
            if (workflowDescriptionData != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(this.workflowFolder + fileName))
                {
                    streamWriter.Write(workflowDescriptionData);
                }
            }

            if (File.Exists(this.workflowFolder + fileName))
            {
                // add the new element to the collection
                this.AddNewWorkflow(name, version, fileName, encryptedFileName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ensures that the local collection of BPEL files is loaded.  This collection
        /// is only loaded once.  It can be updated by the AddNewWorkflow method.
        /// </summary>
        private void LoadWorkflowFileCollection()
        {
            if (this.defaultWorkflows == null)
            {
                lock (this.lockObject)
                {
                    // check if the collection exists again just in case someone else got a lock 
                    // between when the check above found null and this method execution got the
                    // lock.
                    if (this.defaultWorkflows == null)
                    {

                        // parse the list of BPEL files into a hash table with the latest version 
                        // of each workflow and a 2nd table with earlier versions.  
                        // This allows for default workflows to be quick located while
                        // still being able to find earlier workflows in the infrequent cases 
                        // where this may be necessaary.
                        this.defaultWorkflows = new System.Collections.Hashtable();
                        this.olderWorkflows = new WorkflowFilesCollection();
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new workflow into one of the local workflow tables.
        /// If this is a newer version of an existing workflow, it is
        /// placed in the defaultWorlows table and the older version is
        /// placed in the olderWorkflows table. 
        /// </summary>
        /// <param name="fileElement">Class defining the new workflow to add.  The version
        /// member of this class is used to determine the latest version of a workflow.</param>
        private void AddNewWorkflow(WorkflowFileElement fileElement)
        {
            if (this.defaultWorkflows.ContainsKey(fileElement.Name))
            {
                WorkflowFileElement defaultItem = this.defaultWorkflows[fileElement.Name] as WorkflowFileElement;
                if (defaultItem.Version > fileElement.Version)
                {
                    // Add new  to older table
                    this.olderWorkflows.Add(fileElement);
                }
                else
                {
                    // Found a later version of a workflow
                    // Move item in default table to older table 
                    // and add new element to default table
                    this.olderWorkflows.Add(defaultItem);
                    this.defaultWorkflows[fileElement.Name] = fileElement;
                }
            }
            else
            {
                // first time this workflow has been encountered.
                // add it to the default table.
                this.defaultWorkflows.Add(fileElement.Name, fileElement);
            }
        }

        /// <summary>
        /// Find the correct Bpel file, given the name and version
        /// A version of 0 indicates that the latest version should be retrieved
        /// returns null lif can't find the specified workflow
        /// </summary>
        /// <param name="name">String containing name of workflow to load</param>
        /// <param name="version">String containing version of workflow to load</param>
        /// <returns>XmlElement corresponding to process element of BPEL file</returns>
        private XmlElement LoadWorkflowFile(string name, string version)
        {
            this.LoadWorkflowFileCollection();
            if (this.defaultWorkflows == null)
            {
                return null;
            }

            XmlElement rootElement = null;
            bool needLatestVersion = false;
            float requestedVersion;

            if (string.IsNullOrEmpty(version))
            {
                needLatestVersion = true;
                requestedVersion = 0;
            }
            else
            {
                requestedVersion = Convert.ToSingle(version, System.Globalization.CultureInfo.InvariantCulture);
            }

            // first check the defaultWorkflows table.  The olderWorkflows table is only used is if a 
            // requested version is not found in the defaultWorkflows table.
            WorkflowFileElement fileElement = this.defaultWorkflows[name] as WorkflowFileElement;
            if (fileElement != null)
            {
                // found one with the right name, check the version
                if (!needLatestVersion && (requestedVersion != fileElement.Version))
                {
                    // need to try to get this version from the olderWorkflows table
                    fileElement = this.GetOlderWorkflow(name, requestedVersion);
                }

                if (fileElement != null)
                {
                    if (cachedElements.ContainsKey(fileElement))
                    {
                        rootElement = cachedElements[fileElement];
                    }
                    else
                    {
                        // Create XML reader for this file
                        // Must be able to have more than one reader active at a time.
                        var doc = new XmlDocument();
                        var fileName = string.Empty;
                        using (Stream stream = new FileStream(this.workflowFolder + fileName, FileMode.OpenOrCreate))
                        {
                            doc.Load(stream);
                        }
                        rootElement = doc.DocumentElement;
                        if (!(rootElement.LocalName == "process"))
                        {
                            Debug.Fail("LoadNewWorkflow: Could not find process in " + fileElement.FileName);
                            rootElement = null;
                        }
                        else
                        {
                            cachedElements.Add(fileElement, rootElement);
                        }
                    }
                }
            }

            return rootElement;
        }

        /// <summary>
        /// close the opened bpel file
        /// </summary>
        private void CloseWorkflowFile()
        {
            if (this.reader != null)
            {
                this.reader.Close();
            }
        }

        /// <summary>
        /// Find a workflow with the specified name and version in the
        /// table of older workflows.  Only an exact match for the version 
        /// will return a workflow.  This is typically used to continue
        /// </summary>
        /// <param name="name">A string identifying the name of the workflow to find</param>
        /// <param name="version">A float indicating the version of the workflow to find.</param>
        /// <returns>A WorkflowFileElement matching the name and version if one can be located.  Null
        /// if no matching workflow can be located.</returns>
        private WorkflowFileElement GetOlderWorkflow(string name, float version)
        {
            foreach (WorkflowFileElement fileElement in this.olderWorkflows)
            {
                if ((name == fileElement.Name) && (version == fileElement.Version))
                {
                    return fileElement;
                }
            }

            return null;
        }

        /// <summary>
        /// Fill the various structures with step data from the BPEL XML file
        /// </summary>
        /// <param name="workflowVariables">Dictionary of workflow variables to populate with any
        /// variables defined in the BPEL file.</param>
        /// <param name="faultHandlers">Collection of FaultHandlers  to populate with any
      /// fault handlers defined in the BPEL file.</param>
        /// <param name="startElement">Integer value representing the starting element in the nodeList.</param>
        /// <param name="workflowStepList">Collection of WorkflowSteps  to populate with any
        /// steps defined in the BPEL file.</param>
        /// <param name="nodeList">XmlNodeList containing the BPEL data from the BPEL file</param>
        /// <param name="messageTimeoutEventHanlderDict">XmlNodeList containing the BPEL data from the BPEL file</param>
        private void FillStepList(IDictionary<string, string> workflowVariables, List<FaultHandler> faultHandlers, int startElement, List<WorkflowStep> workflowStepList, XmlNodeList nodeList, Dictionary<string, string> messageTimeoutEventHanlderDict, ref string cancelEventHandlerName)
        {
            WorkflowStep workflowStep;
            for (int i = startElement; i < nodeList.Count; i++)
            {
                XmlNode currentStep = nodeList[i];

                if (!string.IsNullOrEmpty(currentStep.Prefix))
                {
                    currentStep.Prefix = "bpel";
                }

                if (currentStep is System.Xml.XmlComment)
                {
                    continue;
                }

                if (currentStep.LocalName == "invoke")
                {
                    workflowStep = new InvokeStep(currentStep.Attributes);
                    workflowStepList.Add(workflowStep);
                }
                else if (currentStep.LocalName == "sequence")
                {
                    SequenceStep sequenceStep= new SequenceStep(currentStep.Attributes);
                    workflowStepList.Add(sequenceStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, sequenceStep.WorkflowSteps.ToList(), currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "scope")
                {
                    // can't get tool to produce invoke without having it enclosed in a scope.
                    // just ignore scope for now but get the internals as if at the same level.
                    this.FillStepList(workflowVariables, faultHandlers, 0, workflowStepList, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "receive")
                {
                    ReceiveStep receiveStep = new ReceiveStep(currentStep.Attributes);
                    workflowStepList.Add(receiveStep);
                    string messageName = String.Empty;
                    string timesetTime = String.Empty;
                    foreach (XmlAttribute attrib in currentStep.Attributes)
                    {
                        if (attrib.LocalName == "timeout")
                        {
                            timesetTime = attrib.Value;
                        }
                        if (attrib.LocalName == "variable")
                        {
                            messageName = attrib.Value;
                        }
                    }
                    if (!timeoutParameters.ContainsKey(messageName))
                    {
                        timeoutParameters.Add(messageName, timesetTime);
                    }

                }
                else if (currentStep.LocalName == "onEvent")
                {
                    OnEventStep onEventStep = new OnEventStep(currentStep.Attributes);

                    messageTimeoutEventHanlderDict.Add(onEventStep.EventKey, onEventStep.StepId);
                   // worktsageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "case")
                {
                    CaseStep caseStep = new CaseStep(currentStep.Attributes, false);
                    workflowStepList.Add(caseStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, caseStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "otherwise")
                {
                    CaseStep caseStep = new CaseStep(currentStep.Attributes, true);
                    workflowStepList.Add(caseStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, caseStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "while")
                {
                    WhileStep whileStep = new WhileStep(currentStep.Attributes);
                    workflowStepList.Add(whileStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, whileStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "pick")
                {
                    PickStep pickStep = new PickStep(currentStep.Attributes);
                    workflowStepList.Add(pickStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, pickStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "eventHandlers")
                {
                    //we will handle this section as same as handl scope
                    this.FillStepList(workflowVariables, faultHandlers, 0, workflowStepList, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "onMessage")
                {
                    OnMessageStep messageStep = new OnMessageStep(currentStep.Attributes);
                    workflowStepList.Add(messageStep);

                    string messageName = String.Empty;
                    string TimesetTime = String.Empty;
                    foreach (XmlAttribute attrib in currentStep.Attributes)
                    {
                        if (attrib.LocalName == "timeout")
                        {
                            TimesetTime = attrib.Value;
                        }
                        if (attrib.LocalName == "variable")
                        {
                            messageName = attrib.Value;
                        }
                    }
                    if (!timeoutParameters.ContainsKey(messageName))
                    {
                        timeoutParameters.Add(messageName, TimesetTime);
                    }

                    this.FillStepList(workflowVariables, faultHandlers, 0, messageStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "onEvent")
                {
                    OnEventStep onEventStep = new OnEventStep(currentStep.Attributes);

                    messageTimeoutEventHanlderDict.Add(onEventStep.EventKey, onEventStep.StepId);
                    workflowStepList.Add(onEventStep);
                    this.FillStepList(workflowVariables, faultHandlers, 0, onEventStep.WorkflowSteps, currentStep.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);


                }
                else if (currentStep.LocalName == "partnerLinks")
                {
                    // ignore this for now
                    continue;
                }
                else if (currentStep.LocalName == "variables")
                {
                    foreach (XmlNode variableNode in currentStep.ChildNodes)
                    {
                        if (variableNode.LocalName == "variable")
                        {
                            string variableName = null;
                            string variableType = null;
                            foreach (XmlAttribute variableAttribute in variableNode.Attributes)
                            {
                                if (variableAttribute.LocalName == "name")
                                {
                                    variableName = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "messageType")
                                {
                                    variableType = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "type")
                                {
                                    variableType = variableAttribute.Value;
                                }
                            }

                            if ((variableName != null) && (variableType != null))
                            {
                                workflowVariables.Add(variableName, variableType);
                            }
                        }
                    }
                }
                else if (currentStep.LocalName == "faultHandlers")
                {
                    foreach (XmlNode childNode in currentStep.ChildNodes)
                    {
                        FaultHandler faultHandler = new FaultHandler(childNode);
                        faultHandlers.Add(faultHandler);
                        this.FillStepList(workflowVariables, faultHandlers, 0, faultHandler.WorkflowSteps, childNode.ChildNodes, messageTimeoutEventHanlderDict, ref cancelEventHandlerName);
                    }
                }
                else
                {
                    Debug.Fail("Unhandled " + currentStep.Name + " of " + currentStep.InnerText);
                }
            }
        }

        /// <summary>
        /// Fill the WorkflowVariable and Handler field in workflow context. The workflowSteps will not be load from the bpel in this method.
        /// For the workflowSteps may be cached.
        /// </summary>
        /// <param name="workflowVariables">Dictionary of workflow variables to populate with any
        /// variables defined in the BPEL file.</param>
        /// <param name="nodeList">XmlNodeList containing the BPEL data from the BPEL file</param>
        private void FillVariableList(IDictionary<string, string> workflowVariables, XmlNodeList nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode currentStep = nodeList[i];
                if (currentStep.LocalName == "variables")
                {
                    foreach (XmlNode variableNode in currentStep.ChildNodes)
                    {
                        if (variableNode.LocalName == "variable")
                        {
                            string variableName = null;
                            string variableType = null;
                            foreach (XmlAttribute variableAttribute in variableNode.Attributes)
                            {
                                if (variableAttribute.LocalName == "name")
                                {
                                    variableName = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "messageType")
                                {
                                    variableType = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "type")
                                {
                                    variableType = variableAttribute.Value;
                                }
                            }

                            if ((variableName != null) && (variableType != null))
                            {
                                workflowVariables.Add(variableName, variableType);
                            }
                        }
                    }
                }
            }
        }
    }
}
