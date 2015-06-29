using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Email
{
    /// <summary>
    /// 邮件发送接口
    /// </summary>
    public interface ISmtpMail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        void SendEmail(MailInfo mail);
        //string ReadFileContent(string FileNeme);
    }
}
