using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Objects.DataClasses;
namespace System.Web.Mvc
{
    public static class PagerHelper
    {
        public static string MobilePager(this HtmlHelper helper, string keyword, string info, PagerModel pageModel)
        {
            if (pageModel.TotalRow == 0 || pageModel.AllPage <= 1)
            {
                return "";
            }
            string aspx = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                aspx += keyword + "=" + info + "&";
            }
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page" && keys[i].ToLower() != keyword)
                {
                    if (keys[i].ToLower() == "type" && collection[keys[i]] == "Search")
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                    else if (keys[i].ToLower() == "type") { }
                    else
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<div class=\"pages\"><div>");

            if (pagePage != 1 && pageCount > 1)
            {
                pagestr.Append("<a class=\"pagepn\" href='" + aspx + prev + "'>上一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"pagepn\">上一页</span>");
            }
            pagestr.Append("<span class=\"thispage\">第 <label>" + pagePage + "</label>页 / 共 " + pageCount + " 页</span>");

            if (pagePage < pageCount)
            {
                pagestr.Append("<a class=\"pagepn\" href='" + aspx + next + "'>下一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"pagepn\">下一页</span>");
            }

            pagestr.Append("</div></div>");
            return pagestr.ToString();
        }
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="keyword">自定义参数</param>
        /// <param name="info">参数具体数据</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string WebPager(this HtmlHelper helper, string keyword, string info, Boolean list, int count, PagerModel pageModel, string Languages = "Cn")
        {
            return WebPager(keyword, info, list, count, pageModel, Languages);
        }
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="keyword">自定义参数</param>
        /// <param name="info">参数具体数据</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string WebPager(string keyword, string info, Boolean list, int count, PagerModel pageModel, string Languages = "Cn")
        {
            //if (pageModel.TotalRow == 0)
            if (pageModel.TotalRow == 0 || pageModel.AllPage <= 1)
            {
                return "";
            }
            string aspx = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                aspx += keyword + "=" + info + "&";
            }
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page" && keys[i].ToLower() != keyword)
                {
                    if (keys[i].ToLower() == "type" && collection[keys[i]] == "Search")
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                    else if (keys[i].ToLower() == "type") { }
                    else
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<div class=\"cls\"></div><div class=\"page\">");

