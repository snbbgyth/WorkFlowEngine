/********************************************************************************
** Class Name:   WorkflowFilesCollection 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     WorkflowFilesCollection class
*********************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WorkFlowHandle.Model
{
    [ConfigurationCollection(typeof(WorkflowFileElement))]
    public class WorkflowFilesCollection : ConfigurationElementCollection, ICollection<WorkflowFileElement>
    {/// <summary>
        /// The <see cref="WorkflowFileElement"/> at a specified index location.
        /// </summary>
        /// <param name="index">The index of the <see cref="WorkflowFileElement"/></param>
        /// <returns>The <see cref="WorkflowFileElement"/> at the specified index; otherwise, a null reference (Nothing in Visual Basic).
        /// </returns>
        [ConfigurationProperty("bpelFile")]
        public WorkflowFileElement this[int index]
        {
            get
            {
                return (WorkflowFileElement)BaseGet(index);
            }
        }

        /// <summary>
        /// Returns the <see cref="WorkflowFileElement"/> with the specified key. 
        /// </summary>
        /// <param name="key">The key of the element to return.</param>
        /// <returns>The <see cref="WorkflowFileElement"/> with the specified key; otherwise, a null reference (Nothing in Visual Basic).
        /// </returns>
        [ConfigurationProperty("bpelFile")]
        public new WorkflowFileElement this[string key]
        {
            get
            {
                return (WorkflowFileElement)BaseGet(key);
            }
        }

        /// <summary>
        /// Adds a new element to the WorkflowFilesCollection.  When a new workflow file
        /// is added via this method, it is not a persistent add.  The next time the
        /// workflow handler is restarted, the list in the configuration file will
        /// be the only elements populated in the WorkflowFilesCollection.
        /// </summary>
        /// <param name="workflowFileElement">Class containing the definition of a new workflow file.</param>
        public void Add(WorkflowFileElement workflowFileElement)
        {
            this.BaseAdd(workflowFileElement);
        }

        /// <summary>
        /// Adds a new element to the WorkflowFilesCollection.  When a new workflow file
        /// is added via this method, it is not a persistent add.  The next time the
        /// workflow handler is restarted, the list in the configuration file will
        /// be the only elements populated in the WorkflowFilesCollection.
        /// </summary>
        /// <param name="name">string containing the name of the workflow. </param>
        /// <param name="version">float value indicating the version of the workflow </param>
        /// <param name="fileName">filename of the BPEL workflow description file</param>
        /// <param name="encryptedFileName">encrypted file name of the BPEL workflow description file</param>
        public void AddNewElement(string name, float version, string fileName, string encryptedFileName)
        {
            WorkflowFileElement fileElement = new WorkflowFileElement(name, version, fileName, encryptedFileName);
            this.BaseAdd(fileElement);
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationElement"/>.
        /// </summary>
        /// <returns>A new ConfigurationElement.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new WorkflowFileElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="ConfigurationElement"/> to return the key for. </param>
        /// <returns>An Object that acts as the key for the specified <see cref="ConfigurationElement"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WorkflowFileElement)element).FileName;
        }

        #region ICollection<WorkflowFileElement> Members


        /// <summary>
        /// Clear the collection
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Indicat if the collection contains an item
        /// </summary>
        /// <param name="item">an item</param>
        /// <returns>true if the item is in the collection</returns>
        public bool Contains(WorkflowFileElement item)
        {
            int index = BaseIndexOf(item);
            return (index != -1);
        }

        /// <summary>
        /// Copies the collection to an array
        /// </summary>
        /// <param name="array">an array of MediaTypeEntry objects</param>
        /// <param name="arrayIndex">the starting index for placing into the array</param>
        public void CopyTo(WorkflowFileElement[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Indicates if the collection is Read-only
        /// </summary>
        public new bool IsReadOnly
        {
            get { return base.IsReadOnly(); }
        }

        /// <summary>
        /// Remove an item from the collection
        /// </summary>
        /// <param name="item">the item</param>
        /// <returns>true if the item was removed</returns>
        public bool Remove(WorkflowFileElement item)
        {
            int index = BaseIndexOf(item);
            if (index == -1)
            {
                return false;
            }
            else
            {
                BaseRemoveAt(index);
                return true;
            }
        }

        #endregion

        #region IEnumerable<WorkflowFileElement> Members

        /// <summary>
        /// Get an enumerator for the collection
        /// </summary>
        /// <returns>an enumerator</returns>
        public new IEnumerator<WorkflowFileElement> GetEnumerator()
        {
            IEnumerator enumerator = base.GetEnumerator();
            return new WorkflowEnumerator(enumerator);
        }

        #endregion

        /// <summary>
        /// Defines methods to manipulate generic collections of <see cref="WorkflowFileElement"/>
        /// </summary>
        private class WorkflowEnumerator : IEnumerator<WorkflowFileElement>
        {
            /// <summary>
            /// IEnumerator
            /// </summary>
            private IEnumerator _enumerator;

            /// <summary>
            /// Initializes a new instance of the WorkflowEnumerator class.
            /// </summary>
            /// <param name="enumerator">The enumerator of <see cref="System.Collections.IEnumerator"/> type.</param>
            public WorkflowEnumerator(IEnumerator enumerator)
            {
                this._enumerator = enumerator;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value></value>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            public WorkflowFileElement Current
            {
                get { return this._enumerator.Current as WorkflowFileElement; }
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                // System.Collections.IEnumerator is not disposable 
                // FxCop wants this  here
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value></value>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            object IEnumerator.Current
            {
                get { return this._enumerator.Current; }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            public bool MoveNext()
            {
                return this._enumerator.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            public void Reset()
            {
                this._enumerator.Reset();
            }
        }
    }
}
