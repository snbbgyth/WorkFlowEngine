/********************************************************************************
** Class Name:   WorkflowContext 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowContext class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLibrary.Help;
using WorkFlowHandle.BLL;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.Model
{
     [Serializable]
    public class WorkflowContext : IDisposable
    {
        #region Class Fields
 
        /// <summary>
        /// a flag to identify wheter the whole workflow should wait when it handle cancel event
        /// </summary>
        [NonSerialized]
        private AutoResetEvent _cancelWaitEvent;

        /// <summary>
        /// the key of the fired event
        /// </summary>
        private string _firedEventKey;

        /// <summary>
        /// A list of workflows that this workflow has started.  This list
        /// is updated to remove a workflow when it completes.
        /// </summary>
        private IList<string> _workflowWaitlist;

        /// <summary>
        /// DateTime of the last state change.
        /// </summary>
        private DateTime _lastStateChange;

        /// <summary>
        /// Unique identifier string for the workflow described 
        /// by this WorkflowContext.
        /// </summary>
        private string _workflowId;

        /// <summary>
        /// Name of the workflow described by this WorkflowContext.
        /// </summary>
        private string _name;

        /// <summary>
        /// Locutus User Id string for the user associated with this
        /// workflow.  
        /// </summary>
        private string _userId;

        /// <summary>
        /// the collection of timeout message
        /// </summary>
        private ICollection<string> _timeoutMessages;

        /// <summary>
        /// Gets the processed messages.
        /// </summary>
        private ICollection<string> _processedMessages;

        /// <summary>
        /// Gets or sets the workflow status indicating the workflow is addressed canceled and wait for handling the event 
        /// It's set true when this workflow is addressed cancled,but the proper operation to handle cancel event has not been done.
        /// </summary>
        private bool _waitForCancel;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is canceled.
        /// </summary>
        private bool _isCanceled;

        /// <summary>
        /// Gets or sets a value indicating whether [handle cancel event].
        /// </summary>
        private bool _isHandleCancelEvent;

        /// <summary>
        /// Gets or sets a value indicating whether [event handler unfound].
        /// </summary>
        private bool _isEventHandlerUnfound;

        /// <summary>
        /// Gets or sets a value indicating whether the stop execution flag     
        /// </summary>
        private bool _isStopExecution;

        /// <summary>
        /// Gets or sets a dictionary of message name and time out values of the workflow.
        /// It's initialized when workflow load the onMessage/receive node which defined in the bpel file.
        /// It stores the message name and time out values of the current workflow.
        /// The timeout value is defined as a property of the onMessage/receive node.
        /// </summary>
        private Dictionary<string, string> _workflowTimeout;

        /// <summary>
        /// Gets or sets a value indicating whether an exception has been thrown during workflow
        /// execution and the fault handling logic has been started.  
        /// </summary>
        private bool _isProcessingFault;

        /// <summary>
        /// Workflow Step Data - Data in this region can be
        /// recreated by the workflow manager when reloading a 
        /// workflow
        /// This field is not persisted.
        /// </summary>
        [NonSerialized]
        private StepData _stepData;

        // Workflow Working Data - Data in this region is only
        // used when processing a workflow and does not need to
        // be saved or restored
        #region Workflow Working Data

        /// <summary>
        /// used to make sure only one completion notification is sent.
        /// </summary>
        private object _lockObject;

        /// <summary>
        /// Indicates that a completion notification has been sent.  
        /// This flag is used to ensure that 2 notifications
        /// are never sent.
        /// </summary>
        private bool _hasSentCompletionNotification;

        /// <summary>
        /// A lock object used to lock access to the event cache
        /// </summary>
        private readonly object _eventLock = new object();

        /// <summary>
        /// Identifies the manager that is starting this workflow.  This string is used
        /// to find a manager to use for message communications.  Workflows starting workflows should
        /// specify the name of the manager that started them.  If non-mananger assemblies are starting i
        /// workflows, the manager name of the manager they are running for must be used.
        /// </summary>
        private string _managerName;

        /// <summary>
        /// Gets or sets the Id of the current workflow step
        /// </summary>
        private string _currentStepId = String.Empty;

        /// <summary>
        /// Gets or sets the version of the BPEL workflow
        /// </summary>
        private string _version = String.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the workflow steps have been loaded into 
        /// this workflow context
        /// </summary>
        private bool _isWorkflowStepsLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether that this workflow is allowed to run
        /// when a user is disabled
        /// </summary>
        private bool _isCanRunForDisabledUser;
 
        /// <summary>
        /// Gets or sets id of workflow transaction
        /// </summary>
        private string _applicationTransactionId = String.Empty;

        /// <summary>
        /// Gets or sets start position of progress for this workflow
        /// </summary>
        private double _progressStartPosition;

        /// <summary>
        /// Gets or sets range of progress of this workflow.
        /// </summary>
        private double _progressRange;

        /// <summary>
        /// Get or Set the AntiWorkflow Flog
        /// </summary>
        private bool _isAntiWorkFlow;
        /// <summary>
        /// Instance of workflowHandler.  This instance is loaded
        /// the first time it is requested and used from then on.
        /// This field is not persisted.
        /// </summary>
        [NonSerialized]
        private WorkflowHandle _workflowHandler;
 
        #endregion
        #endregion Class Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the WorkflowContext class
        /// This is a no argument constructor required by NHibernate.  This constructor
        /// gets called when NHibernate is creating a Workflow Context from
        /// saved data.  NHibernate will only fill in the _persistantData.
        /// </summary>
        public WorkflowContext()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the WorkflowContext class
        /// </summary>
        /// <param name="name">string describing this workflow</param>
        /// <param name="workflowId">unique identifier</param>
        /// <param name="userId">user id for this workflow</param>
        /// <param name="managerName">String identifing the manager that is starting this workflow.  This string is used
        /// to find a manager to use for message communications.  Workflows starting workflows should
        /// specify the name of the manager that started them.  If non-mananger assemblies are 
        /// starting workflows, the manager name of
        /// the manager they are running for must be used.</param>
        /// <param name="completionNotification">contains data on who to notify when the
        /// workflow completes (including on error or on cancellation)</param>
        public WorkflowContext(string name,
            string workflowId,
            string userId,
            string managerName )
        {
            _name = name;
            _workflowId = workflowId;
            _userId = userId;
            _managerName = managerName;
            Initialize();
        }
        #endregion

        #region Properties
        /// <summary>
        /// This field is primarily for testing, so a workflow can hold to an anti-workflow it started
        ///  
        /// </summary>
        internal string AntiWorkflowId
        {
            get { return (string)GetWorkflowParameter("antiWorkflowId"); }
            set { SetWorkflowParameter("antiWorkflowId", value); }
        }

        /// <summary>
        /// Gets or sets id of workflow transaction
        /// </summary>
        public string ApplicationTransactionId
        {
            get { return _applicationTransactionId; }
            set { _applicationTransactionId = value; }
        }

        /// <summary>
        /// Gets or sets a CancelEventHandlerName of the workflow. 
        /// It's initialized when load onEvent node from bpel file .
        /// It stores the step Id of the current workflow
        /// </summary>
        public virtual string CancelEventHandlerName
        {
            get { return _stepData.CancelEventHandlerName; }
            set { _stepData.CancelEventHandlerName = value; }
        }

        /// <summary>
        /// Gets the cancel wait event.
        /// </summary>
        /// <value>The cancel wait event.</value>
        public virtual AutoResetEvent CancelWaitEvent
        {
            get { return _cancelWaitEvent; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether that this workflow is allowed to run
        /// when a user is disabled
        /// </summary>
        public virtual bool CanRunForDisabledUser
        {
            get { return _isCanRunForDisabledUser; }
            set { _isCanRunForDisabledUser = value; }
        }

        /// <summary>
        /// Gets or sets the Id of the current workflow step
        /// </summary>
        public virtual string CurrentStepId
        {
            get { return _currentStepId; }

            set { _currentStepId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [event handler unfound].
        /// </summary>
        /// <value><c>true</c> if [event handler unfound]; otherwise, <c>false</c>.</value>
        public virtual bool EventHandlerUnfound
        {
            get { return _isEventHandlerUnfound; }
            set { _isEventHandlerUnfound = value; }
        }

        /// <summary>
        /// Gets the FaultHandlers from BPEL file
        /// </summary>
        public virtual List<FaultHandler> FaultHandlers
        {
            get
            {
                if (!IsWorkflowStepsLoaded || _stepData == null)
                {
                    LoadExistingWorkflow();
                }
                if (_stepData != null)
                {
                    return _stepData.FaultHandlers;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the fired event key.
        /// </summary>
        /// <value>The fired event key.</value>
        public virtual string FiredEventKey
        {
            get { return _firedEventKey; }
            set { _firedEventKey = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [handle cancel event].
        /// </summary>
        /// <value><c>true</c> if [handle cancel event]; otherwise, <c>false</c>.</value>
        public virtual bool HandleCancelEvent
        {
            get { return _isHandleCancelEvent; }
            set { _isHandleCancelEvent = value; }
        }

        /// <summary>
        /// Gets an instance of the WorkflowHandler.
        /// </summary>
        public WorkflowHandle Handler
        {
            get
            {
                if (_workflowHandler == null)
                {
                    // temp lock - need real on eif this is the fix.
                    lock (_name)
                    {
                        
                    }
                }
                return _workflowHandler;
            }
        }

        /// <summary>
        /// Gets or sets a unique workflow identifier
        /// </summary>
        public virtual string Id
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }

        /// <summary>
        /// Get or Set the AntiWorkflow Flog
        /// </summary>
        public bool IsAntiWorkFlow
        {
            get { return _isAntiWorkFlow; }
            set { _isAntiWorkFlow = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is canceled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is canceled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsCanceled
        {
            get { return _isCanceled; }
            set { _isCanceled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the workflow steps have been loaded into 
        /// this workflow context
        /// </summary>
        public virtual bool IsWorkflowStepsLoaded
        {
            get { return _isWorkflowStepsLoaded; }
            set { _isWorkflowStepsLoaded = value; }
        }

        /// <summary>
        /// Gets or sets the DateTime of the last change of CurrentState
        /// </summary>
        public virtual DateTime LastStateChange
        {
            get { return _lastStateChange; }
            set { _lastStateChange = value; }
        }
 
        /// <summary>
        /// Gets or sets a string identifing the manager that is starting this workflow.  This string is used
        /// to find a manager to use for message communications.  Workflows starting workflows should
        /// specify the name of the manager that started them.  If non-mananger assemblies are starting 
        /// workflows, the manager name of the manager they are running for must be used.
        /// </summary>
        public virtual string ManagerName
        {
            get { return _managerName; }
            set { _managerName = value; }
        }
 
        /// <summary>
        /// Gets the workflow step id by event name from dict.
        /// It's initialized when LoadNewWorkflow. 
        /// It's used to find the proepr step id of the workflow when an timeout event occurs
        /// </summary>
        public virtual Dictionary<string, string> MessageTimeoutEventHanlderDict
        {
            get
            {
                if (!IsWorkflowStepsLoaded)
                {
                    LoadExistingWorkflow();
                }
                return _stepData.MessageTimeoutEventHanlderDict;
            }
        }
 
        /// <summary>
        /// Gets or sets the name of the BPEL workflow
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an exception has been thrown during workflow
        /// execution and the fault handling logic has been started.  
        /// </summary>
        public virtual bool ProcessingFault
        {
            get { return _isProcessingFault; }
            set { _isProcessingFault = value; }
        }

        /// <summary>
        /// Gets the processed messages.
        /// </summary>
        /// <value>The processed messages.</value>
        public virtual ICollection<string> ProcessedMessages
        {
            get { return _processedMessages; }
        }

        /// <summary>
        /// Gets or sets start position of progress for this workflow
        /// </summary>
        public double ProgressStartPosition
        {
            get { return _progressStartPosition; }
            set { _progressStartPosition = value; }
        }

        /// <summary>
        /// Gets or sets range of progress of this workflow.
        /// </summary>
        public double ProgressRange
        {
            get { return _progressRange; }
            set { _progressRange = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the stop execution flag     
        /// </summary>
        public virtual bool StopExecution
        {
            get { return _isStopExecution; }
            set { _isStopExecution = value; }
        }

        /// <summary>
        /// Gets the timeout messages.
        /// </summary>
        /// <value>The timeout messages.</value>
        public virtual ICollection<string> TimeoutMessages
        {
            get { return _timeoutMessages; }
        }

        /// <summary>
        /// Gets or sets the unique User Id for the workflow 
        /// </summary>
        public virtual string UserId
        {
            get { return _userId; }
            set { _userId = value.Trim(); }
        }

        /// <summary>
        /// Gets or sets the version of the BPEL workflow
        /// </summary>
        public virtual string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Gets or sets the workflow status indicating the workflow is addressed canceled and wait for handling the event 
        /// It's set true when this workflow is addressed cancled,but the proper operation to handle cancel event has not been done.
        /// </summary>
        public virtual bool WaitForCancel
        {
            get { return _waitForCancel; }
            set { _waitForCancel = value; }
        }

        /// <summary>
        /// Gets or sets a dictionary of message name and time out values of the workflow.
        /// It's initialized when workflow load the onMessage/receive node which defined in the bpel file.
        /// It stores the message name and time out values of the current workflow.
        /// The timeout value is defined as a property of the onMessage/receive node.
        /// </summary>
        public virtual Dictionary<string, string> WorkflowTimeout
        {
            get { return _workflowTimeout; }
            set { _workflowTimeout = value; }
        }

        /// <summary>
        /// Gets the WorkflowSteps from the BPEL file
        /// </summary>
        public virtual List<WorkflowStep> WorkflowSteps
        {
            get
            {
                if (!IsWorkflowStepsLoaded)
                {
                    LoadExistingWorkflow();
                }
                return _stepData.WorkflowSteps;
            }
        }

        /// <summary>
        /// Gets the WorkflowVariables from BPEL file
        /// </summary>
        public virtual Dictionary<string, string> WorkflowVariables
        {
            get
            {
                if (!IsWorkflowStepsLoaded)
                {
                    LoadExistingWorkflow();
                }
                return _stepData.WorkflowVariables;
            }
        }

        /// <summary>
        /// Gets or sets a dictionary of workflows that have been started by this workflow
        /// and this workflow needs to wait for all to complete before it is completed.  This
        /// may be due to some action that needs
        /// to be taken when each one completes or may be due to wanting to only
        /// notify on completion when all actions have been completed.
        /// The key is the string ID of
        /// the workflow that has been started.  The value is the number of the Workflow Step 
        /// that needs to be executed once the workflow has been completed.  This
        /// WorkflowStep could be any step type.  For example a sequence step would
        /// result in a set of steps being performed.
        /// </summary>
        public virtual IList<string> WorkflowWaitlist
        {
            get { return _workflowWaitlist; }
            set { _workflowWaitlist = value; }
        }

        /// <summary>
        /// Utility function to print the workflow name and id
        /// for this context.
        /// </summary>
        /// <returns>A string containing the workflow name and Id.</returns>
        public string WorkflowLogId
        {
            get { return " Workflow " + Name + " : " + Id; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// called to persist the workflow information
        /// </summary>
        public virtual void Save()
        {
          // Handler.SaveContext(this);
        }

        /// <summary>
        /// Waits the handle cancel event.
        /// </summary>
        public virtual void WaitHandleCancelEvent()
        {
            if (_cancelWaitEvent == null)
            {
                _cancelWaitEvent = new AutoResetEvent(false);
            }
            _cancelWaitEvent.WaitOne();
        }

        /// <summary>
        /// Returns the value of a workflow variable.
        /// </summary>
        /// <param name="variableName">identifer for the variable</param>
        /// <returns>value of the workflow variable</returns>
        public virtual string GetWorkflowVariable(string variableName)
        {
            string variable;
            _stepData.WorkflowVariables.TryGetValue(variableName, out variable);
            return variable;
        }

        /// <summary>
        /// Returns the value of a workflow parameter
        /// </summary>
        /// <param name="parameterName">name of the desired parameter</param>
        /// <returns>value for the parameter</returns>
        public virtual object GetWorkflowParameter(string parameterName)
        {
            return null;
        }

        /// <summary>
        /// Sets the value of a workflow parameter
        /// </summary>
        /// <param name="parameterName">name of the desired parameter</param>
        /// <param name="parameterData">value of the parameter</param>
        public virtual void SetWorkflowParameter(string parameterName, object parameterData)
        {
 
        }

        /// <summary>
        /// Processes an event for this workflow
        /// </summary>
        /// <param name="userId">The locutus userId</param>
        /// <param name="workflowEvent"> workflow event to process</param>
        public virtual void OnEvent(string userId, WorkflowEvent workflowEvent)
        {
            if (workflowEvent == null)
            {
                return;
            }
            lock (_eventLock)
            {
                if (workflowEvent.EventType != EventType.Timer && workflowEvent.EventType != EventType.Cancel)
                {
                    AddEvent(userId, workflowEvent);
                }
 
            }
        }
 

        /// <summary>
        /// Find event message with a specific ID.  Returns null if message not yet
        /// received.
        /// </summary>
        /// <param name="userId">The locutus userId</param>
        /// <param name="eventType">requested event type to get</param>
        /// <param name="messageName">requested messageName, only used if eventType is message</param>
        /// <returns>Requested workflow event or null, if none found</returns>
        public virtual WorkflowEvent GetEventById(string userId, EventType eventType, string messageName)
        {
            WorkflowEvent workflowEvent;
            lock (_eventLock)
            {
               // workflowEvent = Handler.WorkflowEventStore.GetEvent(userId, Id, eventType, messageName);
            }

            return new WorkflowEvent();
        }

        /// <summary>
        /// Determines if this workflow has completed execution.
        /// </summary>
        /// <returns>true if completed, false if not completed</returns>
        public virtual bool IsComplete()
        {
            if (WorkflowWaitlist.Count > 0)
            {
                if (!IsWorkflowAndChildenComplete(Id))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if all sub workflows of this workflow has completed execution.
        /// </summary>
        /// <param name="theWorkflowId">workflow Id</param>
        /// <returns>true if all sub workflows completed, false if not</returns>
        private bool IsWorkflowAndChildenComplete(string theWorkflowId)
        {
     

            return true;
        }

        /// <summary>
        /// <para>
        /// This function is called to do any notifications necessary when a workflow completes. 
        /// it will notify the registered component and remove the workflow context from the 
        /// context store.
        /// </para>
        /// <para>
        /// A workflow is completed when the status is complete and it is not waiting for any
        /// other workflows to complete.
        /// </para>
        /// <para>
        /// This method is where anti-workflows are triggered. They are triggered if the
        /// workflow is the main workflow and the <see cref="LastException"/> property is
        /// non-<c>null</c>.
        /// </para>
        /// </summary>
        public virtual void OnWorkflowCompleted()
        {
            if (!IsComplete())
            {
                return;
            }

            // Only send this notificaiton once.
            // Only notify if this workflow isn't waiting for other workflows to complete
            if (_hasSentCompletionNotification || (WorkflowWaitlist.Count > 0 && !IsWorkflowAndChildenComplete(Id)))
            {
                return;
            }

            lock (_lockObject)
            {
                if (!_hasSentCompletionNotification)
                {
                    _hasSentCompletionNotification = true;
                }
            }
        }

        /// <summary>
        /// Remove workflow and all its subworkflow from list
        /// </summary>
        /// <param name="context">the conext associated with the workflow.</param>
        private void RemoveWorkflowAndSubWorkflows(WorkflowContext context)
        {
            if (context == null)
                return;
        }

   
        /// <summary>
        /// Run anti workflow. This method depends on breadcrumbs having been stored
        /// during the workflow's execution. If there is no named 
        /// <see cref="WorkflowHandler.GetAntiWorkflowName"/>, this method does nothing.
        /// If the <see cref="IBreadCrumbCache"/> cannot be loaded, this method does nothing.
        /// </summary>
        /// <param name="context">workflow context</param>
        public void RunAntiWorkflow(WorkflowContext context)
        {
            if (context == null) return;
        }

         /// <summary>
         /// Remove the specified event from the event store
         /// </summary>
         /// <param name="locutusUserId">The locutus user Id</param>
         /// <param name="workflowEvent">event to remove</param>
         internal virtual void RemoveEvent(string locutusUserId, WorkflowEvent workflowEvent)
         {
         }


         /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_cancelWaitEvent != null)
            {
                _cancelWaitEvent.Close();
                _cancelWaitEvent = null;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Add an event to the event store
        /// </summary>
        /// <param name="userId">The locutus user Id</param>
        /// <param name="workflowEvent">event to add</param>
        private void AddEvent(string userId, WorkflowEvent workflowEvent)
        {
             
        }

        /// <summary>
        /// Initializes private fields during class initialization
        /// </summary>
        private void Initialize()
        {
            _stepData = new StepData();
          
            _workflowWaitlist = new List<string>();
        
            _lastStateChange = DateTime.Now;
            _hasSentCompletionNotification = false;
            _lockObject = new object();
            _timeoutMessages = new Collection<string>();
            _processedMessages = new Collection<string>();
            _cancelWaitEvent = new AutoResetEvent(false);
        }

        /// <summary>
        /// Loads an existing workflow BPEL file.  This is used to get
        /// the steps associated with a workflow that is partially completed.
        /// The exact version of the workflow must be loaded.
        /// </summary>
        private void LoadExistingWorkflow()
        {
            if (Handler != null)
            {
               
                IsWorkflowStepsLoaded = true;
            }
        }

        #endregion
    }
}
