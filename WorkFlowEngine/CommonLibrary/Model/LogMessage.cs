/********************************************************************************
** Class Name:   LogMessage 
** Author：      Spring Yang
** Create date： 2013-3-14
** Modify：      Spring Yang
** Modify Date： 2013-3-14
** Summary：     LogMessage class
*********************************************************************************/

namespace CommonLibrary.Model
{
    using System;
    using System.Globalization;
    using Help;

    /// <summary>
    /// Log Message Class
    /// </summary>
    public class LogMessage : EventArgs
    {

        /// <summary>
        /// Create Log message instance
        /// </summary>
        public LogMessage()
            : this("", MessageType.Unknown, null, null)
        {
        }

        /// <summary>
        /// Crete log message by message content and message type
        /// </summary>
        /// <param name="text">message content</param>
        /// <param name="messageType">message type</param>
        /// <param name="classType">class Type </param>
        /// <param name="methodName">method Name </param>
        public LogMessage(string text, MessageType messageType, Type classType, string methodName)
            : this(DateTime.Now, text, messageType, classType, methodName)
        {
        }

        /// <summary>
        /// Create log message by datetime and message content and message type
        /// </summary>
        /// <param name="dateTime">date time </param>
        /// <param name="text">message content</param>
        /// <param name="messageType">message type</param>
        /// <param name="classType">class Type </param>
        /// <param name="methodName"> method Name</param>
        public LogMessage(DateTime dateTime, string text, MessageType messageType, Type classType, string methodName)
        {
            Datetime = dateTime;
            MessageType = messageType;
            Text = text;
            ClassType = classType;
            MethodName = methodName;
        }

        /// <summary>
        /// Gets or sets method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets datetime
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// Gets or sets message content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets message type
        /// </summary>
        public MessageType MessageType { get; set; }


        public Type ClassType { get; set; }

        /// <summary>
        /// Get Message to string
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            return Datetime.ToString(CultureInfo.InvariantCulture) + "\t" + Text + "\n";
        }
    }
}
