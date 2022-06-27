using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

using System.IO;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;

namespace Commons
{
    public class Common
    {
        #region 公共信息
        /// <summary>
        /// 剔除需要更新的字段
        /// </summary>
        /// <param name="prohibit">字段</param>
        /// <returns></returns>
        public static string ExcludingField(string prohibit, params string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                prohibit = prohibit.Replace(list[i], "");
            }
            return prohibit.Trim(',').Replace(",,", ",");
        }

        /// <summary>
        /// 返回正确的语言
        /// </summary>
        /// <param name="msg">控件数据</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string GetLanguages(string mylanguages)
        {
            for (int i = 0; i < Config.Languages.GetLength(0); i++)
            {
                if (mylanguages == Config.Languages[i, 1])
                {
                    return mylanguages;
                }
            }
            return Config.Languages[0, 1];
        }

        /// <summary>
        /// 路由参数
        /// </summary>
        /// <param name="page">page</param>
        /// <param name="keyword">keyword</param>
        /// <param name="list">list</param>
        /// <param name="table">table</param>
        /// <param name="id">id</param>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static object routeValues(object page, object keyword, object list, object table, object languages, object id, object type)
        {
            return new { id = id, type = type, page = page, keyword = keyword, list = list, table = table, Languages = languages };
        }

        /// <summary>
        /// 设置 SelectListItem.Selected
        /// </summary>
        /// <param name="items"></param>
        /// <param name="SelectValue"></param>
        public static void SetSelectListItem(ref List<SelectListItem> items, string SelectValue)
        {
            if (SelectValue == "")
            {
                return;
            }
            foreach (SelectListItem lt in items)
            {
                if (lt.Value == SelectValue)
                {
                    lt.Selected = true;
                }
            }
        }

