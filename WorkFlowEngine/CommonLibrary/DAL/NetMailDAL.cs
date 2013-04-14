/********************************************************************************
** Class Name:   NetMailDAL
** Author：      Spring Yang
** Create date： 2013-4-14
** Modify：      Spring Yang
** Modify Date： 2013-4-14
** Summary：     NetMailDAL class
*********************************************************************************/

namespace CommonLibrary.DAL
{
    using Help;
    using Model;
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    public class NetMailDAL
    {
        #region Private Variable

        #endregion

        #region Private Static Property

        private static Action<string> AsyncAciton { get; set; }

        #endregion

        #region Construct Method

        #endregion

        #region Public Property

        #endregion

        #region Private Method

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            if (AsyncAciton == null) return;
            var token = (string)e.UserState;
            if (e.Cancelled)
            {
                AsyncAciton(string.Format(CommonConstants.MailCancelTags, token));
            }
            else if (e.Error != null)
            {
                AsyncAciton(string.Format(CommonConstants.MailErrorTags, token, e.Error));
            }
            else
            {
                AsyncAciton(CommonConstants.MailSuccessTags);
            }
        }

        private static SmtpClient InitSmtpMail(MailMessageModel entity, out MailMessage mail)
        {
            mail = new MailMessage();
            try
            {
                if (entity.ToMailList.Count > 0)
                {
                    var client = new SmtpClient();
                    foreach (string address in entity.ToMailList)
                    {
                        mail.To.Add(new MailAddress(address));
                    }
                    mail.From = new MailAddress(entity.FromMail);
                    mail.Subject = entity.Subject;
                    mail.Body = entity.Body;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(entity.FromMail, entity.FromMailPwd);
                    client.Host = entity.Host;
                    client.Port = entity.Port;
                    client.EnableSsl = true;
                    return client;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelp.Instance.Write(ex.Message, MessageType.Error, typeof(NetMailDAL), MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        #endregion

        #region Public Static Mehtod

        public static bool SendMail(MailMessageModel entity)
        {
            try
            {
                var result = false;
                if (entity == null || entity.ToMailList == null) return false;
                MailMessage mail;
                var client = InitSmtpMail(entity, out mail);
                if (client != null)
                {
                    client.Send(mail);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelp.Instance.Write(ex.Message, MessageType.Error, typeof(NetMailDAL), MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public static bool SendAsync(MailMessageModel entity)
        {
            var result = false;
            if (entity == null || entity.ToMailList == null) return false;
            MailMessage mail;
            var client = InitSmtpMail(entity, out mail);

            if (client != null)
            {
                AsyncAciton = entity.AsyncAction;
                client.SendCompleted += SendCompletedCallback;
                client.SendAsync(mail, null);
                result = true;
            }
            return result;
        }

        #endregion

        #region Public Event

        #endregion
    }
}
