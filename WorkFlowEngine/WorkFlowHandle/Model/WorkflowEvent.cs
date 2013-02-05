/********************************************************************************
** Class Name:   WorkflowEvent 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowEvent class
*********************************************************************************/

using System;
using Sy
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;

namespace WorkFlowHandle.Model
{
    [Serializable]
    public class WorkflowEvent
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        private string _eventId;

        /// <summary>
        /// Data for the event.  For a message, this is the
        /// actual message.  
        /// </summary>
        private object _eventData;

        /// <summary>
        /// Identifies the type of event.
        /// </summary>
        private EventType _eventType;

        /// <summary>
        /// A string that identifies the event.  This is used by
        /// a workflow to find a specific message.
        /// </summary>
        private string _messageName;

        /// <summary>
        /// The workflow Id of the workflow the event is associated with.
        /// </summary>
        private string _workflowId;

        /// <summary>
        /// Gets or sets the key for BLOB storage.
        /// </summary>
        private string _keyForBlobStorage;

        /// <summary>
        /// Initializes a new instance of the WorkflowEvent class
        /// </summary>
        public WorkflowEvent()
        {
            _eventId = Guid.NewGuid().ToString("n");
        }

        /// <summary>
        /// Gets or sets a unique identifier for an event
        /// </summary>
        public virtual string Id
        {
            get { return _eventId; }
            set { _eventId = value; }
        }

        /// <summary>
        /// Gets or sets the type of workflow event
        /// </summary>
        public virtual EventType EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }

        /// <summary>
        /// Gets or sets data understood by the event creator and the intended workflow
        /// </summary>
        public virtual object EventData
        {
            get { return _eventData; }
            set { _eventData = value; }
        }

        /// <summary>
        /// Gets or sets the message name.  This string is used to identify a 
        /// particular message event
        /// </summary>
        public virtual string MessageName
        {
            get { return _messageName; }
            set { _messageName = value; }
        }

        /// <summary>
        /// Gets or sets the unique workflow Identifier of the workflow this event is associated with
        /// </summary>
        public virtual string WorkflowId
        {
            get { return _workflowId; }
            set { _workflowId = value; }
        }

        /// <summary>
        /// Gets or sets the key for BLOB storage.
        /// </summary>
        /// <value>The key for BLOB storage.</value>
        public string KeyForBlobStorage
        {
            get { return _keyForBlobStorage; }
            set { _keyForBlobStorage = value; }
        }
    }
}
