/********************************************************************************
** Class Name:   LogHelp 
** Author：      Spring Yang
** Create date： 2013-3-14
** Modify：      Spring Yang
** Modify Date： 2013-3-14
** Summary：     LogHelp class
*********************************************************************************/

namespace CommonLibrary.Help
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using IDAL;
    using Model;

    /// <summary>
    /// Author: spring yang
    /// Create time:2012/9/18 
    /// Log Help class
    /// </summary>
    /// <remarks>High performance log class</remarks>
    public class LogHelp : IDisposable
    {
        #region Private Variable

        //Log Message queue
        private static Queue<LogMessage> _logMessages;

        //log save directory
        private static string _logDirectory;

        //log write file state
        private static bool _state;

        //log type
        private static LogType _logType;

        //log life time sign
        private static DateTime _timeSign;

        //log file stream writer
        private static StreamWriter _writer;

        /// <summary>
        /// Wait enqueue wirte log message semaphore will release
        /// </summary>
        private Semaphore _semaphore;

        /// <summary>
        /// Single instance
        /// </summary>
        private static LogHelp _log;

        private object _lockObject;

        #endregion

        #region Private Property

        #endregion

        #region Construct Method

        /// <summary>
        /// Create a log instance
        /// </summary>
        private LogHelp()
        {
            Initialize();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Log save name type,default is daily
        /// </summary>
        public LogType LogType
        {
            get { return _logType; }
            set { _logType = value; }
        }


        /// <summary>
        /// Gets a single instance
        /// </summary>
        public static LogHelp Instance
        {
            get
            {
                if (_log == null) _log = new LogHelp();
                return _log;
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Initialize Log instance
        /// </summary>
        private void Initialize()
        {
            _state = true;
            string logPath = System.Configuration.ConfigurationManager.AppSettings["LogDirectory"];
            _logDirectory = string.IsNullOrEmpty(logPath) ? CommonUnitlHelp.LogPathTags : logPath;
            CheckLogDirectory();
            _logType = LogType.Daily;
            _lockObject = new object();
            _semaphore = new Semaphore(0, int.MaxValue, CommonConstants.LogSemaphoreNameTags);
            _logMessages = new Queue<LogMessage>();
            var thread = new Thread(Work) { IsBackground = true };
            thread.Start();
        }

        private void CheckLogDirectory()
        {
            if (!Directory.Exists(_logDirectory)) Directory.CreateDirectory(_logDirectory);
        }

        /// <summary>
        /// Write Log file  work method
        /// </summary>
        private void Work()
        {
            while (true)
            {
                if (_logMessages.Count > 0)
                {
                    FileWriteMessage();
                }
                else
                    if (WaitLogMessage()) break;
            }
        }

        /// <summary>
        /// Write message to log file
        /// </summary>
        private void FileWriteMessage()
        {
            LogMessage logMessage = null;
            lock (_lockObject)
            {
                if (_logMessages.Count > 0)
                    logMessage = _logMessages.Dequeue();
            }
            if (logMessage != null)
            {
                FileWrite(logMessage);
            }
        }


        /// <summary>
        /// The thread wait a log message
        /// </summary>
        /// <returns>is close or not</returns>
        private bool WaitLogMessage()
        {
            //determine log life time is true or false
            if (_state)
            {
                WaitHandle.WaitAny(new WaitHandle[] { _semaphore }, -1, false);
                return false;
            }
            FileClose();
            return true;
        }

        /// <summary>
        /// Gets file name by log type
        /// </summary>
        /// <returns>log file name</returns>
        private string GetFilename(MessageType type)
        {
            DateTime now = DateTime.Now;
            string format = "";
            switch (_logType)
            {
                case LogType.Daily:
                    _timeSign = new DateTime(now.Year, now.Month, now.Day);
                    _timeSign = _timeSign.AddDays(1);
                    format = CommonConstants.DailyLogFileNameTags;
                    break;
                case LogType.Weekly:
                    _timeSign = new DateTime(now.Year, now.Month, now.Day);
                    _timeSign = _timeSign.AddDays(7);
                    format = CommonConstants.WeeklyLogFileNameTags;
                    break;
                case LogType.Monthly:
                    _timeSign = new DateTime(now.Year, now.Month, 1);
                    _timeSign = _timeSign.AddMonths(1);
                    format = CommonConstants.MonthlyLogFileNameTags;
                    break;
                case LogType.Annually:
                    _timeSign = new DateTime(now.Year, 1, 1);
                    _timeSign = _timeSign.AddYears(1);
                    format = CommonConstants.AnnuallyLogFileNameTags;
                    break;
            }
            return string.Concat(type.ToString(), now.ToString(format));
        }

        /// <summary>
        /// Write log file message
        /// </summary>
        /// <param name="msg"></param>
        private void FileWrite(LogMessage msg)
        {
            try
            {
                if (_writer == null)
                {
                    FileOpen(msg.MessageType);
                }
                else
                {
                    //determine the log file is time sign
                    if (DateTime.Now >= _timeSign)
                    {
                        FileClose();
                        FileOpen(msg.MessageType);
                    }
                }
                if (_writer == null) return;
                _writer.WriteLine(CommonConstants.LogMessageTimeTags + msg.Datetime);
                if (msg.MessageType != MessageType.Runing)
                {
                    if (msg.ClassType != null)
                        _writer.WriteLine(CommonConstants.LogClassNameTags + msg.ClassType.FullName);
                    _writer.WriteLine(CommonConstants.LogMethodNameTags + msg.MethodName);
                    OnNotifyErrorMessage(msg);
                }
                _writer.WriteLine(CommonConstants.LogMessageTypeTags + msg.MessageType);
                _writer.WriteLine(CommonConstants.LogMessageContentTags + msg.Text);
                _writer.WriteLine();
                _writer.Flush();
                FileClose();

            }
            catch (Exception e)
            {
                Console.Out.Write(e);
            }
        }


        public event NotifyErrorMessageHandle NotifyErrorMessage;

        protected virtual void OnNotifyErrorMessage(LogMessage message)
        {
            NotifyErrorMessageHandle handler = NotifyErrorMessage;
            if (handler != null) handler(message);
        }

        /// <summary>
        /// Open log file write log message
        /// </summary>
        private void FileOpen(MessageType type)
        {
            CheckLogDirectory();
            _writer = new StreamWriter(Path.Combine(_logDirectory, GetFilename(type)), true, Encoding.UTF8);
        }

        /// <summary>
        /// Close log file 
        /// </summary>
        private void FileClose()
        {
            if (_writer != null)
            {
                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
        }

        #endregion

        #region Public Mehtod

        /// <summary>
        /// Enqueue a new log message and release a semaphore
        /// </summary>
        /// <param name="msg">Log message</param>
        public void Write(LogMessage msg)
        {
            if (msg != null)
            {
                lock (_lockObject)
                {
                    _logMessages.Enqueue(msg);
                    _semaphore.Release();
                }
            }
        }

        /// <summary>
        /// Write message by message content and type
        /// </summary>
        /// <param name="text">log message</param>
        /// <param name="messageType">message type</param>
        /// <param name="classType">class Type </param>
        /// <param name="methodName">method Name </param>
        public void Write(string text, MessageType messageType, Type classType, string methodName)
        {
            Write(new LogMessage(text, messageType, classType, methodName));
        }

        /// <summary>
        /// Write message
        /// </summary>
        /// <param name="text">message texte</param>
        /// <param name="messageType">messageType</param>
        public void Write(string text, MessageType messageType)
        {
            Write(new LogMessage(text, messageType, null, null));
        }

        /// <summary>
        /// Write Message by datetime and message content and type
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <param name="text">message content</param>
        /// <param name="messageType">message type</param>
        /// <param name="classType">class Type </param>
        /// <param name="methodName">method Name </param>
        public void Write(DateTime dateTime, string text, MessageType messageType, Type classType, string methodName)
        {
            Write(new LogMessage(dateTime, text, messageType, classType, methodName));
        }

        /// <summary>
        /// Write message ty exception and message type 
        /// </summary>
        /// <param name="e">exception</param>
        /// <param name="messageType">message type</param>
        /// <param name="classType">classType </param>
        /// <param name="methodName"> method Name</param>
        public void Write(Exception e, MessageType messageType, Type classType, string methodName)
        {
            Write(new LogMessage(e.Message, messageType, classType, methodName));
        }

        #region IDisposable member

        /// <summary>
        /// Dispose log
        /// </summary>
        public void Dispose()
        {
            _state = false;
        }

        #endregion

        #endregion

        #region Public Event

        #endregion

    }

    /// <summary>
    /// Log Type
    /// </summary>
    /// <remarks>Create log by daily or weekly or monthly or annually</remarks>
    public enum LogType
    {
        /// <summary>
        /// Create log by daily
        /// </summary>
        Daily,

        /// <summary>
        /// Create log by weekly
        /// </summary>
        Weekly,

        /// <summary>
        /// Create log by monthly
        /// </summary>
        Monthly,

        /// <summary>
        /// Create log by annually
        /// </summary>
        Annually
    }
}