        /// <summary>
        /// 获取 SelectListItem.Text
        /// </summary>
        /// <param name="items"></param>
        /// <param name="SelectValue"></param>
        /// <returns></returns>
        public static string GetSelectListItem(List<SelectListItem> items, string SelectValue)
        {
            foreach (SelectListItem lt in items)
            {
                if (lt.Value == SelectValue)
                {
                    return lt.Text;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取 SelectListItem.Value
        /// </summary>
        /// <param name="items"></param>
        /// <param name="SelectValue"></param>
        /// <returns></returns>
        public static string GetSelectListItemValue(List<SelectListItem> items, string SelectText)
        {
            foreach (SelectListItem lt in items)
            {
                if (lt.Text == SelectText)
                {
                    return lt.Value;
                }
            }
            return "";
        }


        public static string GetFilePathClass(string selectLanguage)
        {
            string filePathClass = "";
            if (File.Exists(Config.ClassPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Config.ClassPath);
                if (xmlDoc.SelectSingleNode("NewDataSet") != null)
                {
                    XmlNode xn = xmlDoc.SelectSingleNode("NewDataSet");

                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;
                        if (xe.GetAttribute("PageCode") == selectLanguage)
                        {
                            string[] a = xe.GetAttribute("Url").ToLower().Split(',');
                            for (int i = 0; i < a.Length; i++)
                            {
                                if (GetFilePath().IndexOf(a[i]) > -1)
                                {
                                    filePathClass = xe.GetAttribute("Name");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return filePathClass;
        }
        public static string GetFilePathClass(string selectLanguage, string selectPath)
        {
            string filePathClass = "";
            if (File.Exists(Config.ClassPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Config.ClassPath);
                if (xmlDoc.SelectSingleNode("NewDataSet") != null)
                {
                    XmlNode xn = xmlDoc.SelectSingleNode("NewDataSet");

                    XmlNodeList xnl = xn.ChildNodes;

                    foreach (XmlNode xnf in xnl)
                    {
                        XmlElement xe = (XmlElement)xnf;
                        if (xe.GetAttribute("PageCode") == selectLanguage)
                        {
                            string[] a = xe.GetAttribute("Url").ToLower().Split(',');
                            for (int i = 0; i < a.Length; i++)
                            {
                                if (GetFilePath().IndexOf(a[i]) > -1)
                                {
                                    filePathClass = xe.GetAttribute(selectPath);
                                    break;
                                }
                            }
                        }
                        //XmlElement xe = (XmlElement)xnf;
                        //if (GetFilePath().IndexOf(xe.GetAttribute("Type").ToLower()) > -1 && xe.GetAttribute("PageCode") == selectLanguage)
                        //{
                        //    filePathClass = xe.GetAttribute(selectPath);
                        //    break;
                        //}
                    }
                }
            }
            return filePathClass;
        }
        /// <summary>
        /// 获取文件虚拟目录
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            //return url.Substring(url.LastIndexOf("/") + 1);
            //string url = HttpContext.Current.Request.PhysicalPath.ToString().TrimEnd('\\');
            //return url.Substring(url.LastIndexOf("\\") + 1).Replace(".html","");
            //return HttpContext.Current.Request.PhysicalPath.ToString().ToLower();
            return HttpContext.Current.Request.FilePath.ToString().ToLower();
        }
        #endregion

        #region 调用xml生成整站数据
        public static void BindDataCollection(object Table, string textField, ref ArrayList List)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.DataCollectionPath);
            if (Table != null)
            {
                if (xmlDoc.SelectSingleNode("Model/" + Table) != null)
                {
                    string[] mylist1 = textField.Split(',');
                    int legth = xmlDoc.SelectSingleNode("Model/" + Table + "/Field").InnerText.Split(',').Length;
                    int legth2 = xmlDoc.SelectSingleNode("Model/" + Table).Attributes["NavText"].Value.Split(',').Length;
                    for (int i = 0; i < mylist1.Length; i++)
                    {
                        switch (mylist1[i])
                        {
                            case "Text":
                                List.Add(xmlDoc.SelectSingleNode("Model/" + Table).Attributes["Text"].Value); break;
                            case "PicPath":
                            case "NavBox":
                                List.Add(xmlDoc.SelectSingleNode("Model/" + Table).Attributes[mylist1[i]].Value); break;
                            case "NavText":
                            case "NavAction":
                            case "NavShow":
                                string[] other = xmlDoc.SelectSingleNode("Model/" + Table).Attributes[mylist1[i]].Value.Split(',');
                                if (other.Length < legth2)
                                {
                                    string[] other1 = new string[legth2];
                                    Array.Copy(other, 0, other1, 0, other.Length);
                                    List.Add(other1);
                                }
                                else
                                {
                                    List.Add(other);
                                }
                                break;
                            //List.Add(xmlDoc.SelectSingleNode("Model/" + Table).Attributes[mylist1[i]].Value.Split(',')); break;
                            default:
                                other = xmlDoc.SelectSingleNode("Model/" + Table + "/" + mylist1[i]).InnerText.Split(',');
                                if (other.Length < legth)
                                {
                                    string[] other1 = new string[legth];
                                    Array.Copy(other, 0, other1, 0, other.Length);
                                    List.Add(other1);
                                }
                                else
                                {
                                    List.Add(other);
                                }
                                break;
                        }

                    }
                }
            }
            if (List.Count < 5)
            {
                string[] mylist1 = textField.Split(',');
                for (int i = 0; i < mylist1.Length; i++)
                {
                    switch (mylist1[i])
                    {
                        case "Text":
                        case "PicPath":
                            List.Add(""); break;
                        default:
                            string[] s = new string[0];
                            List.Add(s);
                            break;
                    }

                }
            }
            //if (List.Count < 4)
            //{
            //    HttpContext.Current.Response.Write("参数错误!");
            //    HttpContext.Current.Response.End();
            //}
            //string [] strs = s.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 获取图片上传路径
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static string GetPicPath(string Table)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Config.DataCollectionPath);
            if (Table != null)
            {
                if (xmlDoc.SelectSingleNode("Model/" + Table) != null)
                {
                    return xmlDoc.SelectSingleNode("Model/" + Table).Attributes["PicPath"].Value + "/";
                }
            }
            return "";
        }
        public bool checkNode(XmlDocument xmlDoc, string selectNode)
        {
            if (xmlDoc.SelectSingleNode(selectNode) != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 数据格式转换
        /// <summary>
        /// String转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static string IsString(string value)
        {
            //if (string.IsNullOrEmpty(value))
            //{
            //    return "";
            //}
            //return Server.HtmlEncode
            //return InputText(value);
            return InputText1(value);
        }
        public static string IsString(object value)
        {
            //return InputText(Convert.ToString(value));
            return InputText1(Convert.ToString(value));
        }
        /// <summary>
        /// Int转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static int IsInt(string value)
        {
            int a;
            int.TryParse(value, out a);
            return a;
        }

        public static int getInt(object num)
        {
            if (num == null || num.ToString() == "")
                return 0;
            Regex reg = new Regex("^\\+?[0-9]*$");
            Match ma = reg.Match(num.ToString());
            if (ma.Success)
            {
                return Convert.ToInt32(num);
            }
            return 0;
        }
        /// <summary>
        /// Int转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static int IsInt(object value)
        {
            int a;
            int.TryParse(Convert.ToString(value), out a);
            return a;
        }
        /// <summary>
        /// Int转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static int IsInt(int value)
        {
            int a;
            int.TryParse(Convert.ToString(value), out a);
            return a;
        }
        /// <summary>
        /// Decimal转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static decimal IsDecimal(object value)
        {
            decimal a;
            decimal.TryParse(Convert.ToString(value), out a);
            return a;
        }
        /// <summary>
        /// Decimal转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static decimal IsDecimal(decimal value)
        {
            decimal a;
            decimal.TryParse(Convert.ToString(value), out a);
            return a;
        }
        /// <summary>
        /// Decimal转换,如果超过10000，自动转换为万
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IsDecimalSetRound(object value)
        {
            decimal a;
            decimal.TryParse(Convert.ToString(value), out a);
            if (a >= 10000)
            {
                a = a / 10000;
                a = System.Decimal.Round(a, 2);
                return a + " / 万";
            }
            else
            {
                return a + " / 元";
            }
        }
        /// <summary>
        /// Decimal转换,设置百分比 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IsDecimalSetPercentage(object value)
        {
            decimal a;
            decimal.TryParse(Convert.ToString(value), out a);
            a = a * 100;
            a = System.Decimal.Round(a, 2);
            return a.ToString().TrimEnd('0').TrimEnd('.') + "%";
        }
        /// <summary>
        /// DateTime转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static DateTime IsDateTime(object value)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(Convert.ToString(value));
                if (dt == System.DateTime.MinValue)
                { return DateTime.Now; }
                return dt;
            }
            catch { return DateTime.Now; }
        }
        ///// <summary>
        ///// Access数据库DateTime转换
        ///// </summary>
        ///// <param name="value">输入信息</param>
        ///// <returns></returns>
        //public static DateTime IsAccessDateTime(object value)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Convert.ToString(value)) || Convert.ToString(value) == "0001-1-1 0:00:00" || Convert.ToString(value) == "0001-1-1 00:00:00")
        //        { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); }
        //        return Convert.ToDateTime(value);
        //    }
        //    catch { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); }
        //}
        ///// <summary>
        ///// DateTime转换
        ///// </summary>
        ///// <param name="value">输入信息</param>
        ///// <returns></returns>
        //public static DateTime IsDateTime(DateTime value)
        //{
        //    try
        //    {
        //        if (value == DateTime.Parse("0001-1-1 0:00:00"))
        //        { return DateTime.Now; }
        //        return Convert.ToDateTime(Convert.ToString(value));
        //    }
        //    catch { return DateTime.Now; }
        //}
        /// <summary>
        /// Bool转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static bool IsBool(bool value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch { return false; }
        }
        /// <summary>
        /// Bool转换
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static bool IsBool(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                if (Convert.ToString(value) == "1")
                { return true; }
                return false;
            }
        }
        /// <summary>
        /// 去除多余的0跟小数点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDecimal(decimal value)
        {
            if (value.ToString().IndexOf('.') > -1)
            {
                return value.ToString().TrimEnd('0').TrimEnd('.');
            }
            else
            {
                return value.ToString();
            }
        }
        public static string ToDecimal(object value)
        {
            if (IsDecimal(value).ToString().IndexOf('.') > -1)
            {
                return value.ToString().TrimEnd('0').TrimEnd('.');
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// 去除多余的0跟小数点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToIsDecimal(decimal value)
        {
            return Convert.ToDecimal(ToDecimal(value));
        }
        /// <summary>
        /// Double转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double IsDouble(decimal value)
        {
            double a;
            double.TryParse(Convert.ToString(value), out a);
            return a;
        }
        public static double IsDouble(object value)
        {
            double a;
            double.TryParse(Convert.ToString(value), out a);
            return a;
        }
        /// <summary>
        /// Float转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float IsFloat(object value)
        {
            float a;
            float.TryParse(Convert.ToString(value), out a);
            return a;
        }
        public static string IsEqual(string text)
        {
            return text + "=@" + text;
        }
        public static string IsEqualAnd(string text, string text1)
        {
            return text + "=@" + text + " and " + text1 + "=@" + text1;
        }
        /// <summary>
        /// 查询条件用的参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string IsEqualMoreWhere(params string[] list)
        {
            string value = "";
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    value += list[i] + "=@" + list[i];
                }
                else
                {
                    value += " and " + list[i] + "=@" + list[i];
                }
            }
            return value;
        }
        /// <summary>
        /// 更新数据用的参数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string IsEqualMoreParam(params string[] list)
        {
            string value = "";
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    value += list[i] + "=@" + list[i];
                }
                else
                {
                    value += "," + list[i] + "=@" + list[i];
                }
            }
            return value;
        }
        public static string IsEqualOr(string text, string text1)
        {
            return text + "=@" + text + " or " + text1 + "=@" + text1;
        }
        public static string IsLike(string text)
        {
            return text + " like '%'+ @" + text + " + '%'";
        }
        #endregion

