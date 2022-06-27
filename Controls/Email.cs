using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Controls
{
    public class Email
    {

        #region 发送带有图片的邮件
        private Hashtable EmbedList = new Hashtable();
        private string FixupReferences(string rawPayload, ref StringBuilder extras, string boundaryString)
        {
            //Build a symbol table to avoid redundant embedding.
            Regex imgRE, linkRE, hrefRE;
            MatchCollection imgMatches;
            string imgMatchExpression = "(?:img[^>]+src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))|url\\(['\"](?<1>[^'\"]*)['\"]\\))";
            imgRE = new Regex(imgMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string linkMatchExpression = "<\\s*link[^>]+href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))[^>]*>";
            linkRE = new Regex(linkMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //this one's for fixup of relative urls in anchors
            string refMatchExpression = "href\\s*=\\s*(?:['\"](?<1>[^\"]*)['\"]|(?<1>\\S+))";
            hrefRE = new Regex(refMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            imgMatches = imgRE.Matches(rawPayload);
            //translation to a Hashtable weeds out redundant references
            foreach (Match m in imgMatches)
            {
                if (!EmbedList.ContainsKey(m.Groups[1].Value))
                {
                    EmbedList.Add(m.Groups[1].Value, Guid.NewGuid());
                }
            }

            //Prepare embedded data
            extras.Length = 0;
            string contentType;
            ArrayList embeddees = new ArrayList(EmbedList.Keys);
            foreach (string embeddee in embeddees)
            {
                contentType = embeddee.Substring(embeddee.LastIndexOf(".") + 1).ToLower();
                extras.AppendFormat(boundaryString);
                if (contentType.Equals("jpg")) contentType = "jpeg";
                switch (contentType)
                {
                    case "jpeg":
                    case "gif":
                    case "png":
                    case "bmp":
                        extras.AppendFormat("Content-Type: image/{0}; charset=\"iso-8859-1\"\r\n", contentType);
                        extras.Append("Content-Transfer-Encoding: base64\r\n");
                        extras.Append("Content-Disposition: inline\r\n");
                        extras.AppendFormat("Content-ID: <{0}>\r\n\r\n", EmbedList[embeddee]);
                        //extras.Append(GetDataAsBase64(embeddee));
                        extras.Append("\r\n");
                        break;
                }
            }
            //Fixups for references to items now embedded
            rawPayload = imgRE.Replace(rawPayload, new MatchEvaluator(_fixup));
            return rawPayload;
        }
        private string _fixup(Match m)
        {
            string replaceThis = m.Groups[1].Value;
            string withThis = string.Format("cid:{0}", EmbedList[replaceThis]);
            return m.Value.Replace(replaceThis, withThis);
        }
        #endregion

        public static string SendEMail(string userName, string pwd, string Host, string fromMail, string toMail, string subject, string msg, int num = 0, int Port = 25)
        {
            num++;
            try
            {
                if (num <= 3)
                {
                    SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
                    smtp.EnableSsl = false;//smtp服务器是否启用SSL加密
                    smtp.Host = Host; //指定 smtp 服务器地址
                    smtp.Port = Port;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去//如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
                    smtp.UseDefaultCredentials = true;
                    //邮箱服务用户及密码
                    smtp.Credentials = new NetworkCredential(userName, pwd);
                    MailMessage mm = new MailMessage(); //实例化一个邮件类

                    //不被当作垃圾邮件的关键代码--Begin           

                    mm.Headers.Add("X-Priority", "3");
                    mm.Headers.Add("X-MSMail-Priority", "Normal");
                    mm.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");   //本文以outlook名义发送邮件，不会被当作垃圾邮件 
                    mm.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
                    mm.Headers.Add("ReturnReceipt", "1");

                    //不被当作垃圾邮件的关键代码--End  

                    mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                    mm.From = new MailAddress(fromMail, subject, Encoding.GetEncoding(936));
                    //收件方看到的邮件来源；
                    //第一个参数是发信人邮件地址
                    //第二参数是发信人显示的名称
                    //第三个参数是 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码
                    //936是简体中文的codepage值 注：上面的邮件来源，一定要和你登录邮箱的帐号一致，否则会认证失败

                    //mm.To.Add("g@163.com,h@163.com");
                    //邮件的接收者，支持群发，多个地址之间用 半角逗号 分开
                    //当然也可以用全地址添加
                    mm.To.Add(new MailAddress(toMail, subject, Encoding.GetEncoding(936)));
                    //mm.To.Add(new MailAddress("h@163.com", "接收者h", Encoding.GetEncoding(936))); 

                    //mm.ReplyTo = new MailAddress("hnsss_error@126.com", "我的接收邮箱", Encoding.GetEncoding(936));
                    //ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信
                    //上面后两个参数的意义， 同 From 的意义
                    //mm.CC.Add("a@163.com,b@163.com,c@163.com");
                    //邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开

                    //当然也可以用全地址，如下：

                    //mm.CC.Add(new MailAddress("445990702@qq.com", subject, Encoding.GetEncoding(936)));

                    //mm.Bcc.Add("d@163.com,e@163.com");
                    //邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开

                    //当然也可以用全地址，如下：
                    //mm.CC.Add(new MailAddress("d@163.com", "密送者D", Encoding.GetEncoding(936)));
                    //mm.CC.Add(new MailAddress("e@163.com", "密送者E", Encoding.GetEncoding(936))); 
                    //mm.Sender = new MailAddress("hnsss_send@126.com", "邮件发送者", Encoding.GetEncoding(936));
                    //可以任意设置，此信息包含在邮件头中，但并不会验证有效性，也不会显示给收件人
                    //说实话，我不知道有啥实际作用，大家可不理会，也可不写此项
                    mm.Subject = subject + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); //邮件标题
                    mm.SubjectEncoding = Encoding.GetEncoding(936);
                    // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                    // 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用mm.IsBodyHtml = true; //邮件正文是否是HTML格式

                    mm.BodyEncoding = Encoding.GetEncoding(936);
                    //邮件正文的编码， 设置不正确， 接收者会收到乱码
                    mm.IsBodyHtml = true;//是否是HTML邮件
                    mm.Body = msg;
                    //邮件正文mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );
                    //添加附件，第二个参数，表示附件的文件类型，可以不用指定
                    //可以添加多个附件
                    //mm.Attachments.Add(new Attachment(@"d:b.doc"));

                    smtp.Send(mm); //发送邮件，如果不返回异常， 则发送成功。
                }
            }
            catch (Exception ex)
            {
                Commons.WriteLog.CreateLog("SendEMail-ex:" + ex.ToString());
                //return ex.ToString();
                SendEMail(userName, pwd, Host, fromMail, toMail, subject, msg, num, Port);
            }
            finally { }
            return "";
        }

        public static string SendEMail1(string userName, string pwd, string Host, string fromMail, string toMail, string subject, string msg, int num = 0, int Port = 25)
        {
            num++;
            try
            {
                if (num <= 3)
                {
                    SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
                    smtp.EnableSsl = false;//smtp服务器是否启用SSL加密
                    smtp.Host = Host; //指定 smtp 服务器地址
                    smtp.Port = Port;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去//如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
                    //smtp.UseDefaultCredentials = true;
                    //邮箱服务用户及密码
                    smtp.Credentials = new NetworkCredential(userName, pwd);
                    MailMessage mm = new MailMessage(); //实例化一个邮件类

                    //不被当作垃圾邮件的关键代码--Begin           

                    mm.Headers.Add("X-Priority", "3");
                    mm.Headers.Add("X-MSMail-Priority", "Normal");
                    mm.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");   //本文以outlook名义发送邮件，不会被当作垃圾邮件 
                    mm.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
                    mm.Headers.Add("ReturnReceipt", "1");

                    //不被当作垃圾邮件的关键代码--End  

                    mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                    mm.From = new MailAddress(fromMail, subject, Encoding.GetEncoding(936));
                    //收件方看到的邮件来源；
                    //第一个参数是发信人邮件地址
                    //第二参数是发信人显示的名称
                    //第三个参数是 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码
                    //936是简体中文的codepage值 注：上面的邮件来源，一定要和你登录邮箱的帐号一致，否则会认证失败

                    //mm.To.Add("g@163.com,h@163.com");
                    //邮件的接收者，支持群发，多个地址之间用 半角逗号 分开
                    //当然也可以用全地址添加
                    mm.To.Add(new MailAddress(toMail, subject, Encoding.GetEncoding(936)));
                    //mm.To.Add(new MailAddress("h@163.com", "接收者h", Encoding.GetEncoding(936))); 

                    //mm.ReplyTo = new MailAddress("hnsss_error@126.com", "我的接收邮箱", Encoding.GetEncoding(936));
                    //ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信
                    //上面后两个参数的意义， 同 From 的意义
                    //mm.CC.Add("a@163.com,b@163.com,c@163.com");
                    //邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开

                    //当然也可以用全地址，如下：

                    //mm.CC.Add(new MailAddress("445990702@qq.com", subject, Encoding.GetEncoding(936)));

                    //mm.Bcc.Add("d@163.com,e@163.com");
                    //邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开

                    //当然也可以用全地址，如下：
                    //mm.CC.Add(new MailAddress("d@163.com", "密送者D", Encoding.GetEncoding(936)));
                    //mm.CC.Add(new MailAddress("e@163.com", "密送者E", Encoding.GetEncoding(936))); 
                    //mm.Sender = new MailAddress("hnsss_send@126.com", "邮件发送者", Encoding.GetEncoding(936));
                    //可以任意设置，此信息包含在邮件头中，但并不会验证有效性，也不会显示给收件人
                    //说实话，我不知道有啥实际作用，大家可不理会，也可不写此项
                    mm.Subject = subject + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); //邮件标题
                    mm.SubjectEncoding = Encoding.GetEncoding(936);
                    // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                    // 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用mm.IsBodyHtml = true; //邮件正文是否是HTML格式

                    mm.BodyEncoding = Encoding.GetEncoding(936);
                    //邮件正文的编码， 设置不正确， 接收者会收到乱码
                    mm.IsBodyHtml = true;//是否是HTML邮件
                    mm.Body = msg;
                    //if (!string.IsNullOrEmpty(account.id_card_img) && File.Exists(System.Web.HttpContext.Current.Server.MapPath(account.id_card_img)))
                    //{
                    //    mm.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath(account.id_card_img)));
                    //}
                    //if (!string.IsNullOrEmpty(account.id_card_img1) && File.Exists(System.Web.HttpContext.Current.Server.MapPath(account.id_card_img1)))
                    //{
                    //    mm.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath(account.id_card_img1)));
                    //}
                    //if (!string.IsNullOrEmpty(account.yinhang_zhanghao_img) && File.Exists(System.Web.HttpContext.Current.Server.MapPath(account.yinhang_zhanghao_img)))
                    //{
                    //    mm.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath(account.yinhang_zhanghao_img)));
                    //}
                    //if (!string.IsNullOrEmpty(account.yinhang_zhanghao_img1) && File.Exists(System.Web.HttpContext.Current.Server.MapPath(account.yinhang_zhanghao_img1)))
                    //{
                    //    mm.Attachments.Add(new Attachment(System.Web.HttpContext.Current.Server.MapPath(account.yinhang_zhanghao_img1)));
                    //}
                    //邮件正文mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );
                    //添加附件，第二个参数，表示附件的文件类型，可以不用指定
                    //可以添加多个附件
                    //mm.Attachments.Add(new Attachment(@"d:b.doc"));

                    smtp.Send(mm); //发送邮件，如果不返回异常， 则发送成功。
                    mm.Dispose();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
                //SendEMail1(userName, pwd, Host, fromMail, toMail, subject, msg, num, Port);
            }
            finally { }
            return "";
        }
        ///// <summary>
        ///// 从固定邮箱中发送邮件
        ///// </summary>
        ///// <param name="toMail">接收者邮箱</param>
        ///// <param name="subject">标题</param>
        ///// <param name="msg">正文</param>
        //public static void SendEMail(string toMail, string subject, string msg)
        //{
        //    string EMailSmtp = ConfigurationManager.AppSettings["EMailSmtp"];
        //    string EMailName = ConfigurationManager.AppSettings["EMailName"];
        //    string EMailPWD = ConfigurationManager.AppSettings["EMailPWD"];
        //    string EMailSend = ConfigurationManager.AppSettings["EMailSend"];
        //    string EMailOrg = ConfigurationManager.AppSettings["EMailOrg"];
        //    try
        //    {
        //        SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
        //        smtp.EnableSsl = false;//smtp服务器是否启用SSL加密
        //        smtp.Host = EMailSmtp; //指定 smtp 服务器地址
        //        smtp.Port = 25;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去//如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
        //        smtp.UseDefaultCredentials = true;
        //        //如果需要认证，则用下面的方式
        //        //smtp.Credentials = new NetworkCredential("EMailName", "EMailPass");
        //        smtp.Credentials = new NetworkCredential(EMailName, EMailPWD);
        //        MailMessage mm = new MailMessage(); //实例化一个邮件类
        //        mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
        //        mm.From = new MailAddress(EMailSend, EMailOrg, Encoding.GetEncoding(936));
        //        //收件方看到的邮件来源；
        //        //第一个参数是发信人邮件地址
        //        //第二参数是发信人显示的名称
        //        //第三个参数是 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码
        //        //936是简体中文的codepage值 注：上面的邮件来源，一定要和你登录邮箱的帐号一致，否则会认证失败

        //        //mm.To.Add("g@163.com,h@163.com");
        //        //邮件的接收者，支持群发，多个地址之间用 半角逗号 分开
        //        //当然也可以用全地址添加
        //        mm.To.Add(new MailAddress(toMail, EMailOrg, Encoding.GetEncoding(936)));
        //        //mm.To.Add(new MailAddress("h@163.com", "接收者h", Encoding.GetEncoding(936))); 

        //        //mm.ReplyTo = new MailAddress("hnsss_error@126.com", "我的接收邮箱", Encoding.GetEncoding(936));
        //        //ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信
        //        //上面后两个参数的意义， 同 From 的意义
        //        //mm.CC.Add("a@163.com,b@163.com,c@163.com");
        //        //邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开

        //        //当然也可以用全地址，如下：
        //        //mm.CC.Add(new MailAddress("14477017@qq.com", "周杰", Encoding.GetEncoding(936)));

        //        //mm.Bcc.Add("d@163.com,e@163.com");
        //        //邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开

        //        //当然也可以用全地址，如下：
        //        //mm.CC.Add(new MailAddress("d@163.com", "密送者D", Encoding.GetEncoding(936)));
        //        //mm.CC.Add(new MailAddress("e@163.com", "密送者E", Encoding.GetEncoding(936))); 
        //        //mm.Sender = new MailAddress("hnsss_send@126.com", "邮件发送者", Encoding.GetEncoding(936));
        //        //可以任意设置，此信息包含在邮件头中，但并不会验证有效性，也不会显示给收件人
        //        //说实话，我不知道有啥实际作用，大家可不理会，也可不写此项
        //        mm.Subject = subject + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); //邮件标题
        //        mm.SubjectEncoding = Encoding.GetEncoding(936);
        //        // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
        //        // 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用mm.IsBodyHtml = true; //邮件正文是否是HTML格式

        //        mm.BodyEncoding = Encoding.GetEncoding(936);
        //        //邮件正文的编码， 设置不正确， 接收者会收到乱码

        //        mm.Body = msg;
        //        //邮件正文mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );
        //        //添加附件，第二个参数，表示附件的文件类型，可以不用指定
        //        //可以添加多个附件
        //        //mm.Attachments.Add(new Attachment(@"d:b.doc"));

        //        smtp.Send(mm); //发送邮件，如果不返回异常， 则发送成功。
        //    }
        //    catch (Exception ex)
        //    {
        //        SendEMail(toMail, subject, msg);
        //    }
        //    finally { }
        //}

    }
}
