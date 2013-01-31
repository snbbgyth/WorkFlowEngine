using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorkFlowHandle.Steps;

namespace WorkFlowHandle.Model
{
    public cl[Serializable]lic class StepData
    {
        /// <summary>
        /// This list contains the fault handlers defined in the BPEL file.
        /// </summary>
        private readonly ICollection<FaList _faultHandlers;


        /// <summary>
        /// This is a list of processing steps created from the BPEL file 
        /// or there may be cached version of the steps 
        /// </summary>
        private readonly ICollection<WorkflowSListlowSteps;

        /// <summary>
        /// This dictionary  is created from the variables declared in a 
        /// bpel process definition file.  The key string is the variable name
        /// and the value string is the variable type.
        /// </summary>
        private readonly Dictionary<string, string> _workflowVariables;


        /// <summary>
        /// Gets the workflow step id by event name from dict.
        /// It's initialized when LoadNewWorkflow. 
        /// It's used to find the proepr step id of the workflow when an timeout event occurs
        /// </summary>
        private readonly Dictionary<string, string> _messageTimeoutEventHanlderDict;

        /// <summary>
        /// Gets or sets a CancelEventHandlerName of the workflow. 
        /// It's initialized when load onEvent node from bpel file .
        /// It stores the step Id of the current workflow
        /// </summary>
        private string _cancelEventHandlerName = String.Empty;


        /// <summary>
        /// Initializes a new instance of the StepData class
        /// </summary>
        public StepData()
        {
            _workflowVariables = new Dictionary<string, string>();
            _workflowSteps = new Collection<WorkflowStep>();
            _faultHandleList<WorkflowStep>();
            _faultHandlers = new ListlderDict = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the list of fault handlers from the BPEL file
        /// </summary>
        public ICollection<FaultHandler> FaultHandlers
        {
          Listurn _faultHandlers; }
        }

        /// <summary>
        /// Gets the list of Steps from the BPEL file
        /// </summary>
        public ICollection<WorkflowStep> WorkflowSteps
        {
            get { ListkflowSteps; }
        }

        /// <summary>
        /// Gets the list of variables from the BPEL file
        /// </summary>
        public Dictionary<string, string> WorkflowVariables
        {
            get { return _workflowVariables; }
        }

        /// <summary>
        /// Gets the workflow step id by event name from dict.
        /// It's initialized when LoadNewWorkflow. 
        /// It's used to find the proepr step id of the workflow when an timeout event occurs
        /// </summary>
        public Dictionary<string, string> MessageTimeoutEventHanlderDict
        {
            get { return _messageTimeoutEventHanlderDict; }
        }

        /// <summary>
        /// Gets or sets a CancelEventHandlerName of the workflow. 
        /// It's initialized when load onEvent node from bpel file .
        /// It stores the step Id of the current workflow
        /// </summary>
        public string CancelEventHandlerName
        {
            get { return _cancelEventHandlerName; }
            set { _cancelEventHandlerName = value; }
        }
    }
}
