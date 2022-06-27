using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public class PagerModel
    {
        private int allPage;//总页数
        /// <summary>
        /// 总页数
        /// </summary>
        public int AllPage
        {
            get
            {
                if (totalRow % currentRow != 0)
                {
                    this.allPage = totalRow / currentRow + 1;
                }
                else
                {
                    this.allPage = totalRow / currentRow;
                }
                return allPage;
            }
        }
        private string pageNo;//当前页
        /// <summary>
        /// 当前页
        /// </summary>
        public string PageNo
        {
            get { return pageNo; }
            set
            {
                pageNo = value;
            }
        }
        private int currentPage;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage//当前页
        {
            get
            {
                int.TryParse(PageNo, out currentPage);
                if (currentPage <= 1)
                {
                    currentPage = 1;
                }
                return currentPage;
            }
        }

        private int totalRow;//总行数
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRow
        {
            get { return totalRow; }
            set { totalRow = value; }
        }

        private int currentRow;//每页显示的行数
        /// <summary>
        /// 每页显示的行数
        /// </summary>
        public int CurrentRow
        {
            get { return currentRow; }
            set { currentRow = value; }
        }

        private string selectwhere;//查询条件
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SelectWhere
        {
            get { return selectwhere; }
            set { selectwhere = value; }
        }
    }
}
