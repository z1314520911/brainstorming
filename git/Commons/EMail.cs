using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using CDO;
using System.Threading;

namespace Commons
{
    public class EMail
    {
        public static void SendMail465(string to, string title, string body)
        {
            string EMailSmtp = ConfigurationManager.AppSettings["EMailSmtp"];
            string EMailName = ConfigurationManager.AppSettings["EMailName"];
            string EMailPWD = ConfigurationManager.AppSettings["EMailPWD"];
            string EMailSend = ConfigurationManager.AppSettings["EMailSend"];
            string EMailOrg = ConfigurationManager.AppSettings["EMailOrg"];
            int EMailPort = ConfigurationManager.AppSettings["EMailPort"].Int();

            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    System.Web.Mail.MailMessage mmsg = new System.Web.Mail.MailMessage();
                    //验证  
                    mmsg.Subject = title.Trim(); ;// "zhuti1";//邮件主题

                    mmsg.BodyFormat = System.Web.Mail.MailFormat.Html;
                    mmsg.Body = body;// "wqerwerwerwer";//邮件正文
                    mmsg.BodyEncoding = Encoding.UTF8;//正文编码
                    mmsg.Priority = System.Web.Mail.MailPriority.High;//优先级

                    mmsg.From = EMailSend;//发件者邮箱地址
                    mmsg.To = to;//收件人收箱地址
                    mmsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                    //登陆名  
                    mmsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", EMailName);
                    //登陆密码  
                    mmsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", EMailPWD);
                    mmsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", EMailPort);//端口 
                    mmsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
                    System.Web.Mail.SmtpMail.SmtpServer = EMailSmtp;
                    System.Web.Mail.SmtpMail.Send(mmsg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            })).Start();
        }

        /// <summary>
        /// 从固定邮箱中发送邮件
        /// </summary>
        /// <param name="toMail">接收者邮箱</param>
        /// <param name="subject">标题</param>
        /// <param name="msg">正文</param>
        public static int SendEMail(string toMail, string subject, string msg)
        {
            int intResult = 0;
            string IsSendEMail = ConfigurationManager.AppSettings["IsSendEMail"];

            if (IsSendEMail.Equals("yes"))
            {
                string EMailSmtp = ConfigurationManager.AppSettings["EMailSmtp"];
                string EMailName = ConfigurationManager.AppSettings["EMailName"];
                string EMailPWD = ConfigurationManager.AppSettings["EMailPWD"];
                string EMailSend = ConfigurationManager.AppSettings["EMailSend"];
                string EMailOrg = ConfigurationManager.AppSettings["EMailOrg"];
                int EMailPort = ConfigurationManager.AppSettings["EMailPort"].Int();
                try
                {
                    SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
                    smtp.EnableSsl = true;//smtp服务器是否启用SSL加密
                    smtp.Host = EMailSmtp; //指定 smtp 服务器地址
                    smtp.Port = EMailPort;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去//如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
                    //smtp.UseDefaultCredentials = true;
                    //如果需要认证，则用下面的方式
                    //smtp.Credentials = new NetworkCredential("EMailName", "EMailPass");
                    smtp.Credentials = new NetworkCredential(EMailName, EMailPWD);
                    MailMessage mm = new MailMessage(); //实例化一个邮件类
                    mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                    mm.From = new MailAddress(EMailSend, EMailOrg, Encoding.GetEncoding(936));
                    mm.To.Add(new MailAddress(toMail, EMailOrg, Encoding.GetEncoding(936)));
                    mm.Subject = subject + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); //邮件标题
                    mm.SubjectEncoding = Encoding.GetEncoding(936);
                    // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                    // 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用
                    //mm.IsBodyHtml = true; //邮件正文是否是HTML格式

                    mm.BodyEncoding = Encoding.GetEncoding(936);
                    //邮件正文的编码， 设置不正确， 接收者会收到乱码

                    mm.Body = msg;
                    //邮件正文mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );
                    //添加附件，第二个参数，表示附件的文件类型，可以不用指定
                    //可以添加多个附件
                    //mm.Attachments.Add(new Attachment(@"d:b.doc"));

                    smtp.Send(mm); //发送邮件，如果不返回异常， 则发送成功。
                    intResult = 1;
                }
                catch (Exception ex)
                {
                    intResult = -1;
                    //SendEMail(toMail, subject, msg);
                }
                finally { }
            }
            return intResult;
        }


        /// <summary>
        /// 针对465加密端口 加密协议SSL（Implicit SSL）进行的邮件发送
        /// </summary>
        public static int SendMailForSSL(string toMail, string subject, string msg)
        {
            string EMailSmtp = ConfigurationManager.AppSettings["EMailSmtp"];
            string EMailName = ConfigurationManager.AppSettings["EMailName"];
            string EMailPWD = ConfigurationManager.AppSettings["EMailPWD"];
            string EMailSend = ConfigurationManager.AppSettings["EMailSend"];
            string EMailOrg = ConfigurationManager.AppSettings["EMailOrg"];
            int EMailPort = ConfigurationManager.AppSettings["EMailPort"].Int();

            try
            {
                CDO.Message oMsg = new CDO.Message();
                CDO.Configuration conf = new CDO.ConfigurationClass();
                conf.Fields[CDO.CdoConfiguration.cdoSendUsingMethod].Value = CdoSendUsing.cdoSendUsingPort;
                conf.Fields[CDO.CdoConfiguration.cdoSMTPAuthenticate].Value = CdoProtocolsAuthentication.cdoBasic;
                conf.Fields[CDO.CdoConfiguration.cdoSMTPUseSSL].Value = true;
                conf.Fields[CDO.CdoConfiguration.cdoSMTPServer].Value = EMailSmtp;//必填，而且要真实可用   
                conf.Fields[CDO.CdoConfiguration.cdoSMTPServerPort].Value = EMailPort;//465特有
                conf.Fields[CDO.CdoConfiguration.cdoSendEmailAddress].Value = "<" + EMailOrg + ">";
                conf.Fields[CDO.CdoConfiguration.cdoSendUserName].Value = EMailName;//真实的邮件地址   
                conf.Fields[CDO.CdoConfiguration.cdoSendPassword].Value = EMailPWD;   //为邮箱密码，必须真实   


                conf.Fields.Update();

                oMsg.Configuration = conf;

                oMsg.Subject = subject;
                oMsg.HTMLBody = msg;

                WriteLog.CreateLog("toMail:" + toMail);

                oMsg.From = EMailSend;
                oMsg.To = toMail;
                //ADD attachment.
                //TODO: Change the path to the file that you want to attach.
                //oMsg.AddAttachment("C:\Hello.txt", "", "");
                //oMsg.AddAttachment("C:\Test.doc", "", "");
                oMsg.Send();
                return 1;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                WriteLog.CreateLog("SendMailForSSL-ex:" + ex.ToString());
                return 0;
            }
        }
    }
}
