using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 自定义参数
    /// </summary>
    public class MyParame
    {

        /// <summary>
        /// 语言
        /// </summary>
        public string Languages { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// 其它参数
        /// </summary>
        public string List { get; set; }

        /// <summary>
        /// 表
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}