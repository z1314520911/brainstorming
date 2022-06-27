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
    public class XmlConfig
    {
        /// <summary>
        /// 微信在线调试
        /// </summary>
        public string IsWeiXin{ get; set; }

        private string web;
        /// <summary>
        /// 网站网址
        /// </summary>
        public string Web
        {
            get { return web; }
            set { web = value; }
        }
        private string title;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string keyWord;
        /// <summary>
        /// 网站关键字
        /// </summary>
        public string KeyWord
        {
            get { return keyWord; }
            set { keyWord = value; }
        }

        private string descrip;
        /// <summary>
        /// 网站描述
        /// </summary>
        public string Descrip
        {
            get { return descrip; }
            set { descrip = value; }
        }

        private string about;
        /// <summary>
        /// 公司简介
        /// </summary>
        public string About
        {
            get { return about; }
            set { about = value; }
        }

        private string zip;
        /// <summary>
        /// 公司邮编
        /// </summary>
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        private string address;
        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string phone;
        /// <summary>
        /// 销售电话①
        /// </summary>
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string tel;
        /// <summary>
        /// 销售电话②
        /// </summary>
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string fax;
        /// <summary>
        /// 公司传真
        /// </summary>
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string address1;
        /// <summary>
        /// 工厂地址
        /// </summary>
        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        private string qq;
        /// <summary>
        /// QQ①
        /// </summary>
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }

        private string qq1;
        /// <summary>
        /// QQ②
        /// </summary>
        public string QQ1
        {
            get { return qq1; }
            set { qq1 = value; }
        }

        private string company;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private string email;
        /// <summary>
        /// Email①
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string email1;
        /// <summary>
        /// Email②
        /// </summary>
        public string Email1
        {
            get { return email1; }
            set { email1 = value; }
        }

        private string msn;
        /// <summary>
        /// Msn
        /// </summary>
        public string Msn
        {
            get { return msn; }
            set { msn = value; }
        }

        private string icp;
        /// <summary>
        /// Icp
        /// </summary>
        public string Icp
        {
            get { return icp; }
            set { icp = value; }
        }
        private string templet;
        /// <summary>
        /// 模板选择
        /// </summary>
        public string Templet
        {
            get { return templet; }
            set { templet = value; }
        }
        /// <summary>
        /// 自动读取配置
        /// </summary>
        public XmlConfig()
        {
            BindXml("Cn");
        }
        /// <summary>
        /// 手动读取配置
        /// </summary>
        /// <param name="Languages">节点名</param>
        public XmlConfig(string Languages)
        {
            BindXml(Languages);
        }
        public void BindXml(string Languages)
        {
            if (File.Exists(Config.ConfigPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Config.ConfigPath);
                this.web = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Web");
                this.title = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Title");
                IsWeiXin = ReturnString(xmlDoc, "Master/Config_" + Languages + "/IsWeiXin");
                this.keyWord = ReturnString(xmlDoc, "Master/Config_" + Languages + "/KeyWord");
                this.descrip = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Descrip");
                this.about = ReturnString(xmlDoc, "Master/Config_" + Languages + "/About");
                this.zip = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Zip");
                this.address = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Address");
                this.phone = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Phone");
                this.tel = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Tel");
                this.fax = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Fax");
                this.address1 = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Address1");
                this.qq = ReturnString(xmlDoc, "Master/Config_" + Languages + "/QQ");
                this.qq1 = ReturnString(xmlDoc, "Master/Config_" + Languages + "/QQ1");
                this.company = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Company");
                this.email = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Email");
                this.email1 = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Email1");
                this.msn = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Msn");
                this.icp = ReturnString(xmlDoc, "Master/Config_" + Languages + "/Icp");
                this.templet = ReturnString(xmlDoc, "Master/Other/Templet");
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