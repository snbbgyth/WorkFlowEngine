/********************************************************************************
** Class Name:   ProcessDefinitionEngine 
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
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
    using Help;
    using CommonLibrary.Help;
    public class ProcessDefinitionEngine
    {
        /// <summary>
        /// Cache the XmlElement to improve performace.
        /// </summary>
        private IDictionary<WorkflowFileElement, XmlElement> _cachedElements = new Dictionary<WorkflowFileElement, XmlElement>();

        /// <summary>
        /// Cache the WorkflowStep to improve performace.
        /// </summary>
        private IDictionary<string, ICollection<WorkflowStep>> _cachedWorkflowSteps = new Dictionary<string, ICollection<WorkflowStep>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, ICollection<FaultHandler>> _cachedFaultHandler = new Dictionary<string, ICollection<FaultHandler>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, Dictionary<string, string>> _cachedMessageTimeoutHandler = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Cache the FaultHandler to improve performace.
        /// </summary>
        private IDictionary<string, string> _cachedCancelHandler = new Dictionary<string, string>();

        private IDictionary<string, Dictionary<string, string>> _cachedVariables = new Dictionary<string, Dictionary<string, string>>();

        private IDictionary<string, List<PartnerLinkModel>> _cachedPartnerLinks =new Dictionary<string, List<PartnerLinkModel>>(); 

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


        public static ProcessDefinitionEngine Current
        {
            get { return new ProcessDefinitionEngine(); }
        }


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
            this.workflowFolder += string.Format(System.Globalization.CultureInfo.InvariantCulture, Constants.BpelFileFolderTags, Path.DirectorySeparatorChar);
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
                XmlElement rootElement = this.LoadWorkflowFile(context.WorkflowName, context.Version);
                if (rootElement == null)
                {
                    return false;
                }

                XmlNodeList childList = rootElement.ChildNodes;
                this.CloseWorkflowFile();

                // Set this before doing actual load so it doesn't trigger
                // a load on its own.
                context.IsWorkflowStepsLoaded = true;

                if (_cachedWorkflowSteps.ContainsKey(context.WorkflowName) && _cachedFaultHandler.ContainsKey(context.WorkflowName) && _cachedMessageTimeoutHandler.ContainsKey(context.WorkflowName) && _cachedCancelHandler.ContainsKey(context.WorkflowName))
                {
                    //this.FillVariableList(context.WorkflowVariables, childList);
                    foreach (FaultHandler handler in _cachedFaultHandler[context.WorkflowName])
                    {
                        context.FaultHandlers.Add(handler);
                    }

                    foreach (WorkflowStep step in _cachedWorkflowSteps[context.WorkflowName])
                    {
                        context.WorkflowStepList.Add(step);
                    }

                    foreach (var dict in _cachedMessageTimeoutHandler[context.WorkflowName])
                    {
                        context.MessageTimeoutEventHanlderDict.Add(dict.Key, dict.Value);
                    }

                    foreach (var cachedVariable in _cachedVariables[context.WorkflowName])
                    {
                        context.WorkflowVariables.Add(cachedVariable.Key, cachedVariable.Value);
                    }

                    foreach (var cachedPartnerLink in _cachedPartnerLinks[context.WorkflowName])
                    {
                        context.PartnerLinkList.Add(cachedPartnerLink);
                    }

                    context.CancelEventHandlerName = _cachedCancelHandler[context.WorkflowName];
                }
                else
                {
                    // Do the actual load
                    string cancelEventHandlerName = string.Empty;
                    this.FillStepList(context,context.WorkflowStepList, 0, childList, ref cancelEventHandlerName);
                    context.CancelEventHandlerName = cancelEventHandlerName;
                    _cachedWorkflowSteps.Add(context.WorkflowName, context.WorkflowStepList);
                    _cachedFaultHandler.Add(context.WorkflowName, context.FaultHandlers);
                    _cachedMessageTimeoutHandler.Add(context.WorkflowName, context.MessageTimeoutEventHanlderDict);
                    _cachedCancelHandler.Add(context.WorkflowName, cancelEventHandlerName);
                    _cachedVariables.Add(context.WorkflowName, context.WorkflowVariables);
                    _cachedPartnerLinks.Add(context.WorkflowName,context.PartnerLinkList);
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
        //public bool LoadExistingWorkflow(string name, string version, StepData stepData)
        //{
        //    XmlElement rootElement = this.LoadWorkflowFile(name, version);
        //    if (rootElement == null)
        //    {
        //        return false;
        //    }

        //    XmlNodeList childList = rootElement.ChildNodes;
        //    string cancelEventHandlerName = string.Empty;
        //    this.FillStepList(stepData.WorkflowVariables, stepData.FaultHandlers, 0, stepData.WorkflowSteps, childList, stepData.MessageTimeoutEventHanlderDict, ref cancelEventHandlerName);
        //    stepData.CancelEventHandlerName = cancelEventHandlerName;
        //    return true;
        //}

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


                        // load list of BPEL files into local collection
                        WorkflowHandlerSettingsConfigSection section = UnitlHelp.GetWorkflowHandlerSettingsConfigSection();

                        // parse the list of BPEL files into a hash table with the latest version 
                        // of each workflow and a 2nd table with earlier versions.  
                        // This allows for default workflows to be quickly located while
                        // still being able to find earlier workflows in the infrequent cases 
                        // where this may be necessaary.
                        this.defaultWorkflows = new System.Collections.Hashtable();
                        this.olderWorkflows = new WorkflowFilesCollection();

                        if (section != null)
                        {
                            foreach (WorkflowFileElement fileElement in section.WorkflowFiles)
                            {
                                this.AddNewWorkflow(fileElement);
                            }
                        }
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
                var defaultItem = this.defaultWorkflows[fileElement.Name] as WorkflowFileElement;
                if (defaultItem != null && defaultItem.Version > fileElement.Version)
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
            var fileElement = this.defaultWorkflows[name] as WorkflowFileElement;
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
                    if (_cachedElements.ContainsKey(fileElement))
                    {
                        rootElement = _cachedElements[fileElement];
                    }
                    else
                    {
                        // Create XML reader for this file
                        // Must be able to have more than one reader active at a time.
                        var doc = new XmlDocument();
                        var fileName = fileElement.FileName;
                        using (Stream stream = new FileStream(this.workflowFolder + fileName, FileMode.OpenOrCreate))
                        {
                            doc.Load(stream);
                        }
                        rootElement = doc.DocumentElement;
                        if (rootElement != null && !(rootElement.LocalName == "process"))
                        {
                            Debug.Fail("LoadNewWorkflow: Could not find process in " + fileElement.FileName);
                            rootElement = null;
                        }
                        else
                        {
                            _cachedElements.Add(fileElement, rootElement);
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
        /// <param name="workflowContext"> workflowcontextModel class</param>
        /// <param name="startElement">Integer value representing the starting element in the nodeList.</param>
        /// <param name="nodeList">XmlNodeList containing the BPEL data from the BPEL file</param>
        /// <param name="cancelEventHandlerName">cancelEventHandlerName</param>
        private void FillStepList(WorkflowContext workflowContext, List<WorkflowStep> workflowStepList, int startElement, XmlNodeList nodeList, ref string cancelEventHandlerName)
        {
            for (int i = startElement; i < nodeList.Count; i++)
            {
                var currentStep = nodeList[i];

                if (!string.IsNullOrEmpty(currentStep.Prefix))
                {
                    currentStep.Prefix = "bpel";
                }

                if (currentStep is XmlComment)
                {
                    continue;
                }

                if (currentStep.LocalName == "invoke")
                {
                    var workflowStep = new InvokeStep(currentStep.Attributes);
                    workflowStepList.Add(workflowStep);
                }
                else if (currentStep.LocalName == "sequence")
                {
                    var sequenceStep = new SequenceStep(currentStep.Attributes);
                    workflowStepList.Add(sequenceStep);
                    FillStepList(workflowContext, sequenceStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "scope")
                {
                    // can't get tool to produce invoke without having it enclosed in a scope.
                    // just ignore scope for now but get the internals as if at the same level.
                    FillStepList(workflowContext,workflowStepList, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "receive")
                {
                    var receiveStep = new ReceiveStep(currentStep.Attributes);
                    workflowStepList.Add(receiveStep);
                    string messageName = String.Empty;
                    string timesetTime = String.Empty;
                    if (currentStep.Attributes != null)
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
                else if (currentStep.LocalName == "switch")
                {
                    var switchStep = new SwitchStep(currentStep.Attributes);
                    workflowStepList.Add(switchStep);

                    // Fill case/otherwise steps inside of switch step
                    FillStepList(workflowContext,switchStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "case")
                {
                    var caseStep = new CaseStep(currentStep.Attributes, false);
                    workflowStepList.Add(caseStep);
                    FillStepList(workflowContext,caseStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "otherwise")
                {
                    var caseStep = new CaseStep(currentStep.Attributes, true);
                    workflowStepList.Add(caseStep);
                    FillStepList(workflowContext,caseStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "while")
                {
                    var whileStep = new WhileStep(currentStep.Attributes);
                    workflowStepList.Add(whileStep);
                    FillStepList(workflowContext,whileStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "pick")
                {
                    var pickStep = new PickStep(currentStep.Attributes);
                    workflowStepList.Add(pickStep);
                    FillStepList(workflowContext,pickStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "eventHandlers")
                {
                    //we will handle this section as same as handl scope
                    FillStepList(workflowContext,workflowStepList, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "onMessage")
                {
                    var messageStep = new OnMessageStep(currentStep.Attributes);
                    workflowStepList.Add(messageStep);

                    string messageName = String.Empty;
                    string timesetTime = String.Empty;
                    if (currentStep.Attributes != null)
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

                    FillStepList(workflowContext,messageStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "onEvent")
                {
                    var onEventStep = new OnEventStep(currentStep.Attributes);
                    workflowContext.MessageTimeoutEventHanlderDict.Add(onEventStep.EventKey, onEventStep.StepId);
                    workflowStepList.Add(onEventStep);
                    FillStepList(workflowContext,onEventStep.WorkflowSteps, 0, currentStep.ChildNodes, ref cancelEventHandlerName);
                }
                else if (currentStep.LocalName == "partnerLinks")
                {
                    FillPartnerLinkList(workflowContext, currentStep);
                }
                else if (currentStep.LocalName == "variables")
                {
                    FillVariableList(workflowContext, currentStep);
                }
                else if (currentStep.LocalName == "faultHandlers")
                {
                    foreach (XmlNode childNode in currentStep.ChildNodes)
                    {
                        var faultHandler = new FaultHandler(childNode);
                        workflowStepList.Add(faultHandler);
                        FillStepList(workflowContext,faultHandler.WorkflowSteps, 0, childNode.ChildNodes, ref cancelEventHandlerName);
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
        /// <param name="workflowContext">Dictionary of workflow variables to populate with any
        /// variables defined in the BPEL file.</param>
        /// <param name="currentStep">XmlNode containing the BPEL data from the BPEL file</param>
        private void FillVariableList(WorkflowContext workflowContext, XmlNode currentStep)
        {
            if (currentStep.LocalName == "variables")
            {
                foreach (XmlNode variableNode in currentStep.ChildNodes)
                {
                    if (variableNode.LocalName == "variable")
                    {
                        string variableName = null;
                        string variableType = null;
                        if (variableNode.Attributes != null)
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
                            workflowContext.WorkflowVariables.Add(variableName, variableType);
                        }
                    }
                }
            }
        }

        private void FillPartnerLinkList(WorkflowContext workflowContext, XmlNode currentStep)
        {
            if (currentStep.LocalName == "partnerLinks")
            {
                foreach (XmlNode variableNode in currentStep.ChildNodes)
                {
                    if (variableNode.LocalName == "partnerLink")
                    {
                        var partnerLinkEntity = new PartnerLinkModel();
                        if (variableNode.Attributes != null)
                            foreach (XmlAttribute variableAttribute in variableNode.Attributes)
                            {
                                if (variableAttribute.LocalName == "name")
                                {
                                  partnerLinkEntity.Name = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "partnerLinkType")
                                {
                                  partnerLinkEntity.PartnerLinkType = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "myRole")
                                {
                                    partnerLinkEntity.MyRole = variableAttribute.Value;
                                }
                                else if (variableAttribute.LocalName == "partnerRole")
                                {
                                    partnerLinkEntity.PartnerRole = variableAttribute.Value;
                                }
                            }
                        if (!string.IsNullOrEmpty(partnerLinkEntity.Name))
                        {
                            workflowContext.PartnerLinkList.Add(partnerLinkEntity);
                        }
                    }
                }
            }


        }
    }
}
