/********************************************************************************
** Class Name:   MailMessageModel
** Author：      Spring Yang
** Create date： 2013-4-14
** Modify：      Spring Yang
** Modify Date： 2013-4-14
** Summary：     MailMessageModel class
*********************************************************************************/

namespace CommonLibrary.Model
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public class MailMessageModel
    {
        public List<string> ToMailList { get; set; }

        public string FromMail { get; set; }

        public string FromMailPwd { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public object Token { get; set; }

        public List<string> CCMailList { get; set; }

        public List<Attachment> AttachmentList { get; set; }

        public Action<string> AsyncAction { get; set; }

    }
}