            if (Languages == "Cn")
            {
                //pagestr.Append("共有<span>" + recordCount + "</span>条记录<span>;</span>分<span>" + pageCount + "</span>页<span>;</span>");
                if (pagePage != 1 && pageCount > 1)
                {
                    pagestr.Append("<a href='" + aspx + 1 + "' >首页</a>");
                    pagestr.Append("<a class=\"page-prev\" href='" + aspx + prev + "'>上一页</a>");
                }
                else
                {
                    pagestr.Append("<span class=\"page-first\">首页</span>");
                    pagestr.Append("<span class=\"page-top\">上一页</span>");
                }
                if (list)
                {
                    int startPage = pagePage - count / 2;
                    int endPage = pagePage + count / 2;
                    if (startPage <= 0)
                    {
                        startPage = 1;
                        endPage = count;
                    }
                    if (endPage >= pageCount)
                    {
                        startPage = pageCount - count + 1;
                        endPage = pageCount;
                    }
                    if (pageCount < count)
                    {
                        startPage = 1;
                        endPage = pageCount;
                    }
                    if (pageCount == 0)
                    {
                        startPage = 1;
                        endPage = 1;
                    }
                    if (startPage == 1 && endPage == 1)
                    {
                        pagePage = 1;
                    }
                    for (int i = startPage; i <= endPage; i++)
                    {
                        if (i == pagePage)
                        {
                            pagestr.Append("<span class=\"page-cur\">" + i + "</span>");
                        }
                        else
                        {
                            pagestr.Append("<a href='" + aspx + i + "'>" + i + "</a>");
                        }
                    }
                }
                if (pagePage < pageCount)
                {
                    pagestr.Append("<a href='" + aspx + next + "' class=\"page-next\">下一页</a>");
                    pagestr.Append("<a href='" + aspx + pageCount + "'>尾页</a>");
                }
                else
                {
                    pagestr.Append("<span class=\"page-end\">下一页</span>");
                    pagestr.Append("<span class=\"page-last\">尾页</span>");
                    pagePage = pageCount;
                }
                //pagestr.Append("<span class=\"page-skip\">共有" + recordCount + "条信息,共" + pageCount + "页</span>");
                //pagestr.Append("<span class=\"page-skip\"><form id=\"PageForm\" name=\"PageForm\" action=\"" + aspx + "\" method=\"get\">", pagePage);
                //pagestr.Append("共有" + recordCount + "条信息,共" + pageCount + "页 到第<input id=\"page\" size=\"3\" value=\"" + pagePage + "\" name=\"page\">页");
                //if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(info))
                //{
                //    pagestr.Append("<input id=\"" + keyword + "\" type=\"hidden\" value=\"" + info + "\" name=\"" + keyword + "\">");
                //}
                //pagestr.Append("<button type=\"submit\" onclick='this.form.action=\"" + aspx + "\";'>确定</button></form></span>", pagePage);
            }
            else
            {
                //pagestr.Append("A total of <span>" + recordCount + "</span>records<span>;</span><span>" + pageCount + "</span>points<span>;</span>");
                if (pagePage != 1 && pageCount > 1)
                {
                    pagestr.Append("<a href='" + aspx + 1 + "' >Home</a>");
                    pagestr.Append("<a class=\"page-prev\" href='" + aspx + prev + "'>Previous</a>");
                }
                else
                {
                    pagestr.Append("<span class=\"page-first\">Home</span>");
                    pagestr.Append("<span class=\"page-top\">Previous</span>");
                }
                if (list)
                {
                    int startPage = pagePage - count / 2;
                    int endPage = pagePage + count / 2;
                    if (startPage <= 0)
                    {
                        startPage = 1;
                        endPage = count;
                    }
                    if (endPage >= pageCount)
                    {
                        startPage = pageCount - count + 1;
                        endPage = pageCount;
                    }
                    if (pageCount < count)
                    {
                        startPage = 1;
                        endPage = pageCount;
                    }
                    if (pageCount == 0)
                    {
                        startPage = 1;
                        endPage = 1;
                    }
                    if (startPage == 1 && endPage == 1)
                    {
                        pagePage = 1;
                    }
                    for (int i = startPage; i <= endPage; i++)
                    {
                        if (i == pagePage)
                        {
                            pagestr.Append("<span class=\"page-cur\">" + i + "</span>");
                        }
                        else
                        {
                            pagestr.Append("<a href='" + aspx + i + "'>" + i + "</a>");
                        }
                    }
                }
                if (pagePage < pageCount)
                {
                    pagestr.Append("<a href='" + aspx + next + "' class=\"page-next\">Next</a>");
                    pagestr.Append("<a href='" + aspx + pageCount + "'>Last</a>");
                }
                else
                {
                    pagestr.Append("<span class=\"page-end\">Next</span>");
                    pagestr.Append("<span class=\"page-last\">Last</span>");
                    pagePage = pageCount;
                }
                //pagestr.Append("<span class=\"page-skip\"><form id=\"PageForm\" name=\"PageForm\" action=\"" + aspx + "\" method=\"get\">", pagePage);
                //pagestr.Append("Total " + recordCount + " Pieces of information," + pageCount + " Page To<input id=\"page\" size=\"3\" value=\"" + pagePage + "\" name=\"page\">Page");
                ////if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(info))
                ////{
                ////    pagestr.Append("<input id=\"" + keyword + "\" type=\"hidden\" value=\"" + info + "\" name=\"" + keyword + "\">");
                ////}
                //pagestr.Append("<button type=\"submit\" onclick='this.form.action=\"" + aspx + "\";'>Submit</button></form></span>", pagePage);
            }

