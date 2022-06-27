using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;

namespace Commons
{
    /// <summary>
    /// 网站配置文件
    /// </summary>
    public class XmlEmail
    {
        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _passWord;
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        private string _host;
        /// <summary>
        /// smtp 服务器地址
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        private int _port;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _fromMail;
        /// <summary>
        /// 发送者
        /// </summary>
        public string FromMail
        {
            get { return _fromMail; }
            set { _fromMail = value; }
        }

        private string _toMail;
        /// <summary>
        /// 接收者
        /// </summary>
        public string ToMail
        {
            get { return _toMail; }
            set { _toMail = value; }
        }

        /// <summary>
        /// 手动读取配置
        /// </summary>
        public XmlEmail()
        {
            BindXml();
        }
        public void BindXml()
        {
            if (File.Exists(Config.EmailPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Config.EmailPath);
                this._userName = ReturnString(xmlDoc, "Master/Config/UserName");
                this._passWord = ReturnString(xmlDoc, "Master/Config/PassWord");
                this._host = ReturnString(xmlDoc, "Master/Config/Host");
                this._port = Common.IsInt(ReturnString(xmlDoc, "Master/Config/Port"));
                this._fromMail = ReturnString(xmlDoc, "Master/Config/FromMail");
                this._toMail = ReturnString(xmlDoc, "Master/Config/ToMail");
            }
        }

        public string ReturnString(XmlDocument xmlDoc, string selectNode)
        {
            if (xmlDoc.SelectSingleNode(selectNode) != null)
            {
                return Common.filterXml(xmlDoc.SelectSingleNode(selectNode).InnerText);
            }
            return "";
        }
    }
}