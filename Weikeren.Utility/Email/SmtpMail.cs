using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Email
{
   public  class SmtpMail:ISmtpMail
    {
       /// <summary>
       /// 发送邮件
       /// </summary>
       /// <param name="mail"></param>
        public void SendEmail(MailInfo mail)
        {
            //mail.Port = 25;
            SmtpClient client = new SmtpClient(mail.Host, mail.Port);   //设置邮件协议
            client.UseDefaultCredentials = false;//这一句得写前面
            client.DeliveryMethod = SmtpDeliveryMethod.Network; //通过网络发送到Smtp服务器
            client.Credentials = new NetworkCredential(mail.MailServerUserName, mail.MailServerPassword); //通过用户名和密码 认证
            
            MailMessage mailMessage = new MailMessage();
            if (mail.ToAddress != null && mail.ToAddress.Count>0)
            {
                foreach (var item in mail.ToAddress)
                {
                    MailAddress toAddress = new MailAddress(item);
                    mailMessage.To.Add(toAddress);
                }
            }
            if (mail.ToCC != null && mail.ToCC.Count > 0)
            {
                foreach (var item in mail.ToCC)
                {
                    MailAddress toAddress = new MailAddress(item);
                    mailMessage.CC.Add(toAddress);
                }
            }
            if (mail.ToBCC != null && mail.ToBCC.Count > 0)
            {
                foreach (var item in mail.ToBCC)
                {
                    MailAddress toAddress = new MailAddress(item);
                    mailMessage.Bcc.Add(toAddress);
                }
            }

            mailMessage.From = new MailAddress(mail.From, mail.FromName);
            mailMessage.Subject = mail.Subject;      //邮件主题
            mailMessage.SubjectEncoding = mail.Encoding == null ? Encoding.UTF8 : mail.Encoding;   //主题编码
            mailMessage.Body = mail.Body;       //邮件正文
            mailMessage.BodyEncoding = mail.Encoding == null ? Encoding.UTF8 : mail.Encoding;      //正文编码
            mailMessage.IsBodyHtml = mail.Html;    //设置为HTML格式           
            mailMessage.Priority = MailPriority.Normal;   //优先级
            
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public  string ReadFileContent(string FileNeme)
        //{
        //    string strRC = "";
        //    StreamReader sr = new StreamReader(FileNeme, Encoding.Default);
        //    strRC = sr.ReadToEnd();
        //    sr.Close();
        //    return strRC;
        //}
    }
}