            pagestr.Append("</div>");
            return pagestr.ToString();
        }
        public static string WebPager2(this HtmlHelper helper, string keyword, string info, Boolean list, int count, PagerModel pageModel)
        {
            //if (pageModel.TotalRow == 0)
            if (pageModel.TotalRow == 0 || pageModel.AllPage <= 1)
            {
                return "";
            }
            string aspx = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                aspx += keyword + "=" + info + "&";
            }
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page" && keys[i].ToLower() != keyword)
                {
                    if (keys[i].ToLower() == "type" && collection[keys[i]] == "Search")
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                    else if (keys[i].ToLower() == "type") { }
                    else
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<ul class=\"pagination\">");

            //pagestr.Append("共有<span>" + recordCount + "</span>条记录<span>;</span>分<span>" + pageCount + "</span>页<span>;</span>");
            if (pagePage != 1 && prev > count / 2)
            {
                pagestr.Append("<li><a href='" + aspx + 1 + "' >≤</a></li>");
            }
            if (list)
            {
                int startPage = pagePage - count / 2;
                int endPage = pagePage + count / 2;
                if (startPage <= 0)
                {
                    startPage = 1;
                    endPage = count;
                }
                if (endPage >= pageCount)
                {
                    startPage = pageCount - count + 1;
                    endPage = pageCount;
                }
                if (pageCount < count)
                {
                    startPage = 1;
                    endPage = pageCount;
                }
                if (pageCount == 0)
                {
                    startPage = 1;
                    endPage = 1;
                }
                if (startPage == 1 && endPage == 1)
                {
                    pagePage = 1;
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == pagePage)
                    {
                        pagestr.Append("<li class=\"active\"><a>" + i + "</a></li>");
                    }
                    else
                    {
                        pagestr.Append("<li><a href='" + aspx + i + "'>" + i + "</a></li>");
                    }
                }
            }
            if ((pagePage + count / 2) < pageCount)
            {
                pagestr.Append("<li><a href='" + aspx + pageCount + "'>≥</a></li>");
            }
            //pagestr.Append("<span class=\"page-skip\">共有" + recordCount + "条信息,共" + pageCount + "页</span>");
            //pagestr.Append("<span class=\"page-skip\"><form id=\"PageForm\" name=\"PageForm\" action=\"" + aspx + "\" method=\"get\">", pagePage);
            //pagestr.Append("共有" + recordCount + "条信息,共" + pageCount + "页 到第<input id=\"page\" size=\"3\" value=\"" + pagePage + "\" name=\"page\">页");
            //if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(info))
            //{
            //    pagestr.Append("<input id=\"" + keyword + "\" type=\"hidden\" value=\"" + info + "\" name=\"" + keyword + "\">");
            //}
            //pagestr.Append("<button type=\"submit\" onclick='this.form.action=\"" + aspx + "\";'>确定</button></form></span>", pagePage);


            pagestr.Append("</ul>");
            return pagestr.ToString();
        }
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="keyword">自定义参数</param>
        /// <param name="info">参数具体数据</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, string keyword, string info, Boolean list, int count, PagerModel pageModel)
        {
            return Pager(keyword, info, list, count, pageModel);
        }
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="keyword">自定义参数</param>
        /// <param name="info">参数具体数据</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <param name="Languages">语言</param>
        /// <returns></returns>
        public static string Pager(string keyword, string info, Boolean list, int count, PagerModel pageModel, string Languages = "Cn")
        {
            if (pageModel.TotalRow == 0)
            //if (pageModel.TotalRow == 0 || pageModel.AllPage <= 1)
            {
                return "";
            }
            string aspx = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                aspx += keyword + "=" + info + "&";
            }
            //else if (!string.IsNullOrEmpty(info))
            //{
            //    aspx += info + "&";
            //}
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page" && keys[i].ToLower() != keyword && aspx.ToLower().IndexOf(keys[i].ToLower() + '=') == -1)
                {
                    if (keys[i].ToLower() == "type" && collection[keys[i]] == "Search")
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                    else if (keys[i].ToLower() == "type") { }
                    else
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<div class=\"layui-table-tool layui-table-tool-admin\"><div class=\"layui-inline layui-table-page\" id=\"layui-table-page1\"><div class=\"layui-box layui-laypage layui-laypage-default\" id=\"layui-laypage-1\">");

            if (pagePage != 1 && pageCount > 1)
            {
                pagestr.Append("<a href='" + aspx + 1 + "' class=\"layui-laypage-prev\">首页</a>");
                pagestr.Append("<a href='" + aspx + prev + "' class=\"layui-laypage-prev\">上一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"layui-laypage-prev layui-disabled\">首页</span>");
                pagestr.Append("<span class=\"layui-laypage-prev layui-disabled\">上一页</span>");
            }
            if (list)
            {
                int startPage = pagePage - count / 2;
                int endPage = pagePage + count / 2;
                if (startPage <= 0)
                {
                    startPage = 1;
                    endPage = count;
                }
                if (endPage >= pageCount)
                {
                    startPage = pageCount - count + 1;
                    endPage = pageCount;
                }
                if (pageCount < count)
                {
                    startPage = 1;
                    endPage = pageCount;
                }
                if (pageCount == 0)
                {
                    startPage = 1;
                    endPage = 1;
                }
                if (startPage == 1 && endPage == 1)
                {
                    pagePage = 1;
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == pagePage)
                    {
                        pagestr.Append("<span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>" + i + "</em></span>");
                    }
                    else
                    {
                        pagestr.Append("<a href='" + aspx + i + "'>" + i + "</a>");
                    }
                }
            }
            if (pagePage < pageCount)
            {
                pagestr.Append("<a href='" + aspx + next + "' class=\"layui-laypage-next\">下一页</a>");
                pagestr.Append("<a href='" + aspx + pageCount + "'>尾页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"layui-laypage-next layui-disabled\">下一页</span>");
                pagestr.Append("<span class=\"layui-laypage-next layui-disabled\">尾页</span>");
                pagePage = pageCount;
            }
            pagestr.Append("<span class=\"layui-laypage-skip\"><form id=\"PageForm\" name=\"PageForm\" action=\"" + aspx + pagePage + "\" method=\"get\">");
            pagestr.Append("共有" + recordCount + "条信息，共" + pageCount + "页 到第<input id=\"page\" size=\"3\" value=\"" + pagePage + "\" name=\"page\" class=\"layui-input\">页");
            //if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(info))
            //{
            //    pagestr.Append("<input id=\"" + keyword + "\" type=\"hidden\" value=\"" + info + "\" name=\"" + keyword + "\">");
            //}
            pagestr.Append("<button type=\"submit\" onclick='this.form.action=\"" + aspx + pagePage + "\";' class=\"layui-laypage-btn\">确定</button></form></span>");


            pagestr.Append("</div></div></div>");
            return pagestr.ToString();
        }
        /// <summary>
        /// 自定义分页(多个参数)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="keyword">自定义参数，用',分隔'</param>
        /// <param name="info">参数具体数据，用',分隔'</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string AnotherPager(this HtmlHelper helper, string keyword, string info, Boolean list, int count, PagerModel pageModel)
        {
            if (pageModel.TotalRow == 0)
            {
                return "";
            }
            string aspx = "";
            string[] a = keyword.Split(',');
            string[] b = info.Split(',');

            if (a.Length != b.Length)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!string.IsNullOrEmpty(b[i]))
                    {
                        aspx += a[i] + "=" + b[i] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<div class=\"page\">");

            if (pagePage != 1 && pageCount > 1)
            {
                pagestr.Append("<a href='" + aspx + 1 + "' >首页</a>");
                pagestr.Append("<a class=\"page-prev\" href='" + aspx + prev + "'>上一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"page-first\">首页</span>");
                pagestr.Append("<span class=\"page-top\">上一页</span>");
            }
            if (list)
            {
                int startPage = pagePage - count / 2;
                int endPage = pagePage + count / 2;
                if (startPage <= 0)
                {
                    startPage = 1;
                    endPage = count;
                }
                if (endPage >= pageCount)
                {
                    startPage = pageCount - count + 1;
                    endPage = pageCount;
                }
                if (pageCount < count)
                {
                    startPage = 1;
                    endPage = pageCount;
                }
                if (pageCount == 0)
                {
                    startPage = 1;
                    endPage = 1;
                }
                if (startPage == 1 && endPage == 1)
                {
                    pagePage = 1;
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == pagePage)
                    {
                        pagestr.Append("<span class=\"page-cur\">" + i + "</span>");
                    }
                    else
                    {
                        pagestr.Append("<a href='" + aspx + i + "'>" + i + "</a>");
                    }
                }
            }
            if (pagePage < pageCount)
            {
                pagestr.Append("<a href='" + aspx + next + "' class=\"page-next\">下一页</a>");
                pagestr.Append("<a href='" + aspx + pageCount + "'>尾页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"page-end\">下一页</span>");
                pagestr.Append("<span class=\"page-last\">尾页</span>");
                pagePage = pageCount;
            }
            pagestr.Append("<span class=\"page-skip\"><form id=\"PageForm\" name=\"PageForm\" action=\"" + aspx + pagePage + "\" method=\"get\">");
            pagestr.Append("共有" + recordCount + "条信息,共" + pageCount + "页 到第<input id=\"page\" size=\"3\" value=\"" + pagePage + "\" name=\"page\">页");
            if (!string.IsNullOrEmpty(keyword))
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!string.IsNullOrEmpty(b[i]))
                    {
                        pagestr.Append("<input id=\"" + a[i] + "\" type=\"hidden\" value=\"" + a[i] + "\" name=\"" + b[i] + "\">");
                    }
                }
            }
            pagestr.Append("<button type=\"submit\" onclick='this.form.action=\"" + aspx + pagePage + "\";'>确定</button></form></span>");

            pagestr.Append("</div>");
            return pagestr.ToString();
        }
        /// <summary>
        /// 简单分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="keyword">自定义参数，用',分隔'</param>
        /// <param name="info">参数具体数据，用',分隔'</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string EasyPager(this HtmlHelper helper, string keyword, string info, PagerModel pageModel)
        {
            if (pageModel.TotalRow == 0)
            {
                return "";
            }
            string aspx = "";
            string[] a = keyword.Split(',');
            string[] b = info.Split(',');

            if (a.Length != b.Length)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!string.IsNullOrEmpty(b[i]))
                    {
                        aspx += a[i] + "=" + b[i] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<span>" + pagePage + "/" + pageCount + "</span>");

            if (pagePage != 1 && pageCount > 1)
            {
                pagestr.Append("<a class=\"previous\" href='" + aspx + prev + "'>上一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"previous\">上一页</span>");
            }
            if (pagePage < pageCount)
            {
                pagestr.Append("<a class=\"next\" href='" + aspx + next + "'>下一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"next\">下一页</span>");
                pagePage = pageCount;
            }

            return pagestr.ToString();
        }
        public static string EasyPager1(this HtmlHelper helper, string keyword, string info, PagerModel pageModel)
        {
            if (pageModel.TotalRow == 0)
            {
                return "";
            }
            string aspx = "";
            string[] a = keyword.Split(',');
            string[] b = info.Split(',');

            if (a.Length != b.Length)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!string.IsNullOrEmpty(b[i]))
                    {
                        aspx += a[i] + "=" + b[i] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<label>" + pagePage + "/" + pageCount + "</label>");

            if (pagePage != 1 && pageCount > 1)
            {
                pagestr.Append("<a class=\"purchase_numc purchase_bga\" href='" + aspx + prev + "'>上一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"purchase_numc purchase_bga\">上一页</span>");
            }
            if (pagePage < pageCount)
            {
                pagestr.Append("<a class=\"purchase_numd purchase_bga\" href='" + aspx + next + "'>下一页</a>");
            }
            else
            {
                pagestr.Append("<span class=\"purchase_numd purchase_bga\">下一页</span>");
                pagePage = pageCount;
            }

            return pagestr.ToString();
        }


        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="keyword">自定义参数</param>
        /// <param name="info">参数具体数据</param>
        /// <param name="list">是否展示数字分页</param>
        /// <param name="count">数字分页的条数</param>
        /// <param name="pageModel">分页Model</param>
        /// <returns></returns>
        public static string NumPager(this HtmlHelper helper, string keyword, string info, int count, PagerModel pageModel)
        {
            if (pageModel.TotalRow == 0)
            {
                return "";
            }
            string aspx = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                aspx += keyword + "=" + info + "&";
            }
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page" && keys[i].ToLower() != keyword)
                {
                    if (keys[i].ToLower() == "type" && collection[keys[i]] == "Search")
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                    else if (keys[i].ToLower() == "type") { }
                    else
                    {
                        aspx += keys[i] + "=" + collection[keys[i]] + "&";
                    }
                }
            }
            aspx = HttpContext.Current.Request.Url.AbsolutePath + "?" + aspx + "page=";
            //当前页
            int pagePage = pageModel.CurrentPage;
            //总页数
            int pageCount = pageModel.AllPage;
            //每页显示条数
            int pageSize = pageModel.CurrentRow;
            //数据库总记录数
            int recordCount = pageModel.TotalRow;

            int next = 0, prev = 0;
            //验证当前页是否在合理范围内
            prev = (pagePage <= 1 ? 1 : pagePage - 1);
            next = (pagePage >= pageCount ? pageCount : pagePage + 1);

            StringBuilder pagestr = new StringBuilder();
            pagestr.Append("<div class=\"Paging\">");

            //if (pagePage != 1 && pageCount > 1)
            //{
            //    pagestr.Append("<a href='" + aspx +1+ "' >首页</a>");
            //    pagestr.Append("<a class=\"page-prev\" href='" + aspx+prev+"'>上一页</a>");
            //}
            //else
            //{
            //    pagestr.Append("<span class=\"page-first\">首页</span>");
            //    pagestr.Append("<span class=\"page-top\">上一页</span>");
            //}
            int startPage = pagePage - count / 2;
            int endPage = pagePage + count / 2;
            if (startPage <= 0)
            {
                startPage = 1;
                endPage = count;
            }
            if (endPage >= pageCount)
            {
                startPage = pageCount - count + 1;
                endPage = pageCount;
            }
            if (pageCount < count)
            {
                startPage = 1;
                endPage = pageCount;
            }
            if (pageCount == 0)
            {
                startPage = 1;
                endPage = 1;
            }
            if (startPage == 1 && endPage == 1)
            {
                pagePage = 1;
            }
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == pagePage)
                {
                    pagestr.Append("<a class=\"current\">" + i + "</a>");
                }
                else
                {
                    pagestr.Append("<span><a href='" + aspx + i + "'>" + i + "</a></span>");
                }
            }
            if (pagePage < pageCount)
            {
                pagestr.Append("<a href='" + aspx + next + "'>>></a>");
            }

            pagestr.Append("<div class=\"clear\"></div></div>");
            return pagestr.ToString();
        }

        public static string NumPager1(this HtmlHelper helper, string keyword, string info, int count, PagerModel pageModel)
        {
            return NumPager(helper, keyword, info, count, pageModel).Replace("<div class=\"Paging\">", "<div class=\"Paging\"><a>总计：" + pageModel.TotalRow + " 条记录</a>");
        }
    }
}
