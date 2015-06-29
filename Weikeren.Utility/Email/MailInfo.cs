using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Email
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MailInfo
    {
        public MailInfo()
        {
            ToAddress = new List<string>();
            ToCC = new List<string>();
            ToBCC = new List<string>();
        }
        /// <summary>
        /// 主机名称
        /// 如：smtp.qq.com
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 主机端口
        /// 如：80
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string MailServerUserName { set; get; }

        /// <summary>
        /// 口令
        /// </summary>
        public string MailServerPassword { set; get; }
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From { set; get; }

        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromName { set; get; }

        /// <summary>
        /// 是否支持Html
        /// </summary>
        public bool Html { set; get; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { set; get; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { set; get; }

        ///// <summary>
        ///// 收件人地址
        ///// </summary>
        //public string Recipient { set; get; }
        ///// <summary>
        ///// 收件人姓名
        ///// </summary>
        //public string RecipientName { set; get; }

        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> ToAddress { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public List<string> ToCC { get; set; }
        /// <summary>
        /// 密送
        /// </summary>
        public List<string> ToBCC { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encoding { get; set; }
    }
}