        #region xml数据格式转换
        /// <summary>
        /// 转换文本文件
        /// </summary>
        /// <param name="value">文本文件</param>
        /// <returns></returns>
        public static String filterText(String value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            return value.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }
        /// <summary>
        /// 转换XML文件
        /// </summary>
        /// <param name="value">XML文件</param>
        /// <returns></returns>
        public static string filterXml(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            return value.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&amp;", "&");
        }
        /// <summary>
        /// 判断节点是否存在
        /// </summary>
        /// <param name="xmlDoc">XML文件</param>
        /// <param name="selectNode">选择节点</param>
        /// <returns></returns>
        public static bool BoolSelectSingleNode(XmlDocument xmlDoc, string selectNode)
        {
            XmlNode singleNode = xmlDoc.SelectSingleNode(selectNode);
            if (singleNode == null)
            {
                return false;
            }
            return true;
        }

        public static string ConvertToString(string a)
        {
            if (string.IsNullOrEmpty(a))
            {
                return "";
            }
            return a;
        }
        #endregion

        #region 常用函数
        /// <summary>
        /// 替换网页格式
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string HtmlEscape(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        /// <summary>
        /// 数据截取
        /// </summary>
        /// <param name="num">截取数据的长度</param>
        /// <param name="s">数据</param>
        /// <returns></returns>
        public static string PartSubString(int byteCount, string mText)
        {
            if (string.IsNullOrEmpty(mText))
            {
                return "";
            }
            if (byteCount < 1)
                return mText;

            if (System.Text.Encoding.Default.GetByteCount(mText) <= byteCount)
            {
                return mText;
            }
            else
            {
                byte[] txtBytes = System.Text.Encoding.Default.GetBytes(mText);
                byte[] newBytes = new byte[byteCount - 4];

                for (int i = 0; i < byteCount - 4; i++)
                {
                    newBytes[i] = txtBytes[i];
                }
                string OutPut = System.Text.Encoding.Default.GetString(newBytes) + "...";
                if (OutPut.EndsWith("?...") == true)
                {
                    OutPut = OutPut.Substring(0, OutPut.Length - 4);
                    OutPut += "...";
                }
                return OutPut;
            }

        }
        /// <summary>
        /// 数据截取(无...)
        /// </summary>
        /// <param name="num">截取数据的长度</param>
        /// <param name="s">数据</param>
        /// <returns></returns>
        public static string PartSubString1(int byteCount, string mText)
        {
            if (string.IsNullOrEmpty(mText))
            {
                return "";
            }
            if (byteCount < 1)
                return mText;

            if (System.Text.Encoding.Default.GetByteCount(mText) <= byteCount)
            {
                return mText;
            }
            else
            {
                byte[] txtBytes = System.Text.Encoding.Default.GetBytes(mText);
                byte[] newBytes = new byte[byteCount - 4];

                for (int i = 0; i < byteCount - 4; i++)
                {
                    newBytes[i] = txtBytes[i];
                }
                string OutPut = System.Text.Encoding.Default.GetString(newBytes);
                if (OutPut.EndsWith("?") == true)
                {
                    OutPut = OutPut.Substring(0, OutPut.Length - 1);
                }
                return OutPut;
            }

        }
        /// <summary>
        /// 在数字面前填充0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PartSubInt(object value)
        {
            int id;
            int.TryParse(Convert.ToString(value), out id);
            return id.ToString("D7");
        }
        public static string PartSubInt(int value)
        {
            return value.ToString("D7");
        }
        /// <summary>
        /// 返回当前页数
        /// </summary>
        public static int PageRow()
        {
            int row;
            int.TryParse(Convert.ToString(HttpContext.Current.Request.QueryString["ReturnUrl"]), out row);
            if (row < 1)
            {
                return 1;
            }
            else
            {
                return row;
            }
        }
        /// <summary>
        /// 返回当前页数
        /// </summary>
        /// <param name="label">页数</param>
        /// <returns></returns>
        public static int PageRow(string label)
        {
            int row;
            int.TryParse(label.Trim(), out row);
            if (row < 1)
            {
                return 1;
            }
            else
            {
                return row;
            }
        }
        /// <summary>
        /// 首个字母大写
        /// </summary>
        /// <param name="value">输入信息</param>
        /// <returns></returns>
        public static string ToTitleCase(object value)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(value));
        }
        /// <summary>
        /// 数据防SQL注入
        /// </summary>
        /// <param name="text">关键字</param>
        /// <returns></returns>
        public static string InputText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            text = text.Trim();
            text = Regex.Replace(text, "[\\s]{2,}", " ");    //two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");    //<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");    //&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);    //any other tags
            text = text.Replace("'", "''");
            text = text.Replace("xp_cmdshell", "");
            text = text.Replace("exec master.dbo.xp_cmdshell", "");
            text = text.Replace("net localgroup administrators", "");
            text = text.Replace("delete from", "");
            text = text.Replace("net user", "");
            text = text.Replace("/add", "");
            text = text.Replace("drop table", "");
            text = text.Replace("update", "");
            //text = MaskKeyword(text);
            return text;
        }
        /// <summary>
        /// 数据防SQL注入(不屏蔽网页格式)
        /// </summary>
        /// <param name="text">关键字</param>
        /// <returns></returns>
        public static string InputText1(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            text = text.Trim();
            //text = Regex.Replace(text, "[\\s]{2,}", " ");    //two or more spaces
            //text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");    //<br>
            //text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");    //&nbsp;
            //text = text.Replace("'", "''");
            text = text.Replace("xp_cmdshell", "");
            text = text.Replace("exec master.dbo.xp_cmdshell", "");
            text = text.Replace("net localgroup administrators", "");
            text = text.Replace("delete from", "");
            text = text.Replace("net user", "");
            text = text.Replace("/add", "");
            text = text.Replace("drop table", "");
            text = text.Replace("update", "");
            //text = MaskKeyword(text);
            return text;
        }
        /// <summary>
        /// 去除网页多余格式
        /// </summary>
        /// <param name="html">输入的文字</param>
        /// <returns></returns>
        public static string wipeScript(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }
            //System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"<asp[\s\S]+</asp *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            html = RemoveHtml(html);

            //html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            //html = regex4.Replace(html, ""); //过滤iframe
            //html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //asp
            return html;

        }

        /// <summary>
        /// 删除html标签
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>删除html标签后的代码</returns>
        public static string RemoveHtml(string html)
        {
            //return Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<html(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</html>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<head(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</head>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<body(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</body>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<meta[\s\S]*?(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<style>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<script(\s[^>]*?)?>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<iframe(\s[^>]*?)?>[\s\S]*?</iframe>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<frameset(\s[^>]*?)?>[\s\S]*?</frameset>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<map(\s[^>]*?)?>[\s\S]*?</map>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<img[\s\S]*?(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<input[\s\S]*?(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<form(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</form>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<p(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</p>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<strong(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</strong>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<span(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</span>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<div(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</div>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<ul(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</ul>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<li(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</li>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<ol(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</ol>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<table(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</table>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<tr(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</tr>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<th(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</th>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<td(\s[^>]*?)?>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</td>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"on[\s\S]*='*?'", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"on[\S]*?[\s]*?=", "v=", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"class='*?'", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"class=", "v=", RegexOptions.IgnoreCase);
            return html;
        }

        /// <summary>
        /// 删除html标签
        /// </summary>
        /// <param name="html">页面代码</param>
        /// <returns>删除html标签后的代码</returns>
        public static string RemoveAllHtml(string html)
        {
            return Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase).Replace("&nbsp;", " ");
        }

        public static string ReturnString(XmlDocument xmlDoc, string selectNode)
        {
            if (xmlDoc.SelectSingleNode(selectNode) != null)
            {
                return filterXml(xmlDoc.SelectSingleNode(selectNode).InnerText);
            }
            return "";
        }


        /// <summary>
        /// 压缩页面
        /// </summary>
        /// <param name="array"></param>
        public static void CompressedPage(ref string array)
        {
            array = Regex.Replace(array, "\\s+", " ");
            array = Regex.Replace(array, ">\\s+<", "><");
        }
        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static string ReturnAlert(string msg)
        {
            return "<script type='text/javascript'>alert('" + msg + "!');</script>";
        }
        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static string ReturnAlertHref(string msg, string url)
        {
            return "<script type='text/javascript'>alert('" + msg + "');window.location.href='" + url + "';</script>";
        }
        /// <summary>
        /// 返回上一页
        /// </summary>
        /// <returns></returns>
        public static string ReturnHistory(string msg)
        {
            return "<script type='text/javascript'>alert('" + msg + "');history.go(-1);</script>";
        }
        public static string ReturnHistory()
        {
            return "<script type='text/javascript'>history.go(-1);</script>";
        }

        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static string ReturnLayerAlert(string msg)
        {
            return "<script type='text/javascript'>layui.use('layer', function(){ var layer = layui.layer;layer.msg('" + msg + "');});</script>";
        }

        /// <summary>
        /// 返回ALERT信息，返回上一页
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static string ReturnLayerHistory(string msg)
        {
            return "<script type='text/javascript'>layui.use('layer', function(){ var layer = layui.layer;layer.open({type:1,title:false,closeBtn:false,offset:'30%',content:'<div style=\"padding:50px;line-height:30px;background-color:#393D49;color:#fff;font-weight:300;font-size:160%;\">" + msg + "</div>',btn:'返回上一页',btnAlign:'c',shade:0.2 ,yes:function(){history.go(-1);}});});</script>";
        }
        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="url">跳转网址</param>
        /// <returns></returns>
        public static string ReturnDiscuzAlert(string msg, string url = "1")
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[<div class=\"tip\"><dt id=\"messagetext\"><p>" + msg + "<script type=\"text/javascript\" reload=\"1\">if(typeof errorhandle_registerform=='function'){errorhandle_registerform('" + msg + "',{});}</script></p>" + (url == "1" ? "" : "<script type=\"text/javascript\">setTimeout(function(){window.location.href='" + url + "';},'1500');</script>") + "</dt></div>]]></root>";
        }

        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="type">alert/confirm</param>
        /// <param name="url">跳转网址</param>
        /// <returns></returns>
        public static string ReturnDiscuzPopup(string msg, string type, string url = "")
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[<script type=\"text/javascript\">javascript:popup.open('" + msg + "', '" + type + "', '" + url + "');</script>]]></root>";
        }

        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="t">1.xml类型 2.js类型</param>
        /// <param name="time">自动关闭时间(毫秒)</param>
        /// <returns></returns>
        public static string ReturnDiscuzPopup(string msg, float time = 3000)
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[<script type=\"text/javascript\">javascript:popup.open('" + msg + "', 'alert', '');timer=setTimeout(function(){popup.close();},'" + time + "');</script>]]></root>";
        }

        /// <summary>
        /// 返回ALERT信息
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="t">1.xml类型 2.js类型</param>
        /// <param name="time">自动关闭时间(毫秒)</param>
        /// <returns></returns>
        public static string ReturnDiscuzJsPopup(string msg, float time = 3000)
        {
            return "<script type=\"text/javascript\">popup.open('" + msg + "', 'alert', '');timer=setTimeout(function(){popup.close();},'" + time + "');</script>";
        }


        /// <summary>
        /// 今天是星期几
        /// </summary>
        /// <returns></returns>
        public static string Week()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];

            return week;
        }

        /// <summary>
        /// 日期判断星期几
        /// </summary>
        /// <returns></returns>
        public static string Week(string day)
        {

            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(Convert.ToDateTime(day).DayOfWeek)];

            return week;
        }

        #endregion

        #region 生成默认xml配置文件
        public static void XmlCreat()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级） 
            XmlElement root = doc.CreateElement("Master");
            doc.AppendChild(root);
            //创建节点（二级） 
            for (int i = 0; i < Config.Languages.GetLength(0); i++)
            {
                XmlNode node = doc.CreateElement("Config_" + Config.Languages[i, 1]);
                //创建节点（三级） 
                ConfigBind(node, doc);
                root.AppendChild(node);
            }
            doc.Save(Config.ConfigPath);
        }
        private static void ConfigBind(XmlNode node, XmlDocument doc)
        {
            XmlElement element1 = doc.CreateElement("Title");
            element1.InnerText = "";
            node.AppendChild(element1);
            XmlElement element2 = doc.CreateElement("Web");
            element2.InnerText = "";
            node.AppendChild(element2);
            XmlElement element3 = doc.CreateElement("KeyWord");
            element3.InnerText = "";
            node.AppendChild(element3);
            XmlElement element4 = doc.CreateElement("Descrip");
            element4.InnerText = "";
            node.AppendChild(element4);
            XmlElement element5 = doc.CreateElement("Company");
            element5.InnerText = "";
            node.AppendChild(element5);
            XmlElement element6 = doc.CreateElement("Address");
            element6.InnerText = "";
            node.AppendChild(element6);
            XmlElement element7 = doc.CreateElement("Phone");
            element7.InnerText = "";
            node.AppendChild(element7);
            XmlElement elementTel = doc.CreateElement("Tel");
            elementTel.InnerText = "";
            node.AppendChild(elementTel);
            XmlElement element8 = doc.CreateElement("Fax");
            element8.InnerText = "";
            node.AppendChild(element8);
            XmlElement element14 = doc.CreateElement("About");
            element14.InnerText = "";
            node.AppendChild(element14);
            XmlElement element9 = doc.CreateElement("Zip");
            element9.InnerText = "";
            node.AppendChild(element9);
            XmlElement element10 = doc.CreateElement("QQ");
            element10.InnerText = "";
            node.AppendChild(element10);
            XmlElement QQ1 = doc.CreateElement("QQ1");
            QQ1.InnerText = "";
            node.AppendChild(QQ1);
            XmlElement element11 = doc.CreateElement("Email");
            element11.InnerText = "";
            node.AppendChild(element11);
            XmlElement Email1 = doc.CreateElement("Email1");
            Email1.InnerText = "";
            node.AppendChild(Email1);
            XmlElement Msn = doc.CreateElement("Msn");
            Msn.InnerText = "";
            node.AppendChild(Msn);
            XmlElement element12 = doc.CreateElement("Address1");
            element12.InnerText = "";
            node.AppendChild(element12);
            XmlElement element13 = doc.CreateElement("Icp");
            element13.InnerText = "";
            node.AppendChild(element13);
        }
        #endregion

        #region 自定义控件
        /// <summary>
        /// 自定义控件(dropdownlist,checkboxlist,radiolist)
        /// </summary>
        /// <param name="type">控件类型</param>
        /// <param name="name">控件名称</param>
        /// <param name="value">选中值</param>
        /// <param name="list">数据</param>
        /// <returns></returns>
        public static string BindControls(string type, string name, string value, List<SelectListItem> list)
        {
            string msg = "";
            switch (type)
            {
                case "dropdownlist":
                    msg = "<select id=\"" + name + "\" name=\"" + name + "\" lay-filter=\"" + name + "\">";
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<option value=\"" + list[i].Value + "\">" + list[i].Text + "</option>";
                    }
                    msg += "</select>";
                    msg = ReplaceSelect(msg, value);
                    break;
                case "dropdownlist1":
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<option value=\"" + list[i].Value + "\">" + list[i].Text + "</option>";
                    }
                    msg = ReplaceSelect(msg, value);
                    break;
                case "checkboxlist":
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<input id=\"" + name + i + "\" type=\"checkbox\" name=\"" + name + "\" value=\"" + list[i].Value + "\"" + (list[i].Selected ? " checked=\"checked\"" : "") + "> " + list[i].Text + " ";
                    }
                    break;
                case "layui-checkboxlist":
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<input id=\"" + name + i + "\" type=\"checkbox\" name=\"" + name + "\" value=\"" + list[i].Value + "\"" + (list[i].Selected ? " checked=\"checked\"" : "") + " title=\"" + list[i].Text + "\">";
                    }
                    break;
                case "radiolist":
                    //msg += "<input id=\"" + name + i + "\" type=\"radio\" name=\"" + name + "\" value=\"" + list[i].Value + "\"" + (list[i].Selected ? " checked=\"checked\"" : "") + "> " + list[i].Text + " ";
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<input id=\"" + name + i + "\" type=\"radio\" name=\"" + name + "\" value=\"" + list[i].Value + "\"> " + list[i].Text + " ";
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        msg = msg.Replace("value=\"" + value + "\"", " value=\"" + value + "\" checked=\"checked\"");
                    }
                    break;
            }
            return msg;
        }
        /// <summary>
        /// 控件(label,hidden,text,text1,textarea,editor,checkbox,pic,datepicker)
        /// </summary>
        /// <param name="type">label,hidden,text,text1,textarea,editor,checkbox,pic,datepicker</param>
        /// <param name="name">控件名称</param>
        /// <param name="value">选中值</param>
        /// <param name="myDescrip">描述</param>
        /// <param name="myFormat">字段类型</param>
        /// <returns></returns>
        public static string BindControls(string type, string name, string value, string myDescrip = "", string myFormat = "")
        {
            string msg = "";
            switch (type)
            {
                case "label":
                    msg += "<label>" + value + "</label><input type=\"hidden\" id=\"" + name + "\" name=\"" + name + "\" value=\"" + value + "\" />";
                    break;
                case "hidden":
                    msg += "<input id=\"" + name + "\" name=\"" + name + "\" type=hidden value=\"" + value + "\">";
                    break;
                case "text":
                    if (myFormat == "Int" || myFormat == "Double" || myFormat == "Decimal")
                    {
                        msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" class=\"layui-input\" value=\"" + value + "\" style=\"width:80px\" />";
                    }
                    else
                    {
                        msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" class=\"layui-input\" value=\"" + Common.HtmlEscape(value) + "\" />";
                    }
                    break;
                case "text1":
                    if (myFormat == "Int" || myFormat == "Double" || myFormat == "Decimal")
                    {
                        msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" class=\"layui-input\" lay-verify=\"required|number\" placeholder=\"请输入" + myDescrip + "\"  value=\"" + value + "\" style=\"width:80px\" />";
                    }
                    else
                    {
                        msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" class=\"layui-input\" lay-verify=\"required\" placeholder=\"请输入" + myDescrip + "\" value=\"" + Common.HtmlEscape(value) + "\" />";
                    }
                    break;
                case "textarea":
                    msg += "<textarea name=\"" + name + "\" id=\"" + name + "\" rows=\"4\" cols=\"25\" class=\"layui-textarea\">" + Common.HtmlEscape(value) + "</textarea>";
                    break;
                case "editor":
                    //msg += "<textarea name=\"" + name + "\" id=\"" + name + "\" rows=\"4\" cols=\"25\" class=\"text\">" + Common.HtmlEscape(value) + "</textarea><script type=\"text/javascript\">CKEDITOR.replace( '" + name + "');</script>";
                    //if (!string.IsNullOrEmpty(value))
                    //{
                    //    value = value.Replace("http://" + Request.Url.Host, "http://WebsiteUrl");
                    //}
                    msg += "<textarea name=\"" + name + "\" id=\"" + name + "\" rows=\"4\" cols=\"25\" class=\"layui-textarea\">" + Common.HtmlEscape(value) + "</textarea><script type=\"text/javascript\">$(function(){var ue" + name + " = UE.getEditor('" + name + "');$('#mybtn').click(function(){ue" + name + ".sync();$('#myform')[0].submit();});})</script>";
                    break;
                case "checkbox":
                    msg += "<input id=\"" + name + "\" type=\"checkbox\" name=\"" + name + "\" value=\"1\"" + (value == "1" ? " checked=\"checked\"" : (value.ToLower() == "true" ? " checked=\"checked\"" : "")) + "> " + myDescrip + " "; ;
                    break;
                //case "dropdownlist":
                //    msg += "<select id=\"" + name + "\" name=\"" + name + "\">";
                //    Assembly assembly = Assembly.LoadFrom(System.Web.HttpContext.Current.Server.MapPath("~/bin/" + Config.ProjectName + "Web.dll"));
                //    Type type = assembly.GetType("Web.Areas.AdminManagement." + table + "Controller");
                //    if (type != null)
                //    {
                //        MethodInfo mi = type.GetMethod("DropDownListBind", BindingFlags.Public | BindingFlags.Instance);
                //        if (mi != null)
                //        {
                //            object o = Activator.CreateInstance(type);
                //            MyParame myParame = new MyParame();
                //            myParame.Field = name;
                //            myParame.Value = value;
                //            myParame.Table = table;
                //            myParame.Languages = languages;
                //            myParame.List = Common.IsString(Request["list"]);
                //            object[] par = new object[] { myParame };
                //            msg += mi.Invoke(o, par);
                //            //msg += type.InvokeMember("DropDownListBin21d", BindingFlags.Default | BindingFlags.InvokeMethod, null, o, par);
                //        }
                //    }
                //    msg += "</select>";
                //    break;
                case "pic":
                    //if (!string.IsNullOrEmpty(value) && value != "1")
                    //{
                    //    msg1 += "<img src=\"" + value + "\" class=\"newspic\" /><input name=\"" + myField[k] + "1\" type=hidden value=\"" + value + "\"><br/>";
                    //    //msg1 += "<img src=\"/upload/" + myPicPath + "/" + value + "\" class=\"newspic\" /><input name=\"" + myField[k] + "1\" type=hidden value=\"" + value + "\"><br/>";
                    //}
                    //msg1 += "<input type=\"file\" id=\"" + myField[k] + "\" name=\"" + myField[k] + "\" class=\"layui-input\" />";
                    msg += "<div class=\"am-form-file\"><input type=\"file\" id=\"" + name + "\" name=\"" + name + "\" />";
                    if (!string.IsNullOrEmpty(value) && value != "1")
                    {
                        msg += "<div class=\"box\"><div id=\"" + name + "box\"><img style=\"width:100%\" id=\"" + name + "ImgPr\" src=\"" + value + "\"><input name=\"" + name + "1\" type=hidden value=\"" + value + "\"><input id=\"" + name + "2\" name=\"" + name + "2\" type=hidden value=\"0\"></div></div><a class='layui-btn layui-btn-danger layui-btn-small' id=\"" + name + "delete\" href='##' onclick='deleteImg(\"" + name + "\")'>删除图片</a>";
                    }
                    msg += "</div><script>$(function () { $(\"#" + name + "\").uploadPreview();});</script>";
                    break;
                case "download":
                    if (!string.IsNullOrEmpty(value) && value != "1")
                    {
                        //msg1 += "<a href=\"/upload/" + myPicPath + "/" + value + "\" target=\"_blank\">" + value + "</a><input name=\"" + myField[k] + "1\" type=hidden value=\"" + value + "\"><br/>";
                        msg += "<br/><a href=\"" + value + "\" target=\"_blank\" class='layui-btn'>" + myDescrip + "查看</a><br/><br/>";
                    }
                    msg += "<input name=\"" + name + "1\" type=text class=\"layui-input\" value=\"" + value + "\"><br/><input type=\"file\" id=\"" + name + "\" name=\"" + name + "\" />";
                    break;
                case "datepicker":
                    msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" placeholder=\"yyyy-MM-dd\" autocomplete=\"off\" class=\"layui-input\" value=\"" + (string.IsNullOrEmpty(value) ? "" : Common.HtmlEscape(Common.IsDateTime(value).ToString("yyyy-MM-dd"))) + "\" />"
                         + "<script>layui.use([\"laydate\"],function(){var laydate=layui.laydate;laydate.render({elem:\"#" + name + "\"})});</script>";
                    break;
                case "timepicker":
                    msg += "<input type=\"text\" id=\"" + name + "\" name=\"" + name + "\" placeholder=\"yyyy-MM-dd HH:mm:ss\" autocomplete=\"off\" class=\"layui-input\" value=\"" + (string.IsNullOrEmpty(value) ? "" : Common.HtmlEscape(Common.IsDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"))) + "\" />"
                         + "<script>layui.use([\"laydate\"],function(){var laydate=layui.laydate;laydate.render({elem:\"#" + name + "\",type: 'datetime'})});</script>";
                    break;
            }
            return msg;
        }
        /// <summary>
        /// 自定义控件(dropdownlist,checkboxlist,radiolist)
        /// </summary>
        /// <param name="type">控件类型</param>
        /// <param name="name">空间名称</param>
        /// <param name="value">选中值</param>
        /// <param name="list">数据</param>
        /// <param name="style">样式</param>
        /// <returns></returns>
        public static string BindControlsAmazeUi(string type, string name, string value, List<SelectListItem> list, string style = "am-btn-default")
        {
            string msg = "";
            switch (type)
            {
                case "dropdownlist":
                    msg = "<select id=\"" + name + "\" name=\"" + name + "\" lay-filter=\"" + name + "\" data-am-selected=\"{btnSize: 'sm'}\">";
                    for (int i = 0; i < list.Count; i++)
                    {
                        msg += "<option value=\"" + list[i].Value + "\">" + list[i].Text + "</option>";
                    }
                    msg += "</select>";
                    msg = ReplaceSelect(msg, value);
                    break;
                //case "dropdownlist1":
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        msg += "<option value=\"" + list[i].Value + "\">" + list[i].Text + "</option>";
                //    }
                //    msg = ReplaceSelect(msg, value);
                //    break;
                case "checkboxlist":
                    if (!string.IsNullOrEmpty(style))
                    {
                        style = " " + style.Trim();
                    }
                    msg += "<div class=\"am-btn-group\" data-am-button=\"\"> ";
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Selected)
                        {
                            msg += "<label class=\"am-btn" + style + " am-btn-xs am-active\"><input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + i + "\" value=\"" + list[i].Value + "\"  checked=\"checked\"> " + list[i].Text + " </label>";
                        }
                        else
                        {
                            msg += "<label class=\"am-btn" + style + " am-btn-xs\"><input type=\"checkbox\" name=\"" + name + "\" id=\"" + name + i + "\" value=\"" + list[i].Value + "\"> " + list[i].Text + " </label>";
                        }
                    }
                    msg += "</div>";
                    break;
                case "radiolist":
                    if (!string.IsNullOrEmpty(style))
                    {
                        style = " " + style.Trim();
                    }
                    msg += "<div class=\"am-btn-group\" data-am-button=\"\"> ";
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(value) && list[i].Value == value)
                        {
                            msg += "<label class=\"am-btn" + style + " am-btn-xs am-active\"><input type=\"radio\" name=\"" + name + "\" id=\"" + name + i + "\" value=\"" + list[i].Value + "\" checked=\"checked\"> " + list[i].Text + " </label>";
                        }
                        else
                        {
                            msg += "<label class=\"am-btn" + style + " am-btn-xs\"><input type=\"radio\" name=\"" + name + "\" id=\"" + name + i + "\" value=\"" + list[i].Value + "\"> " + list[i].Text + " </label>";
                        }
                    }
                    msg += "</div>";
                    break;
            }
            return msg;
        }

        public static List<SelectListItem> BindSelected(List<SelectListItem> list, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.IndexOf(",") > -1)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (("," + value + ",").IndexOf("," + list[i].Value + ",") > -1)
                        {
                            list[i].Selected = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Value == value)
                        {
                            list[i].Selected = true;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 自定义控件(dropdownlist,checkboxlist,radiolist)
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="rowText">rowText</param>
        /// <param name="rowValue">rowValue</param>
        /// <param name="type">控件类型</param>
        /// <param name="name">控件名称</param>
        /// <param name="value">选中值</param>
        /// <returns></returns>
        public static string BindControls(string type, System.Data.DataTable dt, string rowText, string rowValue, string name, string value)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem { Text = dr[rowText].ToString(), Value = dr[rowValue].ToString(), Selected = false });
            }
            return BindControls(type, name, value, list);
        }
        /// <summary>
        /// 绑定List<SelectListItem>
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="rowText">rowText</param>
        /// <param name="rowValue">rowValue</param>
        /// <returns></returns>
        public static List<SelectListItem> BindSelectListItem(System.Data.DataTable dt, string rowText, string rowValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem { Text = dr[rowText].ToString(), Value = dr[rowValue].ToString(), Selected = false });
            }
            return list;
        }
        /// <summary>
        /// 整合List<SelectListItem>
        /// </summary>
        /// <param name="rowText">rowText</param>
        /// <param name="rowValue">rowValue</param>
        /// <param name="list">原始数据</param>
        /// <param name="end">是否末尾添加</param>
        /// <returns></returns>
        public static List<SelectListItem> AddSelectListItem(string rowText, string rowValue, List<SelectListItem> list, bool end = true)
        {
            if (end)
            {
                list.Add(new SelectListItem { Text = rowText, Value = rowValue, Selected = false });
                return list;
            }
            else
            {
                List<SelectListItem> newlist = new List<SelectListItem>();
                newlist.Add(new SelectListItem { Text = rowText, Value = rowValue, Selected = false });
                newlist.AddRange(list);
                return newlist;
            }
        }

        /// <summary>
        /// Select控件绑值
        /// </summary>
        /// <param name="msg">控件数据</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string ReplaceSelect(string msg, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                msg = msg.Replace("<option value=\"" + value + "\">", "<option value=\"" + value + "\" selected=\"selected\">");
            }
            return msg;
        }
        public static string FullSelect(string msg, string name)
        {
            msg = "<select id=\"" + name + "\" name=\"" + name + "\" lay-filter=\"" + name + "\">" + msg + "</select>";
            return msg;
        }
        public static string FullSelect(string msg, string name, string value)
        {
            msg = ReplaceSelect("<select id=\"" + name + "\" name=\"" + name + "\" >" + msg + "</select>", value);
            return msg;
        }
        #endregion

        public static string CheckBoxHtml(object value)
        {
            if (Convert.ToString(value).ToLower() == "true")
            {
                return "<font color=\"red\">√</font>";
            }
            else if (Convert.ToString(value).ToLower() == "false")
            {
                return "<font color=\"blue\">×</font>";
            }
            return Convert.ToString(value);
        }
        public static string ReplaceEditor(object str)
        {
            string value = Common.IsString(str);
            if (string.IsNullOrEmpty(value))
            {
                value = "";
            }
            else
            {
                string myUrl = "http://" + HttpContext.Current.Request.Url.Host;
                value = System.Text.RegularExpressions.Regex.Replace(value, " _src=\"(.*?)\"", "");
                value = System.Text.RegularExpressions.Regex.Replace(value, " _href=\"(.*?)\"", "");
                value = System.Text.RegularExpressions.Regex.Replace(value, " title=\"(.*?).[jpg|jpeg|gif|bmp|png]\"", "");
                value = System.Text.RegularExpressions.Regex.Replace(value, " alt=\"(.*?).[jpg|jpeg|gif|bmp|png]\"", "");
                value = value.Replace(" src=\"" + myUrl, " src=\"");
                value = value.Replace(" href=\"" + myUrl, " href=\"");
                value = value.Replace("http://WebsiteUrl", myUrl);
            }
            return value;
        }
        /// <summary>
        /// 获取DataTable字段集合
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetIds(System.Data.DataTable dt, string field)
        {
            string ids = "";
            foreach (System.Data.DataRow arrayDR in dt.Rows)
            {
                ids += Common.IsInt(arrayDR[field]) + ",";
            }
            return ids.TrimEnd(',');
        }

        /// <summary>
        /// 转换Ids,获取字段集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string SplitIds(string ids)
        {
            string strIds = "";
            string[] a = ids.Split(',');
            foreach (string b in a)
            {
                int c = Common.IsInt(b);
                if (c > 0)
                {
                    strIds += c + ",";
                }
            }
            return strIds.TrimEnd(',');
        }

        public static string GetAvatar(object Avatar)
        {
            return string.IsNullOrEmpty(Convert.ToString(Avatar)) ? "/img/noavatar_middle.gif" : Convert.ToString(Avatar);
        }
    }
}

