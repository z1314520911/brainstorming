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
    public class CommonClass
    {
        #region 常用函数

        #region 调用html模板
        /// <summary>
        /// 调用html模板
        /// </summary>
        /// <param name="FormerName">模型名称</param>
        /// <returns></returns>
        public static string includeHTML(string FormerName)
        {
            string array = "<br /><center>文件不存在、加载文件失败...</center><br />";
            if (!string.IsNullOrEmpty(FormerName))
            {
                //if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Templets/" + FormerName)))
                if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~" + FormerName)))
                {
                    array = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~" + FormerName), System.Text.Encoding.Default);
                }
                else
                {
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~" + FormerName)))
                    {
                        array = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~" + FormerName), System.Text.Encoding.Default);
                    }
                }
            }
            return array;
        }

        /// <summary>
        /// 调用html模板
        /// </summary>
        /// <param name="FormerName">模型名称</param>
        /// <returns></returns>
        public static string includeHTML(string FormerName, ControllerBase controllerBase)
        {
            return CommonClass.includeHTML("/Areas/" + controllerBase.ControllerContext.RouteData.DataTokens["area"] + "/Views/" + FormerName);
        }
        #endregion

        //写文本
        public static void WriteTxtFile(string FilePath, string message)
        {
            try
            {
                //写文本
                StreamWriter writer = null;
                //
                string filePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);
                if (File.Exists(filePath))
                {
                    writer = File.AppendText(filePath);
                }
                else
                {
                    writer = File.CreateText(filePath);
                }
                writer.WriteLine(message);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="FilePath">文件所在目录</param>
        /// <returns></returns>
        public static List<string> ReadTxtFile(string FilePath)
        {
            List<string> list = new List<string>();
            try
            {
                FilePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);
                if (File.Exists(FilePath))
                {
                    StreamReader objReader = new StreamReader(FilePath, System.Text.Encoding.Default);
                    string sLine = "";

                    while (sLine != null)
                    {
                        sLine = objReader.ReadLine();
                        if (sLine != null)
                            list.Add(sLine);
                    }
                    //
                    objReader.Close();
                    objReader.Dispose();
                }
            }
            catch (Exception) { }
            return list;
        }

        /// <summary>
        /// 设置网站标题属性
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="value">属性</param>
        /// <returns></returns>
        public static System.Web.UI.HtmlControls.HtmlMeta AddMetaTag(string name, string value)
        {
            System.Web.UI.HtmlControls.HtmlMeta meta = new System.Web.UI.HtmlControls.HtmlMeta();
            meta.Name = name;
            meta.Content = value;
            return meta;
        }

        /// <summary>
        /// 关键字替换为红色
        /// </summary>
        /// <param name="strSource">输入记录</param>
        /// <param name="strRe">要替换的文字</param>
        /// <returns></returns>
        public static string myReplace(string strSource, string strRe)
        {
            string strSl, strRl;
            strSl = strSource.ToLower();
            strRl = strRe.ToLower();
            int i = strRe.Length;
            int start = strSl.IndexOf(strRl);
            if (start != -1)
            {
                strSource = strSource.Substring(0, start) + "<font color=#c60a00>"
                    + strSource.Substring(start, strRe.Length) + "</font>"
                    + myReplace(strSource.Substring(start + strRe.Length), strRe);
            }
            return strSource;
        }

        /// <summary>
        /// 图片宽度自动缩放
        /// </summary>
        /// <param name="filepath">原始地址</param>
        /// <param name="width">最大宽度</param>
        /// <param name="height">最大高度</param>
        /// <param name="img">控制的Image控件</param>
        public static string ShowInfoAboutImg(string filepath, int width, int height)
        {
            int Width = width;
            int Height = height;
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + filepath;
            if (File.Exists(filePath))
            {
                System.Drawing.Image bitmap = System.Drawing.Image.FromFile(filePath);
                if (bitmap.Width > 0 && bitmap.Height > 0)
                {
                    if (bitmap.Width >= width && bitmap.Height >= height)
                    {
                        if (bitmap.Width >= bitmap.Height)
                        {
                            if (((bitmap.Height * width) / bitmap.Width) >= height)
                            {
                                Width = (bitmap.Width * height) / bitmap.Height;
                                Height = height;
                            }
                            else
                            {
                                Width = width;
                                Height = (bitmap.Height * width) / bitmap.Width;
                            }
                        }
                        else
                        {
                            if (((bitmap.Width * height) / bitmap.Height) >= width)
                            {
                                Width = width;
                                Height = (bitmap.Height * width) / bitmap.Width;
                            }
                            else
                            {
                                Width = (bitmap.Width * height) / bitmap.Height;
                                Height = height;
                            }
                        }
                    }
                    else if (bitmap.Width >= width && bitmap.Height < height)
                    {
                        Width = width;
                        Height = (bitmap.Height * width) / bitmap.Width;
                    }
                    else if (bitmap.Width < width && bitmap.Height >= height)
                    {
                        Width = (bitmap.Width * height) / bitmap.Height;
                        Height = height;
                    }
                    else
                    {
                        Width = bitmap.Width;
                        Height = bitmap.Height;
                    }
                }
                bitmap.Dispose();
            }
            return "<img src=\"" + filepath + "\" width='" + Width + "' height='" + Height + "' align=\"absmiddle\" />";
        }

        /// <summary>
        /// 返回完整的超级链接
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns></returns>
        public static string FullUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (url == "#")
                {
                    return url;
                }
                url = url.Trim();
                if (url.Length >= 7)
                {
                    if (url.Substring(0, 7) != "http://")
                    {
                        return "http://" + url;
                    }
                    else
                    {
                        return url;
                    }
                }
                else
                {
                    return "http://" + url;
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 用序列化的方式对引用对象完成深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }
}