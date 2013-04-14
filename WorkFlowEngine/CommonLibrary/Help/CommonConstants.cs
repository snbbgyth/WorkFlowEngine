using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Help
{
    public class CommonConstants
    {
        #region Common Tags

        public const string CommaSplitTags = ",";

        #endregion

        #region Log Help

        public const string LogSemaphoreNameTags = "LogSemaphore";

        public const string LogMessageTimeTags = "Log time is ";

        public const string LogMessageTypeTags = "Log type is ";

        public const string LogClassNameTags = "Class Name is ";

        public const string LogMethodNameTags = "Method Name is ";

        public const string LogMessageContentTags = "Log content is ";

        public const string DailyLogFileNameTags = "yyyyMMdd'.log'";

        public const string WeeklyLogFileNameTags = "yyyyMMdd'.log'";

        public const string MonthlyLogFileNameTags = "yyyyMM'.log'";

        public const string AnnuallyLogFileNameTags = "yyyy'.log'";

        public const string LogFolderNameTags = "Log";

        public const string LogEseqIDFileNameTags = "LogEseqID.xml";

        #endregion

        #region Mail Help

        public const string MailCancelTags = "{0} Send canceled.";

        public const string MailErrorTags = "{0} {1}";

        public const string MailSuccessTags = "Message sent.";

        public const string MailSemaphoreNameTags = "MailSemaphore";

        public const string EmailBodyTags = @"1.错误发生时间：{0}<br>
                                              2.用户：{1}<br>
                                              3.错误类：{2}<br>
                                              4.错误方法：{3}<br>
                                              5.错误内容：{4}<br>
                                              6.错误堆栈：{5}<br>";

        public const string FromMail = "FromMail";

        public const string ToMailList = "ToMailList";

        public const string MailHost = "MailHost";

        public const string MailPort = "MailPort";

        public const string MailSubjectTags = "Work flow Message";

        public const string FromMailPwd = "FromMailPwd";

        #endregion
    }
}
