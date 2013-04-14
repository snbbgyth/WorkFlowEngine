/********************************************************************************
** Class Name:   MailHelp
** Author：      Spring Yang
** Create date： 2013-4-14
** Modify：      Spring Yang
** Modify Date： 2013-4-14
** Summary：     MailHelp class
*********************************************************************************/

namespace CommonLibrary.Help
{
    using DAL;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;
    using System.Threading;

    public class MailHelp : IDisposable
    {
        #region Private Variable

        //Log Message queue
        private static Queue<MailMessageModel> _mailMessages;

        //log write file state
        private static bool _state;

        //log life time sign
        private static DateTime _timeSign;

        /// <summary>
        /// Wait enqueue wirte log message semaphore will release
        /// </summary>
        private Semaphore _semaphore;

        /// <summary>
        /// Single instance
        /// </summary>
        private static MailHelp _instance;

        private object _lockObject;

        #endregion

        #region Private Property

        #endregion

        #region Construct Method

        /// <summary>
        /// Create a log instance
        /// </summary>
        private MailHelp()
        {
            Initialize();
        }

        #endregion

        #region Public Property



        /// <summary>
        /// Gets a single instance
        /// </summary>
        public static MailHelp Instance
        {
            get
            {
                if (_instance == null) _instance = new MailHelp();
                return _instance;
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
            _lockObject = new object();
            _semaphore = new Semaphore(0, int.MaxValue, CommonConstants.MailSemaphoreNameTags);
            _mailMessages = new Queue<MailMessageModel>();
            var thread = new Thread(Work) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// Write Log file  work method
        /// </summary>
        private void Work()
        {
            while (true)
            {
                if (_mailMessages.Count > 0)
                {
                    SendMailMessage();
                }
                else
                    if (WaitMailMessage()) break;
            }
        }

        /// <summary>
        /// Write message to log file
        /// </summary>
        private void SendMailMessage()
        {
            MailMessageModel mailMessage = null;
            lock (_lockObject)
            {
                if (_mailMessages.Count > 0)
                    mailMessage = _mailMessages.Dequeue();
            }
            if (mailMessage != null)
            {
                Send(mailMessage);
            }
        }

        /// <summary>
        /// The thread wait a log message
        /// </summary>
        /// <returns>is close or not</returns>
        private bool WaitMailMessage()
        {
            //determine log life time is true or false
            if (_state)
            {
                WaitHandle.WaitAny(new WaitHandle[] { _semaphore }, -1, false);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Write log file message
        /// </summary>
        /// <param name="msg"></param>
        private void Send(MailMessageModel msg)
        {
            try
            {
                NetMailDAL.SendMail(msg);
            }
            catch (Exception e)
            {
                LogHelp.Instance.Write(e, MessageType.Error, GetType(), MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region Public Mehtod

        public void SendMail(DateTime errorDateTime, string user, string className, string methodName, string messageContent, string stackContent)
        {
            MailMessageModel msg = new MailMessageModel()
            {
                FromMail = ConfigurationManager.AppSettings[CommonConstants.FromMail],
                FromMailPwd = ConfigurationManager.AppSettings[CommonConstants.FromMailPwd],
                ToMailList = new List<string>(ConfigurationManager.AppSettings[CommonConstants.ToMailList].Split(CommonConstants.CommaSplitTags.ToCharArray())),
                Host = ConfigurationManager.AppSettings[CommonConstants.MailHost],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings[CommonConstants.MailPort]),
                Subject = CommonConstants.MailSubjectTags,
                Body = string.Format(CommonConstants.EmailBodyTags, errorDateTime, user, className, methodName, messageContent, stackContent)
            };

            SendMail(msg);
        }

        /// <summary>
        /// Enqueue a new log message and release a semaphore
        /// </summary>
        /// <param name="msg">Log message</param>
        public void SendMail(MailMessageModel msg)
        {
            if (msg != null)
            {
                lock (_lockObject)
                {
                    _mailMessages.Enqueue(msg);
                    _semaphore.Release();
                }
            }
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
}
